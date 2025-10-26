using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{

    public GameObject textBox;

    [SerializeField] public string textToSpeak;
    [SerializeField] public int currentTextLength;
    [SerializeField] public int textLength;
    [SerializeField] public GameObject mainTextObject;


    public GameObject exitButton;  


    private bool isTalking = false;
    private bool skipText = false;
    private bool textRunning = false;

    // ✅ NEW: Track when player presses next button
    public bool nextPressed = false;

    public void Update()
    {
        textLength = TextCreator.charCount;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipText = true;
        }
    }

    public void SetTalking(GameObject character, AudioSource audioSource, bool isTalking)
    {
        if (isTalking)
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }


    public IEnumerator NormalDisplayText()
    {
        skipText = false;
        textRunning = true;
        nextPressed = false; // ✅ Reset flag

        textBox.GetComponent<TMP_Text>().text = "";
        for (int i = 0; i < textToSpeak.Length; i++)
        {
            if (skipText)
            {
                textBox.GetComponent<TMP_Text>().text = textToSpeak;
                break;
            }

            textBox.GetComponent<TMP_Text>().text += textToSpeak[i];
            yield return new WaitForSeconds(0.05f);
        }

        textBox.GetComponent<TMP_Text>().text = textToSpeak;
        textRunning = false;
    }

    // ✅ Call this from your Next button's onClick event
    public void OnNextPressed()
    {
        nextPressed = true;
    }
    

    public void ExitDialog()
    {
        StartCoroutine(ExitButton());
    }

    public IEnumerator ExitButton()
    {

        yield return new WaitForSeconds(0.05f);

        mainTextObject.SetActive(false);
        exitButton.SetActive(false);

    }
}



