using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOrderManager : MonoBehaviour
{
    // Character object, that in the outro scene, can not use default order manager due to it only shows moving action but position change.
    // Therefore, it had to have it's own order manager. 

    private outroMovingObject heart;

    void Start()
    {
        heart = FindObjectOfType<outroMovingObject>();
    }

    public void NotMove()
    {
         heart.notMove = true;
    }

    public void Move()
    {
        heart.notMove = false; 
    }
}
