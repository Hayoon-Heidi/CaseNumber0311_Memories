using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

    private MovingObject moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = FindObjectOfType<MovingObject>();
    }

    public void NotMove()
    {
        moving.notMove = true;
    }

    public void Move()
    {
        moving.notMove = false; 
    }
}
