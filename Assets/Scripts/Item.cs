using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// item class

[System.Serializable]
public class Item
{

    public int itemID;
    public string itemName;
    public string itemDescription;
    public int itemCount;
    public ItemType itemType;
    public Sprite itemIcon;

    public enum ItemType
    {
        Heart,
        UseAt,
        UseAny
    }

    public Item(int _itemID, string _itemName, string _itemDescription, ItemType _itemType)
    {
        this.itemID = _itemID;
        this.itemName = _itemName;
        this.itemDescription = _itemDescription;
        this.itemType = _itemType;
        this.itemCount = 1;
        this.itemIcon = Resources.Load("ItemIcons/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }

}
