using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{

    private DatabaseManager theDB;
    private InventoryManager theInven;

    public bool batteryInLight;
    public bool batteryInInven;
    public bool batteryInKeypad;

    public GameObject keypadBoxCollider;
    public GameObject lightBoxCollider;

    //battery have bool
    //battery in keypad bool
    //keypad password success bool

    //scene manager connect
    //dialouge connect
    //

    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DatabaseManager>();
        theInven = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
