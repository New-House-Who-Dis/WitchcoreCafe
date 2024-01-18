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

    public Canvas recipePanel;

    public Canvas optionsMenu;

    public bool regenerate;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.gameObject.SetActive(true);
        instructionPanel.gameObject.SetActive(true);
        closeDialogue.gameObject.SetActive(true);
        recipePanel.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        gameState = GameState.START;
        dialogue = "Bubble, bubble~ I hear trouble brewing! Click on the 'Ready' button when you're ready to start.";
        StartCoroutine(TypeDialogue());
    }

    public void Ready()
    {
        dialoguePanel.gameObject.SetActive(false);
        instructionPanel.gameObject.SetActive(false);
        recipePanel.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(true);
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
        npcManager.BeginGeneration();
        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
