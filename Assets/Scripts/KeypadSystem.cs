using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class KeypadSystem : MonoBehaviour
{

    // Keypad input control 

    private int selectedKey; // current selected number key
    private int keyCount;
    private string inputPass; //input value
    private string password = "0529"; //password

    public AudioClip enterSound;
    public AudioClip openSound;
    public AudioClip notopenSound;
    private AudioSource audioSource;

    public Image[] keyPadsImg;

    public Animator anim;

    public bool PadActivated; //return new waituntil. 
    private bool PadInput; //key input 
    public bool PassCorrect; //is the inputPass and password are the same.
    public bool keypadFinish;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowPad()
    {
        keypadFinish = false;
        PadActivated = true;
        selectedKey = 0;
        inputPass = "";
        keyCount = 1;
        DeselectedKeyColor(11);
        anim.SetBool("ActivePad", true);
        SelectedKeyColor(selectedKey);

        PadInput = true;
    }


    public void HidePad()
    {
        PadActivated = false;
        PadInput = false;
        anim.SetBool("ActivePad", false);

        if (inputPass == password)
        {
            PassCorrect = true;
            audioSource.PlayOneShot(openSound);
        }
        else
        {
            audioSource.PlayOneShot(notopenSound);
        }

        keypadFinish = true;
    }

    public void SelectedKeyColor(int i)
    {
        Color color = Color.gray;
        keyPadsImg[i].color = color;
    }

    public void DeselectedKeyColor(int i)
    {
        Color color = Color.white;
        color.a = 0.4f;
        keyPadsImg[i].color = color;
    }

    public void AddKeyColor(int i)
    {
        Color color = Color.black;
        keyPadsImg[i].color = color;
    }

    public void moveLeft(int currentKey)
    {
        if (currentKey % 3 != 0)
        {
            DeselectedKeyColor(currentKey);
            currentKey--;
            SelectedKeyColor(currentKey);
            selectedKey = currentKey;
        }
        else
        {
            selectedKey = currentKey;
        }
    }

    public void moveRight(int currentKey)
    {
        if (currentKey%3 != 2)
        {
            DeselectedKeyColor(currentKey);
            currentKey++;
            SelectedKeyColor(currentKey);
            selectedKey = currentKey;
        }
        else
        {
            selectedKey = currentKey;
        }
    }

    public void moveUp(int currentKey)
    {
        if (currentKey > 2)
        {
            DeselectedKeyColor(currentKey);
            currentKey = currentKey - 3;
            SelectedKeyColor(currentKey);
            selectedKey = currentKey;
        }
        else
        {
            selectedKey = currentKey;
        }
    }

    public void moveDown(int currentKey)
    {
        if(currentKey < 9)
        {
            DeselectedKeyColor(currentKey);
            currentKey = currentKey + 3;
            SelectedKeyColor(currentKey);
            selectedKey = currentKey;

        } else
        {
            selectedKey = currentKey;
        }
    }

    public void addNumber(int currentKey)
    {

        AddKeyColor(currentKey);

        int tempNumber = 0;
        string tempString;


        if (currentKey < 9)
        {
            tempNumber = currentKey + 1;
            tempString = tempNumber.ToString();
            inputPass = inputPass + tempString;

        } else if(currentKey == 10)
        {
            tempNumber = 0;
            tempString = tempNumber.ToString();
            inputPass = inputPass + tempString;

        } else if (currentKey == 9)
        {
            inputPass = "";
        }

    }

    void Update()
    {
        
        if(PadInput)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDown(selectedKey);
            }else if (Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                moveUp(selectedKey);
            }else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveLeft(selectedKey);
            }else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveRight(selectedKey);
            }else if (Input.GetKeyDown(KeyCode.Z))
            {
                keyCount++;
                audioSource.PlayOneShot(enterSound);

                if (selectedKey == 11)
                {
                    HidePad();
                }
                else if(keyCount != 1)
                {

                    addNumber(selectedKey);
                }
            }

        }

    }
}
