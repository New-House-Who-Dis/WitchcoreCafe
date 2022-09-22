using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    [SerializeField] private GameObject _uiPanel;
    public bool isDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        _uiPanel.SetActive(false);
        isDisplayed = false;
    }

    public void Setup(string text)
    {
        promptText.text = text;
        _uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        _uiPanel.SetActive(false);
        isDisplayed = false;
    }
}
