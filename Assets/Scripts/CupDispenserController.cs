using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupDispenserController : MonoBehaviour
{
    // This is a controller detailing the functions related to the workstation! The functions will be triggered by either animation events (e.g. a work animation finishing) or an interactable game object child.
    public bool isActive;
    public Animator anim;
    public DrinkData drink;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This will help us detect when the player is in range
        drink = collision.gameObject.GetComponent<DrinkData>();
    }

    public void StartWork()
    {
        if (!isActive)
        {
            isActive = !isActive;
            anim.SetBool("isActive", isActive);
            Debug.Log("Making the drink!");
        }
    }

    public void FinishWork()
    {
        if (isActive)
        {
            isActive = !isActive;
            anim.SetBool("isActive", isActive);
            // TODO: create new drink here?
            Debug.Log("I've created a new drink!");
        }

    }
}
