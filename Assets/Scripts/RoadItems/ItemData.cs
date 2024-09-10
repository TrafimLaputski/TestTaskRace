using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RoadItem")]
public class ItemData : ScriptableObject
{
    public Sprite itemBacklight = null;
    public string itemName = null;
    public List<Sprite> itemSprites = new List<Sprite>();
    public Sprite barSprite;
    [Tooltip("Item value (damage, coins count, speed bonus value)")]
    public int itemValue = 0;
    public int duration = 0;
    public int onRoadCount = 1;
    public ItemType itemType;
}

public enum ItemType
{
    Oil,
    Block,
    Crack,
    Coin,
    Heart,
    Magnet,
    Nutro,
    Shield
}