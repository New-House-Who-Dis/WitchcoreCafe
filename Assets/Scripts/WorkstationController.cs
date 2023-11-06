using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationController : MonoBehaviour
{
    // This is a controller detailing the functions related to the workstation! The functions will be triggered by either animation events (e.g. a work animation finishing) or an interactable game object child.
    public bool isActive;
    public Animator anim;
    public int ingredientID;
    private DrinkData drink;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    public void StartWork()
    {
        if (!isActive)
        {
            isActive = !isActive;
            anim.SetBool("isActive", true);
            Debug.Log("Making the drink!");
            string workstationName = transform.name;
            FindObjectOfType<AudioManager>().Play(workstationName);
        }
    }

    public void FinishWork()
    {
        if (isActive)
        {
            isActive = !isActive;
            anim.SetBool("isActive", false);
            DrinkController.gotCup(drink);
            DrinkController.pickupCup(drink);
            DrinkController.addStation(drink, ingredientID);
            Debug.Log("I've finished drink: " + ingredientID + "!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This will help us detect when the player is in range
        drink = collision.gameObject.GetComponent<DrinkData>();
        Debug.Log("I've selected the player's drink!");
    }
}
