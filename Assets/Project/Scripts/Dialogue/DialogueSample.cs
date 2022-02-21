using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSample : MonoBehaviour
{
    public Dialogue sample;
    public DialogueManager dm;

    public void StartDialogue() => dm.StartDialogue(sample);
    public void EndDialogue() => dm.EndDialogue();
}