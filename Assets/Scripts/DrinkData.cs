using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkData : MonoBehaviour
{
    public bool atWork;
    public bool hasCup;
    public List<int> workstations;
    public WorkstationScriptableObject workstationConstant;
    public Player currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        atWork = false;
        hasCup = false;
        workstations = new List<int>();
        RemoveDrink();
    }

    // Makes player hold drink (visually)
    public void HoldDrink() {
        transform.Find("Drink").GetComponent<SpriteRenderer>().sprite = workstationConstant.workstations[0].ingredientSprite;
    }  

    // Makes player remove drink (visually)
    public void RemoveDrink() {
        transform.Find("Drink").GetComponent<SpriteRenderer>().sprite = null;
    }
}
