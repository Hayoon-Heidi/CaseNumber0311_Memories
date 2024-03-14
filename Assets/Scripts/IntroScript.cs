using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{

    //Manage intro scene.

    [SerializeField]
    public SmallDialogue dialogue;

    private SmallDialManager theDM;

    public Animator animatorCharacter;
    public Animator animatorPaper;
    public Animator animatorSleep;
    public Animator animatorLight;

    public Animator animatorComputer;

    private bool showScreen;
    private bool ShowInstruction;
    private int computerInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<SmallDialManager>();

        StartCoroutine(WaitForLong());
        animatorPaper.SetTrigger("movepaper");
        StartCoroutine(WaitForIt());
        animatorLight.SetTrigger("TurnLight");
        animatorSleep.SetTrigger("Wake");
        StartCoroutine(WaitForIt());

        animatorCharacter.SetTrigger("WakeCharacter");

    }

    IEnumerator WaitForIt()
    {
        yield return new WaitForSecondsRealtime(5000f);
    }

    IEnumerator WaitForLong()
    {
        yield return new WaitForSecondsRealtime(100000000f);
    }

    public void ableScreen()
    {
        showScreen = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(showScreen && !ShowInstruction)
        {
            StartCoroutine(WaitForIt());
            EventStart();
        }


        if (theDM.animDialogueWindow.GetBool("SmallAppear") == false && Input.GetKeyDown(KeyCode.Z) && showScreen && computerInt == 0)
        {
            ComputerEvent();

            //Start Computer Event
        }else if (theDM.animDialogueWindow.GetBool("SmallAppear") == false && Input.GetKeyDown(KeyCode.Z) && showScreen && computerInt == 1)
        {
            ChangeTextEvent();
            computerInt++;
        }else if (theDM.animDialogueWindow.GetBool("SmallAppear") == false && Input.GetKeyDown(KeyCode.Z) && showScreen && computerInt == 2)
        {
            FindObjectOfType<SceneChanger>().FadeToScene(2);
        }
    }

    private void EventStart()
    {
        ShowInstruction = true;
        StartCoroutine(EventCoroutine());

        Debug.Log("End instruction");
    }
    private void ComputerEvent()
    {
        animatorComputer.SetTrigger("OpenComputer");

        computerInt++;
    }

    private void ChangeTextEvent()
    {
        animatorComputer.SetTrigger("ChangeText");
    }


    IEnumerator EventCoroutine()
    {
        theDM.StartSmallDialogue(dialogue);

        yield return new WaitUntil(() => theDM.finish);


    }



}
