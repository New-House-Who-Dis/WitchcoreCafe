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

    public static bool compareData(DrinkData drink, List<int> requiredStations)
    {
        drink.workstations.Sort();
        requiredStations.Sort();
        if (drink.workstations.Count != requiredStations.Count)
        {
            return false;
        }
        for (int i = 0; i < drink.workstations.Count; i++)
        {
            if (requiredStations[i] != drink.workstations[i])
            {
                return false;
            }
        }
        return true;
    }

    public static void clearData(DrinkData drink)
    {
        drink.atWork = false;
        drink.hasCup = false;
        drink.workstations = new List<int>();
    }
}
