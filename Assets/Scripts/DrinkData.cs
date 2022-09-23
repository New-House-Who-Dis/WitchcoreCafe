using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkData : MonoBehaviour
{
    public bool atWork;
    public bool hasCup;
    public List<int> workstations;

    // Start is called before the first frame update
    void Start()
    {
        atWork = false;
        hasCup = false;
        workstations = new List<int>();
    }
}
