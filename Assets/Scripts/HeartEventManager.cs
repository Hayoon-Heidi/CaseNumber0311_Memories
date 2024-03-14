using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEventManager : MonoBehaviour
{
    // heart use at the outro

    [SerializeField]
    public SmallDialogue dialogue;

    private SmallDialManager theDM;
    private HeartOrderManager theOrder;
    private MovingObject theMoving;
    private HeartManager theInven;


    private bool flag;
    private bool movingInRange;
    void Start()
    {
        theDM = FindObjectOfType<SmallDialManager>();
        theOrder = FindObjectOfType<HeartOrderManager>();
        theMoving = FindObjectOfType<MovingObject>();
        theInven = FindObjectOfType<HeartManager>();
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
        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);
        flag = false;
        theOrder.Move();
    }

    private void EventStart()
    {
        flag = true;
        StartCoroutine(EventCoroutine());
    }


    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !theInven.inventoryActive && theInven.startOtherEvent)
        {

                EventStart();

        }
    }
}
