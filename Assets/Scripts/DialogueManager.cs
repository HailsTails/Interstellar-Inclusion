using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public RPGTalk rpgTalk;

    public static DialogueManager instance = null;
    public Dialogue currentDialogue;
    public PlayerController player;
    private Dialogue nextDialogue;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
         instance.rpgTalk.OnMadeChoice += OnMadeChoice;
    }

    public void initiateConversation()
    {
        currentDialogue.PlayDialogue();
    }

    public void DialogueEnded()
    {
        Debug.Log("ended dialogue");
        player.useEnergy(currentDialogue.staminaCost);
        
        if (nextDialogue == null)
        {
            DialogueManager.instance.EndOfDialogueTree();
        } else {
            currentDialogue = nextDialogue;
            nextDialogue = null;
            currentDialogue.PlayDialogue();
        }
    }

    void OnMadeChoice(int questionId, int choiceID)
    {
        if (choiceID == 0)
        {
            nextDialogue = currentDialogue.nextDialogueA;
        } else if (choiceID == 1)
        {
            nextDialogue = currentDialogue.nextDialogueB;
        }
    }

    public void EndOfDialogueTree()
    {
        if (currentDialogue.alienType == AlienType.X)
        {
            GameManager.instance.AlienX.wonOver = currentDialogue.winOver;
            GameManager.instance.AlienX.conversed = true;
        }
        else if (currentDialogue.alienType == AlienType.Y)
        {
            GameManager.instance.AlienY.wonOver = currentDialogue.winOver;
            GameManager.instance.AlienY.conversed = true;
        }
        else if (currentDialogue.alienType == AlienType.Z)
        {
            GameManager.instance.AlienZ.wonOver = currentDialogue.winOver;
            GameManager.instance.AlienZ.conversed = true;
        }
        else if (currentDialogue.alienType == AlienType.V) 
        {
            GameManager.instance.AlienYV.wonOver = currentDialogue.winOver;
            GameManager.instance.AlienYV.conversed = true;
        }

        if (currentDialogue.winOver)
        {
            GameManager.instance.wonOverCount++;
        }
    }
}
