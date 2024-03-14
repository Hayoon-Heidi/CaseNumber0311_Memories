using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuokkaManager : MonoBehaviour
{
    //Quokka NPC manager script. This scripts control animator.

    [SerializeField]
    public SmallDialogue dialogue;
    public SmallDialogue dialogue_after;

    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;
    private InventoryManager theInven;

    private bool flag;
    private bool movingInRange;
    private bool wakeQuokka;
    private bool talkQuokka;

    public Animator quokkaAnimation;
    public GameObject block;


    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<SmallDialManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theMoving = FindObjectOfType<MovingObject>();
        theInven = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movingInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movingInRange = false;
    }

    IEnumerator QuokkaEvent1Coroutine()
    {
        theOrder.NotMove();
        quokkaAnimation.SetTrigger("Wake");
        yield return new WaitForSecondsRealtime(1);
        quokkaAnimation.SetBool("QuokkaTalk", true);

        theDM.StartSmallDialogue(dialogue);
        yield return new WaitUntil(() => theDM.finish);
        quokkaAnimation.SetBool("QuokkaTalk", false);

        theOrder.Move();
        block.SetActive(false);
        flag = false;
    }

    IEnumerator QuokkaEvent2Coroutine()
    {
        theOrder.NotMove();

        quokkaAnimation.SetBool("QuokkaTalk", true);
        theDM.StartSmallDialogue(dialogue_after);
        yield return new WaitUntil(() => theDM.finish);
        quokkaAnimation.SetBool("QuokkaTalk", false);

        theOrder.Move();
        flag = false;
    }

    private void QuokkaEvent1Start()
    {
        talkQuokka = true;
        flag = true;
        wakeQuokka = true;
        StartCoroutine(QuokkaEvent1Coroutine());
        talkQuokka = false;
    }
    private void QuokkaEvent2Start()
    {
        flag = true;
        talkQuokka = true;
        StartCoroutine(QuokkaEvent2Coroutine());
        talkQuokka = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !theInven.inventoryActive && theInven.startOtherEvent)
        {

            if (theMoving.animator.GetFloat("DirY") == 1f && !wakeQuokka && !talkQuokka)
            {
                QuokkaEvent1Start();
            }
            else if (theMoving.animator.GetFloat("DirY") == 1f && !talkQuokka)
            {
                QuokkaEvent2Start();
            }

        }
    }
}
