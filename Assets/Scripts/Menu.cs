using System.Collections;
using System.Collections.Generic;
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


    public void openPopup()
    {
        UIController.enableItem(popup.gameObject);
    }

    public void closePopup()
    {
        UIController.disableItem(popup.gameObject);
    }

    public void closeGUI()
    {
        UIController.disableItem(menuCanvas.gameObject);
    }

    public void switchToScene()
    {
        UIController.switchScene(newSceneName);
    }
}
