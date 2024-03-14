using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadEvent : MonoBehaviour
{

    //Manage keypad event 

    [SerializeField]
    public SmallDialogue dialogue_1;
    public SmallDialogue dialogue_2;


    private OrderManager theOrder;
    private MovingObject theMoving;
    private KeypadSystem theKeypad;
    private SmallDialManager theDM;
    private InventoryManager theInven;

    private bool flag;

    private bool movingInRange;

    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theMoving = FindObjectOfType<MovingObject>();
        theKeypad = FindObjectOfType<KeypadSystem>();
        theDM = FindAnyObjectByType<SmallDialManager>();
        theInven = FindObjectOfType<InventoryManager>();
    }

    IEnumerator EventCoroutine()
    {
        flag = true;
        theOrder.NotMove();
        
        if(!theKeypad.PassCorrect)
        {
            theKeypad.ShowPad();

            yield return new WaitUntil(() => theKeypad.keypadFinish);

            if (theKeypad.PassCorrect)
            {
                theDM.StartSmallDialogue(dialogue_1);
                yield return new WaitUntil(() => theDM.finish);
            }
            else
            {
                theDM.StartSmallDialogue(dialogue_2);
                yield return new WaitUntil(() => theDM.finish);
            }
            theOrder.Move();
            flag = false;
        } else
        {
            FindObjectOfType<SceneChanger>().FadeToScene(4);
            theOrder.Move();
            flag = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movingInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movingInRange = false;
    }

    private void EventStart()
    {
        StartCoroutine(EventCoroutine());
    }


    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && theInven.startOtherEvent)
        {

            if (theMoving.animator.GetFloat("DirY") == 1f)
            {
                EventStart();
            }
        }
    }
}
