using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class UIController
{
    public static void updateText(TextMeshPro textObject, string newText)
    {
        textObject.text = newText;
    }

    public static void enableItem(GameObject item)
    {
        item.SetActive(true);
    }

    public static void disableItem(GameObject item)
    {
        item.SetActive(false);
    }

    public static void switchScene(string name)
    { 
        SceneManager.LoadScene(name);
    }
}
