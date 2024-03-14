using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UseItem3002 : MonoBehaviour
{
    [SerializeField]
    public SmallDialogue dialogue_use; //dialouge after use item
    public SmallDialogue dialogue_cantuse;
    public SmallDialogue dialogue_default;
    public SmallDialogue dialogue;

    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;
    private InventoryManager theInven;
    private DatabaseManager theDB;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public string itemId;

    private bool flag;
    private bool movingInRange;
    private int size;

    private bool itemHave; // determine is item in the inventory
    public GameObject OtherCollider; // activate and deactivate gameobject
    public GameObject thisObject;
    public SpriteRenderer smokeCan;
    public ParticleSystem smoke;

    public Animator memory2Animation;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<SmallDialManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theMoving = FindObjectOfType<MovingObject>();
        theInven = FindObjectOfType<InventoryManager>();
        theDB = FindObjectOfType<DatabaseManager>();
        itemHave = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        movingInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movingInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !theInven.inventoryActive && theInven.startOtherEvent && itemHave)
        {
            theOrder.NotMove();
            StartCoroutine(ItemCanotUseCoroutine(dialogue_default));
        }
    }

    public void useItem3002(string invenItemId, int invenItemPositoin)
    {
        Debug.Log("itemId from Inven Manager: " + invenItemId);
        Debug.Log("itemId from public value: " + itemId);

        if (!movingInRange)
        {
            StartCoroutine(ItemCanotUseCoroutine(dialogue_cantuse));

        }
        else if (itemId.Equals(invenItemId) && movingInRange)
        {
            StartCoroutine(ItemUseCoroutine(dialogue_use));
            theDB.inventoryList.RemoveAt(invenItemPositoin);
            theInven.slots[invenItemPositoin].icon.sprite = Resources.Load("ItemIcons/" + "0000", typeof(Sprite)) as Sprite;
            //theDB.inventoryList.Sort();
            ShowSmoke();
            Memory2Start();

        }
    }

    IEnumerator ItemCanotUseCoroutine(SmallDialogue dialogue)
    {
        theInven.startOtherEvent = false;

        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);
        theOrder.Move();
        theInven.startOtherEvent = true;
    }

    IEnumerator ItemUseCoroutine(SmallDialogue dialogue)
    {
        theInven.startOtherEvent = false;

        itemHave = false;
        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);

    }

    public void ShowSmoke()
    {
        smoke.Play();
    }

    public void Memory2Start()
    {
        flag = true;
        showMemory();
        showDialogue();

    }



    public void showMemory()
    {
        memory2Animation.SetTrigger("memory2show");
    }

    public void showDialogue()
    {
        StartCoroutine(Memory1Coroutine());
    }

    IEnumerator Memory1Coroutine()
    {
        theOrder.NotMove();

        yield return new WaitForSecondsRealtime(4);

        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);
        smoke.Stop();
        yield return new WaitForSecondsRealtime(1);
        memory2Animation.SetTrigger("memory2finish");

        smokeCan.sprite = Resources.Load("smokefire", typeof(Sprite)) as Sprite;
        yield return new WaitForSecondsRealtime(3);
        theOrder.Move();
        thisObject.SetActive(false);
        OtherCollider.SetActive(true);
        theInven.startOtherEvent = true;
    }

}
