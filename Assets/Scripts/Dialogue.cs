using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Dialogue nextDialogueA;
    public Dialogue nextDialogueB;
    public string dialogueName;
    public bool winOver;
    public int staminaCost;
    public AlienType alienType;

    public void PlayDialogue()
    {
        DialogueManager.instance.rpgTalk.NewTalk(dialogueName+"-begin", dialogueName + "-end", DialogueManager.instance.rpgTalk.txtToParse, DialogueManager.instance, "DialogueEnded");
    }

    private void OnDrawGizmos()
    {
        if (nextDialogueA!=null) Gizmos.DrawLine(transform.position, nextDialogueA.transform.position);
        if (nextDialogueB != null) Gizmos.DrawLine(transform.position, nextDialogueB.transform.position);
    }
}
