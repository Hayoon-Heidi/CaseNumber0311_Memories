using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{

    public GameObject message; // message canvas
    public GameObject Produce; // producer canvas
    public GameObject SH; // special thanks to canvas
    public GameObject final; 

    public Animator credit; // animator that controls fade in/out 

    IEnumerator Wait1endCoroutine()
    {
        credit.SetTrigger("endCredit");
        yield return new WaitForSecondsRealtime(1);
        Produce.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        credit.SetBool("fadefinish", true);
        credit.SetBool("fadestart", false);
        yield return new WaitForSecondsRealtime(5);
        credit.SetBool("fadefinish", false);
        endProduce();
    }

    IEnumerator Wait2endCoroutine()
    {
        credit.SetBool("fadestart", true);
        yield return new WaitForSecondsRealtime(1);
        SH.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        credit.SetBool("fadefinish", true);
        credit.SetBool("fadestart", false);
        yield return new WaitForSecondsRealtime(5);
        credit.SetBool("fadefinish", false);
        endSH();
    }

    IEnumerator Wait3endCoroutine()
    {
        credit.SetBool("fadestart", true);
        yield return new WaitForSecondsRealtime(1);
        final.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        credit.SetBool("fadefinish", true);
        credit.SetBool("fadestart", false);
        yield return new WaitForSecondsRealtime(5);
        credit.SetBool("fadefinish", false);
        Application.Quit();
    }

    //this method will called from animator/
    public void endCredit() 
    {
        StartCoroutine(Wait1endCoroutine());

    }

    public void endProduce()
    {
        StartCoroutine(Wait2endCoroutine());
    }

    public void endSH()
    {

        StartCoroutine(Wait3endCoroutine());

    }

}
