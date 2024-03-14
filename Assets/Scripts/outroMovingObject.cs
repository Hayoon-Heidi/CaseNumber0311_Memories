using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outroMovingObject : MonoBehaviour
{
    // outro moving object control.
    // character should looked like they aren't change the position

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed;
    private Vector3 vector;
    public int walkCount;
    private int currentWalkCount;


    public bool notMove = false;
    private bool canMove = true;

    public Animator animator;
    public AudioSource audioSource;
    public AudioClip step;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.SetFloat("DirY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !notMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(OutroMoveCoroutine());
            }
        }
    }

    IEnumerator OutroMoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                vector.y = 0;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed), 0);
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;

        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    public void Move()
    {
        notMove = false;
    }

    public void NotMove()
    {
        notMove = true;
    }

    private void Step()
    {
        audioSource.PlayOneShot(step);
    }
}
