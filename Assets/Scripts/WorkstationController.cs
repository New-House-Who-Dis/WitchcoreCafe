using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationController : MonoBehaviour
{
    // This is a controller detailing the functions related to the workstation! The functions will be triggered by either animation events (e.g. a work animation finishing) or an interactable game object child.
    public bool isActive;
    private Animator anim;
    public int ingredientID;
    private DrinkData drink;
    public WorkstationScriptableObject workstationConstant;
    public TooltipScriptableObject tooltipConstant;
    public GameObject toolTipPrefab;
    public GameObject collidedPlayer;
    public HUDController hudController;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        anim = GetComponent<Animator>();
        hudController = GameObject.Find("HUD").GetComponent<HUDController>();
    }

    public void StartWork()
    {
        if (collidedPlayer != null) {
            drink = collidedPlayer.GetComponent<DrinkData>();

            //TODO: wonder if we can just check if no cup or isActive 
            /*ex.
             if (isActive)
            {
                some message about station being used
                empty return
            }

            if (!hasCup)
            {
                Debug.Log("I don't have a cup!");
                // TODO: play "nocup" sound effect
                empty return
            }

            isActive = !isActive;
            anim.SetBool("isActive", true);
            string workstationName = transform.name;
            FindObjectOfType<AudioManager>().Play(workstationName);

             */
            if (!isActive && (DrinkController.hasCup(drink) || ingredientID == 0) && gameObject.name != "Trashcan")
            {
                isActive = !isActive;
                anim.SetBool("isActive", true);
                collidedPlayer.GetComponent<PlayerMovement>().updateBusy(true);
                string workstationName = transform.name;
                FindObjectOfType<AudioManager>().Play(workstationName);
            } else if (!isActive && DrinkController.hasCup(drink) && gameObject.name == "Trashcan") {
                isActive = !isActive;
                anim.SetBool("isActive", true);
                collidedPlayer.GetComponent<PlayerMovement>().updateBusy(true);
                string workstationName = transform.name;
                FindObjectOfType<AudioManager>().Play(workstationName);
            } else {
                Debug.Log("I don't have a cup!");
                // TODO: play "nocup" sound effect
            }
        }
    }

    public void FinishWork()
    {
        if (isActive)
        {
            isActive = !isActive;
            anim.SetBool("isActive", false);
            collidedPlayer.GetComponent<PlayerMovement>().updateBusy(false); //TODO: see if I wanna change how this is designed
            if (gameObject.name == "Trashcan") {
                DrinkController.clearData(drink);
                hudController.RemoveAllIngredients(drink.currentPlayer);
            } else if (ingredientID > 0) {
                // If it's not a cup dispenser station
                DrinkController.pickupCup(drink);
                DrinkController.addStation(drink, ingredientID);
                hudController.AddIngredient(drink.currentPlayer, workstationConstant.workstations[ingredientID].ingredientSprite);
            } else {
                // Make a new drink
                DrinkController.gotCup(drink);
                DrinkController.pickupCup(drink);
            }
            collidedPlayer = null;
        }
    }

    public void TriggerToolTip()
    {
        GameObject newToolTip = Instantiate(toolTipPrefab, transform.position + workstationConstant.workstations[ingredientID].toolTipOffset, Quaternion.identity);

        for (int i = 0; i < tooltipConstant.tooltips.Length; i++)
        {
            if (tooltipConstant.tooltips[i].player == collidedPlayer.GetComponent<DrinkData>().currentPlayer)
            {
                newToolTip.GetComponent<SpriteRenderer>().sprite = tooltipConstant.tooltips[i].workstationTooltip;
                break;
            }
        }
                
        newToolTip.transform.Find("TooltipAsset").GetComponent<SpriteRenderer>().sprite = workstationConstant.workstations[ingredientID].ingredientSprite;

        newToolTip.transform.parent = transform;
        newToolTip.name = "ToolTip";
    }
    
    public void DestroyToolTip()
    {
        Destroy(transform.Find("ToolTip").gameObject);
    }
}
