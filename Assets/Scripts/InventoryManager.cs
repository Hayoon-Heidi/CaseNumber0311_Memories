using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{

    static public InventoryManager instance;

    // I had to make this as reusable script but could not consider that all the items are used differently. 
    // Therefore, had to make all the useItem script per items. 
    // Update method will determine by item id, which is name of the item icon image, to call the each item method.
    private DatabaseManager theDB;
    private OrderManager theOrder;
    private SmallDialManager theDM;
    private UseItemManager theUseItem;
    private UseItem3001 theUse3001;
    private UseItem3002 theUse3002;
    private UseItem3003 theUse3003;
    private UseItem3004 theUse3004;
    private UseItem3005 theUse3005;
    private UseItem4001 theUse4001;

    public InventorySlot[] slots;
    public Image[] slotsColor;
    public GameObject InventoryMenu;

    public AudioClip inventoryMove;
    public AudioClip inventoryOpen;
    private AudioSource audioSource;

    public bool inventoryGo;
    public bool inventoryActive;
    public bool firstInventory;
    public bool startOtherEvent;
    private int selectedSlot;
    private string itemIdAsString;

    [SerializeField]
    public SmallDialogue dialogue_1001_1;
    public SmallDialogue dialogue_1001_2;
    public SmallDialogue dialogue_1001_3;
    public SmallDialogue dialogue_default;

    void Start()
    {
        theDB = FindObjectOfType<DatabaseManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theDM = FindObjectOfType<SmallDialManager>();
        theUseItem = FindObjectOfType<UseItemManager>();
        theUse3001 = FindObjectOfType<UseItem3001>();
        theUse3002 = FindObjectOfType<UseItem3002>();
        theUse3003 = FindObjectOfType<UseItem3003>();
        theUse3004 = FindObjectOfType<UseItem3004>();
        theUse3005 = FindObjectOfType<UseItem3005>();
        theUse4001 = FindObjectOfType<UseItem4001>();


        inventoryGo = true;
        inventoryActive = false;
        firstInventory = true;
        startOtherEvent = true;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(inventoryGo)
        {

            theOrder = FindObjectOfType<OrderManager>();

            if (Input.GetKeyDown(KeyCode.Tab))
            {

                for (int i = 0; i < theDB.inventoryList.Count; i++)
                {
                    slots[i].AddItem(theDB.inventoryList[i]);
                }

                if (firstInventory && !inventoryActive)
                {

                    firstInventory = false;

                    InventoryOpen();
                }
                else if(!firstInventory && !inventoryActive)
                {
                    InventoryOpen();
                }
                else
                {
                    audioSource.PlayOneShot(inventoryOpen);
                    InventoryClose();
                }
            } 

            if(inventoryActive)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goDown(selectedSlot);
                } else if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goUp(selectedSlot);
                } else if(Input.GetKeyDown (KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goRight(selectedSlot);
                } else if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goLeft(selectedSlot);
                }

                if(Input.GetKeyDown(KeyCode.C))
                {
                    startOtherEvent = false;
                    itemIdAsString = slots[selectedSlot].icon.sprite.name;


                    if (slots[selectedSlot].icon.sprite.name != "0000")
                    {

                        if (slots[selectedSlot].icon.sprite.name == "1001")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            StartCoroutine(InventoryCoroutine(dialogue_1001_1));

                        } else if (slots[selectedSlot].icon.sprite.name == "2001")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUseItem.useItem(itemIdAsString, selectedSlot);

                        } else if(slots[selectedSlot].icon.sprite.name == "3001")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUse3001.useItem3001(itemIdAsString, selectedSlot);
                        }
                        else if (slots[selectedSlot].icon.sprite.name == "3002")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUse3002.useItem3002(itemIdAsString, selectedSlot);
                        }
                        else if (slots[selectedSlot].icon.sprite.name == "3003")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUse3003.useItem3003(itemIdAsString, selectedSlot);
                        }
                        else if (slots[selectedSlot].icon.sprite.name == "3004")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUse3004.useItem3004(itemIdAsString, selectedSlot);
                        }
                        else if (slots[selectedSlot].icon.sprite.name == "3005")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUse3005.useItem3005(itemIdAsString, selectedSlot);
                        }
                        else if (slots[selectedSlot].icon.sprite.name == "4001")
                        {
                            InventoryClose();
                            theOrder.NotMove();
                            theUse4001.useItem4001(itemIdAsString, selectedSlot);
                        }


                    } else
                    {
                        Debug.Log("Nothing is here");
                    }
                }

            }

        }
    }

    public void InventoryOpen()
    {
        audioSource.PlayOneShot(inventoryOpen);
        inventoryActive = true;
        theOrder.NotMove();
        InventoryMenu.SetActive(true);
        selectedSlot = 0;
        selectedSlotColor(selectedSlot);

        if (slots[0].icon.sprite.name == "0000")
        {
            slots[0].itemDescription_Text.text = "";
            slots[0].itemName_Text.text = "";
        }
        else
        {
            slots[0].itemName_Text.text = theDB.inventoryList[0].itemName;
            slots[0].itemDescription_Text.text = theDB.inventoryList[0].itemDescription;
        }
    }

    public void InventoryClose()
    {
        inventoryActive = false;
        theOrder.Move();
        InventoryMenu.SetActive(false);

        for(int i = 0; i < slotsColor.Length; i++)
        {
            deselectedSlotColor(i);
        }

        startOtherEvent = true;
        Debug.Log("startOtherEvent goes : " + startOtherEvent);

    }

    public void selectedSlotColor(int slot)
    {
        Color color = Color.white;
        slotsColor[slot].color = color;
    }

    public void deselectedSlotColor(int slot)
    {
        Color color = Color.black;
        color.a = 0.2f;
        slotsColor[slot].color = color;
    }

    public void goDown(int currentSlot)
    {
        if (currentSlot < 8)
        {
            deselectedSlotColor(currentSlot);
            currentSlot = currentSlot + 4;
            selectedSlotColor(currentSlot);
            selectedSlot = currentSlot;

            if (slots[currentSlot].icon.sprite.name == "0000")
            {
                slots[currentSlot].itemDescription_Text.text = "";
                slots[currentSlot].itemName_Text.text = "";
            } else
            {
                slots[currentSlot].itemName_Text.text = theDB.inventoryList[currentSlot].itemName;
                slots[currentSlot].itemDescription_Text.text = theDB.inventoryList[currentSlot].itemDescription;
            }

        }
        else
        {
            selectedSlot = currentSlot;
        }
    }
    public void goUp(int currentSlot)
    {
        if (currentSlot > 3)
        {
            deselectedSlotColor(currentSlot);
            currentSlot = currentSlot - 4;
            selectedSlotColor(currentSlot);
            selectedSlot = currentSlot;

            if (slots[currentSlot].icon.sprite.name == "0000")
            {
                slots[currentSlot].itemDescription_Text.text = "";
                slots[currentSlot].itemName_Text.text = "";
            } else
            {
                slots[currentSlot].itemName_Text.text = theDB.inventoryList[currentSlot].itemName;
                slots[currentSlot].itemDescription_Text.text = theDB.inventoryList[currentSlot].itemDescription;
            }

        }
        else
        {
            selectedSlot = currentSlot;
        }
    }
    public void goRight(int currentSlot)
    {
        if (currentSlot%4 != 3)
        {
            deselectedSlotColor(currentSlot);
            currentSlot++;
            selectedSlotColor(currentSlot);
            selectedSlot = currentSlot;

            if (slots[currentSlot].icon.sprite.name == "0000")
            {
                slots[currentSlot].itemDescription_Text.text = "";
                slots[currentSlot].itemName_Text.text = "";               
            }else
            {
                slots[currentSlot].itemName_Text.text = theDB.inventoryList[currentSlot].itemName;
                slots[currentSlot].itemDescription_Text.text = theDB.inventoryList[currentSlot].itemDescription;
            }
        }
        else
        {
            selectedSlot = currentSlot;
        }
    }
    public void goLeft(int currentSlot)
    {
        if (currentSlot % 4 != 0)
        {
            deselectedSlotColor(currentSlot);
            currentSlot--;
            selectedSlotColor(currentSlot);
            selectedSlot = currentSlot;

            if (slots[currentSlot].icon.sprite.name == "0000")
            {
                slots[currentSlot].itemDescription_Text.text = "";
                slots[currentSlot].itemName_Text.text = "";
            }else
            {
                slots[currentSlot].itemName_Text.text = theDB.inventoryList[currentSlot].itemName;
                slots[currentSlot].itemDescription_Text.text = theDB.inventoryList[currentSlot].itemDescription;
            }

        }
        else
        {
            selectedSlot = currentSlot;
        }
    }

    IEnumerator InventoryCoroutine(SmallDialogue dialogue)
    {
        startOtherEvent = false;

        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);

        theOrder.Move();
        startOtherEvent = true;
    }

}
