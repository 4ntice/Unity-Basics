using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollingText : MonoBehaviour
{
    public string textToDisplay;

    public float timePerCharacter = 0.1f;
    float dTimer = 0;
    int currentCharacter = 0;

    public Canvas canvas;
    public TMP_Text textField;
    public GameObject textBox;

    public AudioSource ad;
    public AudioClip clip;
    //bool isWriting = false;

    void OnEnable()
    {
        //if (isWriting)
        //{
        //    return;
        //}
        //Debug.Log("startText");
        //isWriting = true;
        textField.text = null;
        currentCharacter = 0;
        //canvas.enabled = true;
    }

    void Update()
    {
        if(currentCharacter > textToDisplay.Length - 1) // stopp when finnished
        {
            //Debug.Log("stopText");
            ////isWriting = false ;
            ////canvas.enabled = false ;
            //this.enabled = false;
            return;
        }

        dTimer += Time.deltaTime;
        if(dTimer < timePerCharacter) //wait until time passed
        {
            return;
        }

        char letterToDisplay = textToDisplay[currentCharacter]; //select single letter 
        currentCharacter++;

        if (letterToDisplay != ' ') // exclude space
        {
            ad.PlayOneShot(clip);
            dTimer = 0.0f;
        }

        textField.text += letterToDisplay;
    }
}
