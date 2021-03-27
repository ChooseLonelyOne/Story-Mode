using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private bool isDialogue = false;

    public bool IsDialogue
    {
        get { return isDialogue; }
        private set { }
    }


    private Queue<string> sentences;
    private Queue<string> namesMan;

    private void Start()
    {
        sentences = new Queue<string>();
        namesMan = new Queue<string>();
    }

    private void Update()
    {
        if(isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
            {
                DisplayNextSentence();
                return;
            }
        }     
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogue = true;
        player.StopPlayer();

        animator.SetBool("isOpen", true);

        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach (Mans mans in dialogue.mans)
        {
            foreach (string word in mans.words)
            {
                sentences.Enqueue(word);
                namesMan.Enqueue(mans.name);
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string names = namesMan.Dequeue();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, names));
    }

    IEnumerator TypeSentence(string sentence, string name)
    {
        nameText.text = name;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    private void EndDialogue()
    {
        isDialogue = false;
        player.StopPlayer();
        Debug.Log("end of conversation");
        animator.SetBool("isOpen", false);
    }
}
