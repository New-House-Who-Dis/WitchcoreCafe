using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { START, NEWSTAGE, END}
public class GameStateSystem : MonoBehaviour
{
    public NPCManager npcManager;

    public GameState gameState;
    public Canvas instructionPanel;

    public Canvas dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button closeDialogue;
    public string dialogue;

    public bool regenerate;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.enabled = false;
        instructionPanel.enabled = false;
        closeDialogue.enabled = false;
        gameState = GameState.START;
        dialogue = "Bubble, bubble~ I hear trouble brewing! Click on the 'Ready' button when you're ready to start.";
        StartCoroutine(TypeDialogue());
    }

    public void Ready()
    {
        dialoguePanel.enabled = false;
        instructionPanel.enabled = false;
        StartCoroutine(SetupGame());
    }

    IEnumerator TypeDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.enabled = true;
        instructionPanel.enabled = true;
        foreach (char c in dialogue)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        closeDialogue.enabled = true;

    }

    IEnumerator SetupGame()
    {
        regenerate = false;
        Debug.Log("meow");
        npcManager.BeginGeneration();
        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
