using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    // This is a class that you can attach as a child to any game object to make it interactable!

    bool inRange; // So we can know when the player is in range, so they can interact
    public PlayerKey[] interactionKeys; // The event detailing the interaction
    public UnityEvent interaction; // Event when interacting
    public UnityEvent enterInteraction; // Event when entering collider
    public UnityEvent exitInteraction; // Event when exiting collider
    private GameObject parent;
    public GameObject collidedPlayer;

    [System.Serializable]
    public struct PlayerKey
    {
        public Player player;
        public KeyCode key;
    }

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        parent = transform.parent.gameObject;
        collidedPlayer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange)
        {
            for (int i = 0; i < interactionKeys.Length; i++)
            {
                // collidedPlayer is assumed to have DrinkData
                if (Input.GetKeyDown(interactionKeys[i].key) 
                    && (collidedPlayer.GetComponent<DrinkData>().currentPlayer == interactionKeys[i].player))
                {
                    interaction.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // This will help us detect when the player is in range
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = !inRange;
            collidedPlayer = collision.gameObject;

            // Tell parent which player has entered its range
            if (parent.CompareTag("Workstation")) 
            {
                WorkstationController parentWorkstation = parent.GetComponent<WorkstationController>();

                parentWorkstation.collidedPlayer = collision.gameObject;
            } else if (parent.CompareTag("NPC")) 
            {
                NPCInteraction parentNPC = parent.GetComponent<NPCInteraction>();

                parentNPC.collidedPlayer = collision.gameObject;
            } else if (parent.CompareTag("Jukebox"))
            {
                JukeBoxController parentJukebox = parent.GetComponent<JukeBoxController>();

                parentJukebox.collidedPlayer = collision.gameObject;
            }

            if (enterInteraction != null)
            {
                enterInteraction.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // This will help us detect when the player leaves the range
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = !inRange;
            if (exitInteraction != null)
            {
                exitInteraction.Invoke();
            }
            collidedPlayer = null;
        }
    }
}
