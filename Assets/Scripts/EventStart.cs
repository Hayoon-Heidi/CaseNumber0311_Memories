using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStart : MonoBehaviour
{

    [SerializeField]

    public SmallDialogue dialogue;

    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;

    private bool flag;

    void Start()
    {
        theDM = FindObjectOfType<SmallDialManager>();
        theOrder = FindObjectOfType<OrderManager>();    
        theMoving = FindObjectOfType<MovingObject>();
        flag = true;
        Invoke("EventStartatStart", 1);
    }

    IEnumerator EventCoroutine()
    {
        theOrder.NotMove();
        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);
        theOrder.Move();
        flag = false;
    }

    public void EventStartatStart()
    {
        if (flag)
        {
            StartCoroutine(EventCoroutine());
        }
    }
}
