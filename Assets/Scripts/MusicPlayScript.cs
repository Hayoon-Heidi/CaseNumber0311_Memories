using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayScript : MonoBehaviour
{

    //Scene2, Scene3 in game music play event. 

    public AudioClip playSound;
    private AudioSource audioSource;

    private MovingObject theMoving;
    private InventoryManager theInven;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    private bool flag;
    private bool movingInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        movingInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movingInRange = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        theMoving = FindObjectOfType<MovingObject>();
        theInven = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingInRange && !flag && Input.GetKeyDown(KeyCode.Z) && !theInven.inventoryActive && theInven.startOtherEvent && !audioSource.isPlaying)
        {

            if (up && theMoving.animator.GetFloat("DirY") == 1f)
            {
                audioSource.PlayOneShot(playSound);
            }
            else if (down && theMoving.animator.GetFloat("DirY") == -1f)
            {
                audioSource.PlayOneShot(playSound);
            }
            else if (right && theMoving.animator.GetFloat("DirX") == 1f)
            {
                audioSource.PlayOneShot(playSound);
            }
            else if (left && theMoving.animator.GetFloat("DirX") == -1f)
            {
                audioSource.PlayOneShot(playSound);
            }
        }
    }
}
