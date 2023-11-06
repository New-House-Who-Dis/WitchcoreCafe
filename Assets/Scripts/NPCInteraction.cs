using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool takenOrder = false;
    public bool inRange;
    public bool orderComplete = false;
    public Sprite recipeSprite;

    public GameObject collidedPlayer;

    public List<int> order;

    public NPCManager npcManager;
    public RecipeController rController;
    public NPCMovement npcMovement;

    public int tableNum;

    // Start is called before the first frame update
    void Start()
    {
        takenOrder = false;
        npcMovement = gameObject.GetComponent<NPCMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (takenOrder) //if we have already displayed order on queue
                {
                    Debug.Log("Step 1");
                    DrinkData playerDrink = collidedPlayer.GetComponent<DrinkData>();
                    if (DrinkController.compareData(playerDrink, order)) //if Player delivers correct order
                    {
                        Debug.Log("Correct order");
                        Debug.Log("CLEARING");
                        //updates order status to complete and player no longer has drink
                        orderComplete = true;
                        DrinkController.clearData(playerDrink);
                        rController.removeSprite(recipeSprite);
                        npcManager.clear(tableNum);
                    }
                    else
                    {
                        //display the angry animation
                        Debug.Log("Incorrect order");
                    }
                }
                else //if we have not displayed order on queue
                {
                    //display order/recipe to the queue
                    RecipeData order = rController.createRecipe();
                    gameObject.GetComponent<NPCInteraction>().setOrder(order.recipeList);
                    recipeSprite = order.recipeSprite;
                    takenOrder = true;
                }
            }
        }
    }

    public void setTableNum(int tnum)
    {
        tableNum = tnum;
    }

    public void setOrder(List<int> newOrder)
    {
        order = newOrder;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hi player");
            inRange = true;
            collidedPlayer = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
