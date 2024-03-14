using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemName_Text;
    public Text itemDescription_Text;
    public GameObject selected_Item;

    private InventoryManager theInvenM;
    private DatabaseManager theDB;


    // Add item into the inventory list
    public void AddItem(Item _item)
    {
        itemName_Text.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        itemDescription_Text.text = _item.itemDescription;

    }

    public void RemoveItem()
    {
        itemName_Text.text = "";
        itemDescription_Text.text = "";
        icon.sprite = Resources.Load("ItemIcons/0000", typeof(Sprite)) as Sprite;
    }

    void Start()
    {
        theInvenM = FindObjectOfType<InventoryManager>();
        theDB = FindObjectOfType<DatabaseManager>();
    }

}
