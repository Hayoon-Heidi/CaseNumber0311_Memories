using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemManager : MonoBehaviour
{
    [SerializeField]
    public SmallDialogue dialogue_1; //before add item in inven
    public SmallDialogue dialogue_2; //after add item in inven

    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;
    private InventoryManager theInven;
    private DatabaseManager theDB;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public int itemNumber;

    private bool flag;
    private bool movingInRange;
    private int size;

    private bool itemInInven; // determine is item in the inventory
    public GameObject invenItem; // activate and deactivate gameobject

    void Start()
    {
        theDM = FindObjectOfType<SmallDialManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theMoving = FindObjectOfType<MovingObject>();
        theInven = FindObjectOfType<InventoryManager>();
        theDB = FindObjectOfType<DatabaseManager>();
        itemInInven = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movingInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movingInRange = false;
    }

    IEnumerator Event1Coroutine()
    {
        theOrder.NotMove();
        theDM.StartSmallDialogue(dialogue_1);

        yield return new WaitUntil(() => theDM.finish);
        theOrder.Move();
        flag = false;
    }

    IEnumerator Event2Coroutine()
    {
        theOrder.NotMove();
        theDM.StartSmallDialogue(dialogue_2);

        yield return new WaitUntil(() => theDM.finish);
        theOrder.Move();
        flag = false;
    }

    private void EventStart1()
    {
        flag = true;
        itemInInven = true;

        AddItemIntoInventory();

        StartCoroutine(Event1Coroutine());

        invenItem.SetActive(false);
        theOrder.Move();
    }

    private void EventStart2()
    {
        flag = true;
        StartCoroutine(Event2Coroutine());
        theOrder.Move();
    }

    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !itemInInven && !theInven.inventoryActive && theInven.startOtherEvent)
        {

            if (up && theMoving.animator.GetFloat("DirY") == 1f)
            {
                EventStart1();
            }
            else if (down && theMoving.animator.GetFloat("DirY") == -1f)
            {
                EventStart1();
            }
            else if (right && theMoving.animator.GetFloat("DirX") == 1f)
            {
                EventStart1();
            }
            else if (left && theMoving.animator.GetFloat("DirX") == -1f)
            {
                EventStart1();
            }
        } else if(movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && itemInInven && !theInven.inventoryActive && theInven.startOtherEvent)
        {
            if (up && theMoving.animator.GetFloat("DirY") == 1f)
            {
                EventStart2();
            }
            else if (down && theMoving.animator.GetFloat("DirY") == -1f)
            {
                EventStart2();
            }
            else if (right && theMoving.animator.GetFloat("DirX") == 1f)
            {
                EventStart2();
            }
            else if (left && theMoving.animator.GetFloat("DirX") == -1f)
            {
                EventStart2();
            }
        }
    }

    public void AddItemIntoInventory()
    {
        size = theDB.inventoryList.Count;

        theDB.inventoryList.Add(theDB.itemList[itemNumber]);
        theInven.slots[size].AddItem(theDB.itemList[itemNumber]);

    }
}
