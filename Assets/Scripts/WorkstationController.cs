using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationController : MonoBehaviour
{
    // This is a controller detailing the functions related to the workstation! The functions will be triggered by either animation events (e.g. a work animation finishing) or an interactable game object child.
    public bool isActive;
    public Animator anim;
    public GameObject ingredientPrefab;
    public int ingredientID;

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
            anim.SetBool("isActive", isActive);
            Debug.Log("Chugging away at the drink!");
        }
    }

    public void FinishWork()
    {
        if (isActive)
        {
            isActive = !isActive;
            anim.SetBool("isActive", isActive);
            Instantiate(ingredientPrefab, transform.position, Quaternion.identity);
            // TODO: do something here to add to the drink object's state instead of instantiating a new drink prefab
            Debug.Log("I've finished the drink!");
        }

    }
}
