using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int addPower;
    public int AddPower { get { return addPower; } }
}
