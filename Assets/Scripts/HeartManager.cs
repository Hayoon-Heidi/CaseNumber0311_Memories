using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    // Use heart script at the outro scene.

    static public InventoryManager instance;

    private DatabaseManager theDB;
    private HeartOrderManager theOrderHeart;
    private SmallDialManager theDM;

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

    [SerializeField]
    public SmallDialogue dialogue_1001_heart;
    public SmallDialogue dialogue_1001_image;

    public GameObject heartImg;
    public GameObject heartLight;
    public Animator heartAnimation;
    public Animator character;
    private string itemIdAsString;

    public bool notMove = false;
    private bool canMove = true;



    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DatabaseManager>();
        theOrderHeart = FindObjectOfType<HeartOrderManager>();
        theDM = FindObjectOfType<SmallDialManager>();

        inventoryGo = true;
        inventoryActive = false;
        firstInventory = true;
        startOtherEvent = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryGo)
        {

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
                else if (!firstInventory && !inventoryActive)
                {
                    InventoryOpen();
                }
                else
                {
                    audioSource.PlayOneShot(inventoryOpen);
                    InventoryClose();
                }
            }

            if (inventoryActive)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goDown(selectedSlot);
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goUp(selectedSlot);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goRight(selectedSlot);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    audioSource.PlayOneShot(inventoryMove);
                    goLeft(selectedSlot);
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    startOtherEvent = false;
                    itemIdAsString = slots[selectedSlot].icon.sprite.name;


                    if (slots[selectedSlot].icon.sprite.name != "0000")
                    {

                        if (slots[selectedSlot].icon.sprite.name == "1001")
                        {
                            InventoryClose();
                            theOrderHeart.NotMove();
                            heartImg.SetActive(true);
                            heartLight.SetActive(true);
                            theOrderHeart.NotMove();
                            StartCoroutine(HeartCoroutine(dialogue_1001_heart, dialogue_1001_image));
                            
                        }                    
                    }
                    else
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
        theOrderHeart.NotMove();
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
        theOrderHeart.Move();
        InventoryMenu.SetActive(false);

        for (int i = 0; i < slotsColor.Length; i++)
        {
            deselectedSlotColor(i);
        }

        startOtherEvent = true;

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
            }
            else
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
            }
            else
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
        if (currentSlot % 4 != 3)
        {
            deselectedSlotColor(currentSlot);
            currentSlot++;
            selectedSlotColor(currentSlot);
            selectedSlot = currentSlot;

            if (slots[currentSlot].icon.sprite.name == "0000")
            {
                slots[currentSlot].itemDescription_Text.text = "";
                slots[currentSlot].itemName_Text.text = "";
            }
            else
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
            }
            else
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

    IEnumerator HeartCoroutine(SmallDialogue dialogue_heart, SmallDialogue dialogue_image)
    {
        theOrderHeart.NotMove();
        character.SetFloat("DirY", -1);
        startOtherEvent = false;
        theDM.StartSmallDialogue(dialogue_heart);
        yield return new WaitUntil(() => theDM.finish);
        yield return new WaitForSecondsRealtime(1);
        heartAnimation.SetTrigger("showImage"); // show image
        theDM.StartSmallDialogue(dialogue_image); // show dialogue
        theOrderHeart.NotMove();
        yield return new WaitUntil(() => theDM.finish);
        theOrderHeart.NotMove();
        yield return new WaitForSecondsRealtime(3);
        //heartAnimation.SetTrigger("goWhite");
        FindObjectOfType<SceneChanger>().FadeToScene(10);

    }
}
