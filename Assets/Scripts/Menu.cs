using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 This Menu Component should be reproducable for most menus (main menu, 
pause menu, etc.)

 */
public class Menu : MonoBehaviour
{

    public Canvas popup;

    public Canvas menuCanvas;

    public string newSceneName;

    public TextMeshProUGUI thisButton;

    public bool changeNameAfterClicked;

    public bool clicked = false;

    public string clickedname;

    public string unclickedname;


    public void openClosePopup()
    {
        clicked = !clicked;

        if (!clicked) //if button is 'unclicked'
        {
            UIController.disableItem(popup.gameObject);
            if (changeNameAfterClicked) 
            {
                changeButtonName(unclickedname);
            }
        } else //if button is 'clicked'
        {
            UIController.enableItem(popup.gameObject);
            if (changeNameAfterClicked)
            {
                changeButtonName(clickedname);
            }
        }
    }

    public void closeGUI()
    {
        UIController.disableItem(menuCanvas.gameObject);
    }

    public void changeButtonName(string newName)
    {
        thisButton.text = newName;
    }

    public void switchToScene()
    {
        UIController.switchScene(newSceneName);
    }

    public void exit()
    {
        Application.Quit();
    }
}
