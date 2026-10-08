using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Equipamiento")]
    [SerializeField] private ItemData weapon;
    [SerializeField] private ItemData helmet;
    [SerializeField] private ItemData chest;
    [SerializeField] private ItemData legs;

    [Header("Consumibles")]
    [SerializeField] private ItemData offensiveItem;
    [SerializeField] private ItemData defensiveItem;

    [Header("Monedas")]
    [SerializeField] private int coins = 0;

    public ItemData Weapon => weapon;
    public ItemData Helmet => helmet;
    public ItemData Chest => chest;
    public ItemData Legs => legs;

    public ItemData OffensiveItem => offensiveItem;
    public ItemData DefensiveItem => defensiveItem;

    public int Coins => coins;

    public void SetWeapon(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Weapon)
            return;

        weapon = item;
    }

    public void SetHelmet(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Helmet)
            return;

        helmet = item;
    }

    public void SetChest(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Chest)
            return;

        chest = item;
    }

    public void SetLegs(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Legs)
            return;

        legs = item;
    }

    public void SetOffensiveItem(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Offensive)
            return;

        offensiveItem = item;
    }

    public void SetDefensiveItem(ItemData item)
    {
        if (item == null || item.itemType != ItemType.Defensive)
            return;

        defensiveItem = item;
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0)
            return;

        coins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= 0 || coins < amount)
            return false;

        coins -= amount;
        return true;
    }
}