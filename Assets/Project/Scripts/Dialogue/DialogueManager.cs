using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private List<string> _sentences;
    public GameObject button;

    public bool ButtonsOff
    {
        get => _buttonsOff;
        set
        {
            button.SetActive(!value);

            _buttonsOff = value;
        }
    }

    private bool _buttonsOff;

    private GameManager gm;

    public Animator animator;
    private static readonly int Show = Animator.StringToHash("show");
    private static readonly int Hide = Animator.StringToHash("hide");

    private void Start()
    {
        _sentences = new List<string>();
        gm = GameManager.instance;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        _sentences.Clear();
        gm.inDialogue = true;

        foreach (string s in dialogue.sentences) _sentences.Add(s);

        animator.SetTrigger(Show);
        DisplaySentence();
    }

    private void DisplaySentence()
    {
        string s = _sentences[Random.Range(0, _sentences.Count - 1)];

        StopAllCoroutines();
        StartCoroutine(Type(s));
    }

    IEnumerator Type(string s, bool goodbye = false)
    {
        ButtonsOff = goodbye;
        gm.inDialogue = true;
        dialogueText.text = "";
        foreach (char l in s)
        {
            dialogueText.text += l;
            yield return new WaitForSeconds(0.05f);
        }

        if (goodbye)
        {
            yield return new WaitForSeconds(1.5f);
            animator.SetTrigger(Hide);
            gm.inDialogue = false;
            _buttonsOff = false;
        }
    }

    public void EndDialogue(bool accept)
    {
        if (accept)
        {
            animator.SetTrigger(Hide);
            gm.inDialogue = false;
        }
        else
        {
            if (!gm.inDialogue) animator.SetTrigger(Show);
            string s = gm.goodbyeDialogue.sentences[Random.Range(0, gm.goodbyeDialogue.sentences.Count - 1)];
            StopAllCoroutines();
            StartCoroutine(Type(s, true));
        }
    }
}