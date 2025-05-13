using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSkipDialogue : MonoBehaviour
{
    [SerializeField]public GameObject skipButton; // Assign in Inspector
    [SerializeField] public GameObject skipButtonBG;
    [SerializeField] public ConversationManager conversationManager; // Assign in Inspector
    public bool skipActive = false;
    void Update()
    {
        if (conversationManager != null && skipButton != null)
        {
            //bool isActive = conversationManager.ConversationActive();
            //skipActive = true;
            //skipActive = conversationManager.ConversationActive();
            skipActive = conversationManager.boolSkip();
        }
        else
        {
            skipActive= false;
        }

        if (conversationManager.boolSkip())
        {
            skipButton.SetActive(true);
            if (skipButtonBG.activeSelf == false)
            {
                skipButtonBG.SetActive(true);
            }
        }
        else
        {
            turnOff();
        }

        if(skipActive)
        {
            skipButton.SetActive(true);
            if(skipButtonBG.activeSelf==false)
            {
                skipButtonBG.SetActive(true);
            }
        }
        else
        {
            turnOff();
        }
    }
    void turnOff()
    {
        skipButton.SetActive(false);
        skipButtonBG.SetActive(false);
    }
}
