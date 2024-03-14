using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject exitTheGame;
    private bool exitactive;
    //what

    void Start()
    {
        exitTheGame.SetActive(false);

    }

    // Press esc to show options to exit game
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            exitTheGame.SetActive(true);
            exitactive = true;
        }

        if (exitactive && Input.GetKeyDown(KeyCode.Y))
        {
            Application.Quit();
        }

        if (exitactive && Input.GetKeyDown(KeyCode.N))
        {
            exitactive = false;
            exitTheGame.SetActive(false);
        }
         
    }
}
