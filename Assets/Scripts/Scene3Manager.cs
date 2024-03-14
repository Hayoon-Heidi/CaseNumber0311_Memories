using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Manager : MonoBehaviour
{
    [SerializeField]
    public SmallDialogue dialogue;

    private SmallDialManager theDM;
    private OrderManager theOrder;
    private MovingObject theMoving;
    private InventoryManager theInven;

    public Animator blackAnimation;
    public Animator memory3Animation;

    private bool movingInRange;
    private bool flag;


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

    // Update is called once per frame
    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !theInven.inventoryActive && theInven.startOtherEvent)
        {

            if (theMoving.animator.GetFloat("DirY") == 1f)
            {
                Memory3Start();
            }
        }
    }



    public void Memory3Start()
    {
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
        memory3Animation.SetTrigger("memory3Show");
    }

    public void showDialogue()
    {
        StartCoroutine(Memory1Coroutine());
    }

    IEnumerator Memory1Coroutine()
    {
        theOrder.NotMove();

        yield return new WaitForSecondsRealtime(5);

        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);

        yield return new WaitForSecondsRealtime(1);
        memory3Animation.SetTrigger("memory3Finish");
        yield return new WaitForSecondsRealtime(4);
        FindObjectOfType<SceneChanger>().FadeToScene(6);

    }

}
