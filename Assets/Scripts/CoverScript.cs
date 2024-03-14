using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Press Z key to start the game. 
        {
            FindObjectOfType<SceneChanger>().FadeToScene(1);
        } 
    }
}
