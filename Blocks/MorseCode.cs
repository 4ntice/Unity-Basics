using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MorseCode : MonoBehaviour
{
    /*
     * a   .-
     * b   -...
     * c   -.-.
     * d   -..
     * e   .
     * f   ..-.
     * g   --.
     * h   ....
     * i   ..
     * j   .---
     * k   -.-
     * l   .-..
     * m   --
     * n   -.
     * o   ---
     * p   .--.
     * q   --.-
     * r   .-.
     * s   ...
     * t   -
     * u   ..-
     * v   ...-
     * w   .--
     * x   -..-
     * y   -.--
     * z   --..
     */

    /*
     * Three Cases, Null, Dot, Line
     */

    public AudioSource ad;
    public AudioClip beep;

    public string textToTranslate;
    int currentLetterID = 0;
    float dTimer01 = 0;
    float waitTime = 0;

    public Canvas canvas;
    public TMP_Text textField;
    public GameObject textBox;
    int currentCharacter = 0;

    void OnEnable()
    {
        //if (isWriting)
        //{
        //    return;
        //}
        //Debug.Log("startText");
        //isWriting = true;
        textField.text = null;
        currentLetterID = 0;
        currentCharacter = 0;
        //canvas.enabled = true;
    }

    private void Update()
    {
        dTimer01 += Time.deltaTime;

        string morseText = "";
        foreach(char c in textToTranslate)
        {
            if(c.ToString() == " ") //keep space
            {
                morseText += " ";
            }
            else
            {
                morseText += TranslateLetterToMorse(c.ToString());
            }

            morseText += "1";

        }

        if(dTimer01 < waitTime)
        {
            return;
        }
        dTimer01 = 0;

        currentLetterID++;
        
        if(currentLetterID > morseText.Length - 1)
        {
            return;
        }

        float time = 0.15f;
        ad.Stop();

        if (morseText[currentLetterID].ToString() == "1")
        {
            textField.text += textToTranslate[currentCharacter];
            currentCharacter++;
        }

        switch (morseText[currentLetterID].ToString())
        {
            case "-":
                break;
            case ".": time = 0.03f;
                break;
            case " ":
                time = 0.06f;
                break;
            default: time = 0.18f;
                break;
        }
        ad.PlayOneShot(beep);
        waitTime = time;

    }


    public string TranslateLetterToMorse(string currentLetter)
    {
        string morse = "_";

        switch(currentLetter)
        {
            case "a":
                morse = ".--";
                break;
            case "b":
                morse = "-...";
                break;
            case "c":
                morse = "-.-.";
                break;
            case "d":
                morse = "-..";
                break;
            case "e":
                morse = ".";
                break;
            case "f":
                morse = "..-.";
                break;
            case "g":
                morse = "--.";
                break;
            case "h":
                morse = "....";
                break;
            case "i":
                morse = "..";
                break;
            case "j":
                morse = ".---";
                break;
            case "k":
                morse = "-.-";
                break;
            case "l":
                morse = ".-..";
                break;
            case "m":
                morse = "--";
                break;
            case "n":
                morse = "-.";
                break;
            case "o":
                morse = "---";
                break;
            case "p":
                morse = ".--.";
                break;
            case "q":
                morse = "--.-";
                break;
            case "r":
                morse = ".-.";
                break;
            case "s":
                morse = "...";
                break;
            case "t":
                morse = "-";
                break;
            case "u":
                morse = "..-";
                break;
            case "v":
                morse = "...-";
                break;
            case "w":
                morse = ".--";
                break;
            case "x":
                morse = "-..-";
                break;
            case "y":
                morse = "-.--";
                break;
            case "z":
                morse = "--..";
                break;
                default: morse = "_";
                break;
        }

        return morse;
    }
}
