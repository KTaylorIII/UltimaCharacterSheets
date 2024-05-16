using Microsoft.EntityFrameworkCore;
using UltimaCharacterSheets.Data;
using UltimaCharacterSheets.Exceptions;
using UltimaCharacterSheets.Interfaces;
using UltimaCharacterSheets.Models;

namespace UltimaCharacterSheets.Services;

public interface IItemService
{
    Task<IItem> CreateArmorAsync(
        ArmorType armorType,
        Character? crafter = null,
        long? zenitCostOverride = null,
        int? defenseModifierOverride = null,
        int? magicalDefenseModifierOverride = null,
        int? weightOverride = null);

    Task<IItem> CreateWeaponAsync(
        WeaponType weaponType,
        Character? crafter = null,
        long? zenitCostOverride = null,
        int? accuracyModifierOverride = null,
        int? damageModifierOverride = null,
        DamageType? damageTypeOverride = null,
        int? weightOverride = null
    );
    Task<IEnumerable<IItem>> GetByCharacterAsync(Character character);

    Task<Character?> GetOwnerByItemAsync(IItem item);
    Task<IEnumerable<IItem>> AddItemToCharacterAsync(Character character, IItem item);

    Task<IEnumerable<IItem>> TransferItemToCharacterAsync(Character sender, Character receiver, IItem item);

    Task<IEnumerable<IItem>> DropItemFromCharacterAsync(Character character, IItem item);
}

public class DefaultItemService(ApplicationDbContext dbContext, ILoggerFactory loggerFactory) : IItemService
{
    private readonly ApplicationDbContext _context = dbContext;
    private readonly ILogger<DefaultItemService> _logger = loggerFactory.CreateLogger<DefaultItemService>();

    public async Task<IItem> CreateArmorAsync(
        ArmorType armorType,
        Character? crafter = null,
        long? zenitCostOverride = null,
        int? defenseModifierOverride = null,
        int? magicalDefenseModifierOverride = null,
        int? weightOverride = null)
    {
        var armor = new Armor
        {
            Id = Guid.NewGuid(),
            DateTimeCreated = DateTimeOffset.UtcNow,
            OwnerId = crafter?.Id,
            CreatorId = crafter?.Id,
            ArmorTypeId = armorType.Id,
            ZenitCostOverride = zenitCostOverride,
            DefenseModifierOverride = defenseModifierOverride,
            MagicalDefenseModifierOverride = magicalDefenseModifierOverride,
            WeightOverride = weightOverride
        };
        _context.Add(armor);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Armor created successfully at {Time}: {Id}", DateTimeOffset.UtcNow, armor.Id);
        return armor;
    }
    public async Task<IItem> CreateWeaponAsync(
        WeaponType weaponType,
        Character? crafter = null,
        long? zenitCostOverride = null,
        int? accuracyModifierOverride = null,
        int? damageModifierOverride = null,
        DamageType? damageTypeOverride = null,
        int? weightOverride = null
    )
    {
        var weapon = new Weapon
        {
            Id = Guid.NewGuid(),
            DateTimeCreated = DateTimeOffset.UtcNow,
            OwnerId = crafter?.Id,
            CreatorId = crafter?.Id,
            WeaponTypeId = weaponType.Id,
            ZenitCostOverride = zenitCostOverride,
            AccuracyModifierOverride = accuracyModifierOverride,
            DamageModifierOverride = damageModifierOverride,
            DamageTypeOverride = damageTypeOverride,
            WeightOverride = weightOverride,
        };
        _context.Add(weapon);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Weapon created successfully at {Time}: {Id}", DateTimeOffset.UtcNow, weapon.Id);
        return weapon;
    }
    public async Task<IEnumerable<IItem>> GetByCharacterAsync(Character character)
    {
        var weapons = await _context.Weapons.Where(w => w.Owner != null && w.Owner.Equals(character)).ToListAsync();
        var armors = await _context.Armors.Where(a => a.Owner != null && a.Owner.Equals(character)).ToListAsync();
        var shields = await _context.Shields.Where(s => s.Owner != null && s.Owner.Equals(character)).ToListAsync();
        return [.. weapons, .. armors.Select(a => a as IItem), .. shields.Select(s => s as IItem)];
    }
    public async Task<Character?> GetOwnerByItemAsync(IItem item)
    {
        var weapon = await _context.Weapons.Include(w => w.Owner).SingleOrDefaultAsync(w => w.Id.Equals(item.Id));
        var armor = await _context.Armors.Include(a => a.Owner).SingleOrDefaultAsync(a => a.Id.Equals(item.Id));
        var shield = await _context.Shields.Include(s => s.Owner).SingleOrDefaultAsync(s => s.Id.Equals(item.Id));
        return (weapon as IItem ?? armor as IItem ?? shield)?.Owner;
    }
    public async Task<IEnumerable<IItem>> AddItemToCharacterAsync(Character character, IItem item)
    {
        ArgumentNullException.ThrowIfNull(character, nameof(character));
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        var preexistingOwner = await GetOwnerByItemAsync(item);
        if (preexistingOwner != null)
        {
            throw new ItemAlreadyOwnedException();
        }
        // Fetching the DbContext-backed entity, if it exists, to guarantee that the property updates properly.
        var weapon = await _context.Weapons.Include(w => w.Owner).SingleOrDefaultAsync(w => w.Id.Equals(item.Id));
        var armor = await _context.Armors.Include(a => a.Owner).SingleOrDefaultAsync(a => a.Id.Equals(item.Id));
        var shield = await _context.Shields.Include(s => s.Owner).SingleOrDefaultAsync(s => s.Id.Equals(item.Id));
        if (weapon is not null) weapon.Owner = character;
        if (armor is not null) armor.Owner = character;
        if (shield is not null) shield.Owner = character;
        await _context.SaveChangesAsync();
        return await GetByCharacterAsync(character);
    }

