using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, Interactable {
    public bool conversed = false;
    public AlienType type;
    public bool wonOver;
    public Dialogue currentDialogueX;
    public Dialogue currentDialogueY;

    public void interact()
    {
        if (!conversed)
        {
            if (GameManager.instance.playerType == AlienType.X)
            {
                DialogueManager.instance.currentDialogue = currentDialogueX;
            } else
            {
                DialogueManager.instance.currentDialogue = currentDialogueY;
            }
            
            DialogueManager.instance.initiateConversation();
        }
    }
}
