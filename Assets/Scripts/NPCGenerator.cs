using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class NPCGenerator : MonoBehaviour
{
    public RecipeController rController;
    public NPCManager npcManager;

    public GameObject[] NPCprefabs;

    public Transform[][] table1Paths;
    public Transform[][] table2Paths;
    public Transform[][] table3Paths;

    public Transform[] table1Path1;
    public Transform[] table1Path2;

    public Transform[] table2Path1;
    public Transform[] table2Path2;

    public Transform[] table3Path1;
    public Transform[] table3Path2;

    public int currentNum;
    public int currentTableNum;
    public Transform[][] currentTable;

    public Transform npcSpawn;

    public TableData table1;
    public TableData table2;
    public TableData table3; 

    private void Start()
    {
        table1Paths = new Transform[][] { table1Path1, table1Path2 };
        table2Paths = new Transform[][] { table2Path1, table2Path2 };
        table3Paths = new Transform[][] { table3Path1, table3Path2 };
    }
    public void generateNPC(int num, int tableNum)
    {
        currentNum = num;
        currentTableNum = tableNum;
        if (tableNum == 1)
        {
            currentTable = table1Paths;
        } else if (tableNum == 2)
        {
            currentTable = table2Paths;
        } else if (tableNum == 3)
        {
            currentTable = table3Paths;
        }
        StartCoroutine(Generate(tableNum));

    }
    IEnumerator Generate(int tableNum)
    {
        int randomTableIndex = Random.Range(0, 2);
        for (int i = 0; i < currentNum; i++) //num of NPCs 
        {
            //instantiating NPCprefab
            GameObject prefab = NPCprefabs[Random.Range(0, NPCprefabs.Length)];
            GameObject newNPC = Instantiate(prefab, npcSpawn.position, Quaternion.identity);

            //assigning path and table num
            newNPC.GetComponent<NPCMovement>().setPath(currentTable[randomTableIndex]);
            Debug.Log("hi");
            newNPC.GetComponent<NPCInteraction>().setTableNum(currentTableNum);
            newNPC.GetComponent<NPCInteraction>().npcManager = npcManager;
            newNPC.GetComponent<NPCInteraction>().rController = rController;

            randomTableIndex = 1 - randomTableIndex;
            if (tableNum == 1)
            {
                table1.addNPC(newNPC);
            }
            else if (tableNum == 2)
            {
                table2.addNPC(newNPC);
            }
            else if (tableNum == 3)
            {
                table3.addNPC(newNPC);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
