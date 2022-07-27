using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data/New Item", order = 1)]
public class Item : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString().ToUpper();
    public string FriendlyName;
    public string Description;
    public Categories Category;
    public bool Stackable;
    public int BuyPrice;
    [Range(0, 1)] public float SellPercentage;
    public Sprite Icon;
    public enum Categories
    {
        Food,
        Weapon,
        Junk,
    }
}