    public async Task<IEnumerable<IItem>> TransferItemToCharacterAsync(Character sender, Character receiver, IItem item)
    {
        ArgumentNullException.ThrowIfNull(sender, nameof(sender));
        ArgumentNullException.ThrowIfNull(receiver, nameof(receiver));
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        if (!sender.Inventory.Select(i => i.Id).Contains(item.Id))
        {
            throw new ItemNotFoundException("Item not found in inventory.");
        }
        if (sender.EquippedWeapon != null && sender.EquippedWeapon.Equals(item) ||
        sender.EquippedArmor != null && sender.EquippedArmor.Equals(item) ||
        sender.EquippedShield != null && sender.EquippedShield.Equals(item))
        {
            throw new ItemAlreadyEquippedException("Items must be unequipped before they may be given to someone else.");
        }
        if (sender.Equals(receiver))
        {
            throw new ItemTransferException("Characters may not send items to themselves.");
        }
        var weapon = await _context.Weapons.Include(w => w.Owner).SingleOrDefaultAsync(w => w.Id.Equals(item.Id));
        var armor = await _context.Armors.Include(a => a.Owner).SingleOrDefaultAsync(a => a.Id.Equals(item.Id));
        var shield = await _context.Shields.Include(s => s.Owner).SingleOrDefaultAsync(s => s.Id.Equals(item.Id));
        if (weapon is not null) weapon.Owner = receiver;
        if (armor is not null) armor.Owner = receiver;
        if (shield is not null) shield.Owner = receiver;
        await _context.SaveChangesAsync();
        return await GetByCharacterAsync(sender);


    }

    public async Task<IEnumerable<IItem>> DropItemFromCharacterAsync(Character character, IItem item)
    {
        ArgumentNullException.ThrowIfNull(character, nameof(character));
        ArgumentNullException.ThrowIfNull(item, nameof(item));
        if (!character.Inventory.Select(i => i.Id).Contains(item.Id))
        {
            throw new ItemNotFoundException("Item not found in inventory.");
        }
        if (character.EquippedWeapon == item || character.EquippedArmor == item || character.EquippedShield == item)
        {
            throw new ItemAlreadyEquippedException("Items must be unequipped before they may be dropped.");
        }
        var weapon = await _context.Weapons.Include(w => w.Owner).SingleOrDefaultAsync(w => w.Id.Equals(item.Id));
        var armor = await _context.Armors.Include(a => a.Owner).SingleOrDefaultAsync(a => a.Id.Equals(item.Id));
        var shield = await _context.Shields.Include(s => s.Owner).SingleOrDefaultAsync(s => s.Id.Equals(item.Id));
        if (weapon is not null) weapon.Owner = null;
        if (armor is not null) armor.Owner = null;
        if (shield is not null) shield.Owner = null;
        await _context.SaveChangesAsync();
        return await GetByCharacterAsync(character);
    }
}