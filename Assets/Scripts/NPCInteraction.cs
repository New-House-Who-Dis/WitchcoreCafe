using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool takenOrder;
    public bool inRange;
    public GameObject collidedPlayer;

    public ArrayList order;

    public NPCManager npcManager;

    public int tableNum;

    // Start is called before the first frame update
    void Start()
    {
        takenOrder = false;
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
                    DrinkData playerDrink = collidedPlayer.GetComponent<DrinkData>();
                    if (DrinkController.compareData(playerDrink, order))
                    {
                        npcManager.updateTableOrderStatus(tableNum);
                    }
                    else
                    {
                        //display the angry animation
                    }
                }
                else //if we have not displayed order on queue
                {
                    //display order/recipe to the queue
                }
            }
        }
    }

    public void setTableNum(int tnum)
    {
        tableNum = tnum;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!takenOrder)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                inRange = true;
                collidedPlayer = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        // This will help us detect when the player leaves the range
        Debug.Log("Something just left the workstation!");
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = !inRange;
            Debug.Log("Cat out of " + transform.parent.name + ", meow");
            if (interactionPrompt.isDisplayed)
            {
                interactionPrompt.Close();
            }
        }
    }
}
