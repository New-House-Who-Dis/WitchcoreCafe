using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCManager : MonoBehaviour
{
    public bool table1Occupied;
    public bool table2Occupied;
    public bool table3Occupied;

    public bool[] table1OrdersComplete;
    public bool[] table2OrdersComplete;
    public bool[] table3OrdersComplete;

    public NPCGenerator npcGenerator;

    public bool[] tables;

    // Start is called before the first frame update
    void Start()
    {
        clear(1);
        clear(2);
        clear(3);
        tables = new bool[] { table1Occupied, table2Occupied, table3Occupied };
    }

    public void BeginGeneration()
    {
        StartCoroutine(FillTables());
    }

    IEnumerator FillTables()
    {
        checkOpenTables();
        yield return new WaitForSeconds(2f);
    }
    
    public void clear(int tableNum)
    {
        if (tableNum == 1)
        {
            table1OrdersComplete = new bool[] { false, false };
            table1Occupied = false;
        }
        else if (tableNum == 2)
        {
            table2OrdersComplete = new bool[] { false, false };
            table2Occupied = false;
        }
        else if (tableNum == 3)
        {
            table3OrdersComplete = new bool[] { false, false };
            table3Occupied = false;
        }
    }

    public void updateTableOccupied(int tableNum)
    {
        if (tableNum == 1)
        {
            table1Occupied = true;
        }
        else if (tableNum == 2)
        {
            table2Occupied = true;
        }
        else if (tableNum == 3)
        {
            table3Occupied = true;
        }
    }
    public void updateTableOrderStatus(int tableNum)
    { 
        if (tableNum == 1)
        {
            if (table1OrdersComplete[0] == true)
            {
                table1OrdersComplete[1] = true;
            } else
            {
                table1OrdersComplete[0] = true;
            }
        } else if (tableNum == 2)
        {
            if (table2OrdersComplete[0] == true)
            {
                table2OrdersComplete[1] = true;
            }
            else
            {
                table2OrdersComplete[0] = true;
            }
        } else if (tableNum == 3)
        {
            if (table3OrdersComplete[0] == true)
            {
                table3OrdersComplete[1] = true;
            }
            else
            {
                table3OrdersComplete[0] = true;
            }
        }
    }

    public void checkOpenTables()
    {
        for (int i = 0; i < tables.Length; i++)
        {
            if (!tables[i]) //if table is not occupied
            {
                npcGenerator.generateNPC( Random.Range(1, 3), i + 1); //generate random number between 1 and 2 NPCs to that table
                updateTableOccupied(i + 1);
            }
        }
    }
}
