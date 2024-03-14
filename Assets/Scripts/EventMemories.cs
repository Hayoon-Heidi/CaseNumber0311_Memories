using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMemories : MonoBehaviour
{
    // 19 answers 
    [SerializeField]
    public SmallDialogue dialogue1;
    public SmallDialogue dialogue2;
    public SmallDialogue dialogue3;
    public SmallDialogue dialogue4;
    public SmallDialogue dialogue5;
    public SmallDialogue dialogue6;
    public SmallDialogue dialogue7;
    public SmallDialogue dialogue8;
    public SmallDialogue dialogue9;
    public SmallDialogue dialogue10;
    public SmallDialogue dialogue11;
    public SmallDialogue dialogue12;
    public SmallDialogue dialogue13;
    public SmallDialogue dialogue14;
    public SmallDialogue dialogue15;
    public SmallDialogue dialogue16;
    public SmallDialogue dialogue17;
    public SmallDialogue dialogue18;
    public SmallDialogue dialogue19;


    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;
    private InventoryManager theInven;

    private bool flag;
    private bool movingInRange;
    public int randomNumber;

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

    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        randomNumber = Random.Range(1, 20);


        if (randomNumber == 1)
        {
            theDM.StartSmallDialogue(dialogue1);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==2 )
        {
            theDM.StartSmallDialogue(dialogue2);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==3 )
        {
            theDM.StartSmallDialogue(dialogue3);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==4 )
        {
            theDM.StartSmallDialogue(dialogue4);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==5 )
        {
            theDM.StartSmallDialogue(dialogue5);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==6 )
        {
            theDM.StartSmallDialogue(dialogue6);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==7 )
        {
            theDM.StartSmallDialogue(dialogue7);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==8 )
        {
            theDM.StartSmallDialogue(dialogue8);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==9 )
        {
            theDM.StartSmallDialogue(dialogue9);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==10 )
        {
            theDM.StartSmallDialogue(dialogue10);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==11 )
        {
            theDM.StartSmallDialogue(dialogue11);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==12 )
        {
            theDM.StartSmallDialogue(dialogue12);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==13 )
        {
            theDM.StartSmallDialogue(dialogue13);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==14 )
        {
            theDM.StartSmallDialogue(dialogue14);
            yield return new WaitUntil(() => theDM.finish);
        } else if (randomNumber ==15 )
        {
            theDM.StartSmallDialogue(dialogue15);
            yield return new WaitUntil(() => theDM.finish);
        }else if (randomNumber ==16 )
        {
            theDM.StartSmallDialogue(dialogue16);
            yield return new WaitUntil(() => theDM.finish);
        }else if (randomNumber ==17 )
        {
            theDM.StartSmallDialogue(dialogue17);
            yield return new WaitUntil(() => theDM.finish);
        }else if (randomNumber ==18 )
        {
            theDM.StartSmallDialogue(dialogue18);
            yield return new WaitUntil(() => theDM.finish);
        }else if (randomNumber ==19 )
        {
            theDM.StartSmallDialogue(dialogue19);
            yield return new WaitUntil(() => theDM.finish);
        }


        theOrder.Move();
        flag = false;
    }

    private void EventStart()
    {
        flag = true;
        StartCoroutine(EventCoroutine());
    }


    // Update is called once per frame
    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !theInven.inventoryActive && theInven.startOtherEvent)
        {

            if (theMoving.animator.GetFloat("DirY") == 1f)
            {
                EventStart();
            }

        }
    }
}
