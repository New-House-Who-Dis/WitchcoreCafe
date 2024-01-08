using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCManager : MonoBehaviour
{
    public bool table1Occupied;
    public bool table2Occupied;
    public bool table3Occupied;

    public TableData table1;
    public TableData table2;
    public TableData table3;

    public NPCGenerator npcGenerator;

    public bool[] tables;
    public TableData[] allTables;

    // Start is called before the first frame update
    void Start()
    {

        tables = new bool[] { table1Occupied, table2Occupied, table3Occupied };
        allTables = new TableData[] { table1, table2, table3 };
        table1Occupied = false;
        table2Occupied = false;
        table3Occupied = false;
        tables[0] = false;
        tables[1] = false;
        tables[2] = false;
    }

    void Update()
    {

    }
    public void BeginGeneration()
    {
        StartCoroutine(FillTables());
    }

    IEnumerator FillTables()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        for (int i = 0; i < tables.Length; i++)
        {
            if (!tables[i]) //if table is not occupied
            {
                updateTableOccupied(i + 1);
                int numOfNPCs = Random.Range(1, 3);
                npcGenerator.generateNPC(numOfNPCs, i + 1); //generate random number between 1 and 2 NPCs to that table
                yield return new WaitForSeconds(Random.Range(2,4));
            }
        }
    }

    public void clear(int tableNum)
    { 
        if (tableNum == 1)
        {
            table1Occupied = false;
        }
        else if (tableNum == 2)
        {
            table2Occupied = false;
        }
        else if (tableNum == 3)
        {
            table3Occupied = false;
        }
        tables[tableNum - 1] = false;
        FillTables();
        //TODO: figure out implementing "waves" so that player is not just stuck in a constant loop of serving customers (unless it is the intended gameplay loop)
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
        tables[tableNum - 1] = true;
    }
    /*
    public bool updateTableOrderStatus(int tableNum)
    { 
        if (tableNum == 1)
        {
            if (table1OrdersComplete[0] == true)
            {
                table1OrdersComplete[1] = true;
                return true;
            } else
            {
                table1OrdersComplete[0] = true;
            }
        } else if (tableNum == 2)
        {
            if (table2OrdersComplete[0] == true)
            {
                table2OrdersComplete[1] = true;
                return true;
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
                return true;
            }
            else
            {
                table3OrdersComplete[0] = true;
            }
        }
        return false;
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
    */
}
