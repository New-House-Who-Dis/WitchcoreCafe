using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DrinkController 
{

    public static void gotCup(DrinkData drink)
    {
        drink.hasCup = true;
    }
    public static bool addStation(DrinkData drink, int num)
    {
        if (drink.hasCup && !drink.atWork)
        {
            drink.workstations.Add(num);
            drink.atWork = true;
            return true;
        }
        return false;
    }

    public static void pickupCup(DrinkData drink)
    {
        drink.atWork = false;
    }

    public static bool compareData(DrinkData drink, ArrayList requiredStations)
    {
        drink.workstations.Sort();
        requiredStations.Sort();
        return drink.workstations == requiredStations;
    }
    
    public static void clearData(DrinkData drink)
    {
        drink.atWork = false;
        drink.hasCup = false;
        drink.workstations = new ArrayList();
    }
}
