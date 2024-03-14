using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{

    [SerializeField]
    public SmallDialogue dialogue;

    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;
    private InventoryManager theInven;

    //this will determine characters direction. How to write it? idk...
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    private bool flag;
    private bool movingInRange; // is the character object in the area

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
        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);
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

                if (up && theMoving.animator.GetFloat("DirY") == 1f)
                {
                    EventStart();
                }
                else if (down && theMoving.animator.GetFloat("DirY") == -1f)
                {
                    EventStart();
                }
                else if (right && theMoving.animator.GetFloat("DirX") == 1f)
                {
                    EventStart();
                }
                else if (left && theMoving.animator.GetFloat("DirX") == -1f)
                {
                    EventStart();
                }      
        }
    }
}
