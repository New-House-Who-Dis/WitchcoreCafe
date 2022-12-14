using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // This is a class that you can attach as a child to any game object to make it interactable!

    bool inRange; // So we can know when the player is in range, so they can interact
    public KeyCode interactKey; // Allow us to select which key the player will use to interact with a given item
    public UnityEvent interaction; // The event detailing the interaction
    public GameObject uiPopup; // This is the little instructional popup
    private GameObject _canvas;
    public InteractionPrompt interactionPrompt;

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        _canvas = GameObject.Find("Canvas");
        interactionPrompt = _canvas.GetComponent<InteractionPrompt>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                // This will trigger the interaction event every time the chosen key is pressed when the player is in range of the object
                interaction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        // This will help us detect when the player is in range
        Debug.Log("Something just hit the workstation!");
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = !inRange;
            Debug.Log("Cat into " + transform.parent.name + ", meow");
            if (!interactionPrompt.isDisplayed)
            {
                interactionPrompt.Setup("Press E to interact!");
            }
            // TODO: make this able to discriminate between player 1 and player 2 so we can accurately display the right controls
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
