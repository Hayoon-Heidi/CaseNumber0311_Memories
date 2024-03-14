using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem4001 : MonoBehaviour
{
    [SerializeField]
    public SmallDialogue dialogue_use; //dialouge after use item
    public SmallDialogue dialogue_cantuse;
    public SmallDialogue dialogue_default;
    public SmallDialogue dialogue; // dialogue for memory

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
    public Animator blackAnimation;
    public Animator memory4Animation;

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

            if (theMoving.animator.GetFloat("DirY") == 1f)
            {
                theOrder.NotMove();
                StartCoroutine(ItemCanotUseCoroutine(dialogue_default));
            }
        }
    }

    public void useItem4001(string invenItemId, int invenItemPositoin)
    {

        if (!movingInRange)
        {
            theOrder.NotMove();
            StartCoroutine(ItemCanotUseCoroutine(dialogue_cantuse));

        }
        else if (itemId.Equals(invenItemId) && movingInRange)
        {
            theOrder.NotMove();
            StartCoroutine(ItemUseCoroutine(dialogue_use));
            theDB.inventoryList.RemoveAt(invenItemPositoin);
            theInven.slots[invenItemPositoin].icon.sprite = Resources.Load("ItemIcons/" + "0000", typeof(Sprite)) as Sprite;
            Memory4Start();
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
        theOrder.NotMove();
        theInven.startOtherEvent = false;

        itemHave = false;

        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);

    }

    public void Memory4Start()
    {
        theOrder.NotMove();
        flag = true;
        startFade();
        showMemory();
        showDialogue();
    }

    public void startFade()
    {
        blackAnimation.SetTrigger("goBlack");
    }

    public void showMemory()
    {
        memory4Animation.SetTrigger("memory1Show");
    }

    public void showDialogue()
    {
        StartCoroutine(Memory4Coroutine());
    }

    IEnumerator Memory4Coroutine()
    {

        yield return new WaitForSecondsRealtime(5);

        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);

        yield return new WaitForSecondsRealtime(1);
        memory4Animation.SetTrigger("memory1Finish");
        yield return new WaitForSecondsRealtime(4);
        FindObjectOfType<SceneChanger>().FadeToScene(7);

    }

}
