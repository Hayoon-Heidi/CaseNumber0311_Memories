using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{

    //get db
    //add heart when it start
    //transfer to scene2 when character hits transfer point.


    private DatabaseManager theDB;
    private InventoryManager theInven;
    public int TransferScene;
    private bool enterTransfer = false;

    private MovingObject theMoving;


    // Start is called before the first frame update
    void Start()
    {
        theDB = FindObjectOfType<DatabaseManager>();
        theInven = FindObjectOfType<InventoryManager>();
        theMoving = FindObjectOfType<MovingObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enterTransfer)
        {
            FindObjectOfType<SceneChanger>().FadeToScene(TransferScene);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterTransfer = true;
    }
}
