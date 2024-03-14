using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallDialManager : MonoBehaviour
{

    //Dialogue manager. use queue to show each letter. Press X to show the whole sentence. 

    public Text dialogueText;
    private Queue<string> sentences;
    private string wholeSentence;

    public bool SmallTalking = false;
    public bool finish = false;

    public Animator animDialogueWindow;

    public AudioClip letterSound;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (finish == false && !SmallTalking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SmallTalking = true;
                DisplayNextSmallSentence();
            }
        }


        if(finish == false && SmallTalking && Input.GetKeyDown(KeyCode.X))
        {
            DisplayWholeSentence();
        }
    }

    public void StartSmallDialogue(SmallDialogue dialogue)
    {
        SmallTalking = true;
        finish = false;
        sentences.Clear();

        animDialogueWindow.SetBool("SmallAppear", true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSmallSentence();
    }

    public void DisplayNextSmallSentence()
    {

        if (sentences.Count == 0)
        {
            EndSmallDialogue();
            return;
        }
        wholeSentence = string.Join("", sentences.ToArray()[0]);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSmallSentence(sentence));
    }

    IEnumerator TypeSmallSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            // Play the letter sound effect
            if (letterSound != null)
                audioSource.PlayOneShot(letterSound);

            yield return new WaitForSeconds(0.05f);
        }

        if (SmallTalking)
        {
            SmallTalking = false;
        }
    }

    // Function to display the whole sentence instantly
    void DisplayWholeSentence()
    {
        StopAllCoroutines();

        dialogueText.text = wholeSentence;

        SmallTalking = false;
    }

    void EndSmallDialogue()
    {
        animDialogueWindow.SetBool("SmallAppear", false);
        SmallTalking = false;
        finish = true;
    }


}
