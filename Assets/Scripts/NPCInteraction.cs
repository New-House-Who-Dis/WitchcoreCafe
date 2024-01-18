using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool takenOrder = false;
    public bool inRange;
    public bool orderComplete = false;
    public GameObject collidedPlayer;
    public GameObject toolTipPrefab;
    public TooltipScriptableObject tooltipConstant;

    public Recipe order;

    public NPCManager npcManager;
    public RecipeController rController;
    public NPCMovement npcMovement;
    public HUDController hudController;

    public int tableNum;

    // Start is called before the first frame update
    void Start()
    {
        takenOrder = false;
        npcMovement = gameObject.GetComponent<NPCMovement>();
        collidedPlayer = null;
        hudController = GameObject.Find("HUD").GetComponent<HUDController>();
    }

    public void setTableNum(int tnum)
    {
        tableNum = tnum;
    }

    public void setOrder(Recipe newOrder)
    {
        order = newOrder;
    }

    public void PlayerInRange()
    {
        if (npcMovement.sitting) {
            inRange = true;

            GameObject newToolTip = Instantiate(toolTipPrefab, transform.position + new Vector3(0.5f, 1f, 0f), Quaternion.identity);

            for (int i = 0; i < tooltipConstant.tooltips.Length; i++)
            {
                if (tooltipConstant.tooltips[i].player == collidedPlayer.GetComponent<DrinkData>().currentPlayer)
                {
                    newToolTip.GetComponent<SpriteRenderer>().sprite = tooltipConstant.tooltips[i].npcTooltip;
                    break;
                }
            }

            newToolTip.transform.parent = transform;
            newToolTip.name = "ToolTip";
        }
    }

    public void PlayerNotInRange()
    {
        if (npcMovement.sitting) {
            inRange = false;
            collidedPlayer = null;
            if (transform.Find("ToolTip") != null) {
                Destroy(transform.Find("ToolTip").gameObject);
            }
        }
    }

    public void TakeOrder()
    {
        if (inRange && collidedPlayer != null) {
            Animator alertAnimator = transform.Find("Alert").GetComponent<Animator>();

            if (takenOrder) //if we have already displayed order on queue
            {
                Debug.Log("Order was taken, checking if delivery is correct");
                DrinkData playerDrink = collidedPlayer.GetComponent<DrinkData>();
                if (DrinkController.compareData(playerDrink, order.recipeList)) //if Player delivers correct order
                {
                    alertAnimator.SetBool("isWaiting", false);
                    Debug.Log("Correct order, clearing!");
                    //updates order status to complete and player no longer has drink
                    orderComplete = true;
                    DrinkController.clearData(playerDrink);

                    rController.removeSprite(rController.showingDefault ? order.recipeSprite : order.recipeSpriteBack);
                    hudController.RemoveAllIngredients(playerDrink.currentPlayer);
                    
                    npcManager.clear(tableNum);
                }
                else
                {
                    alertAnimator.SetTrigger("isRejected");
                    // TODO: Display the angry animation
                    Debug.Log("Incorrect order");
                }
            }
            else //if we have not displayed order on queue
            {
                Debug.Log("Giving order to player");
                //display order/recipe to the queue
                Recipe order = rController.createRecipe();
                setOrder(order);
                takenOrder = true;
                
                // Display waiting VFX
                alertAnimator.SetBool("isWaiting", true);
            }
        }
    }
}
