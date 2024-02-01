using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Shovel,
    Pitchfork,
    Scythe,
    Rifle,
    Automatic,
    Shotgun,
    Bag,
    Speed,
    Health,
    Bullet
}

[CreateAssetMenu(fileName = "Item Data", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite itemIcon;
    public Sprite ItemIcon { get { return itemIcon; } }
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField] private string itemDesc1;
    public string ItemDesc1 { get { return itemDesc1; } }
    [SerializeField] private string itemDesc2;
    public string ItemDesc2 { get { return itemDesc2; } }
    [SerializeField] private ItemType type;
    public ItemType Type { get { return type; } }
    [SerializeField] private float value;
    public float Value { get { return value; } }
}
