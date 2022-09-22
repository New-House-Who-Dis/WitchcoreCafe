using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
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
    public Transform[][] currentTable;

    public Transform npcSpawn;

    private void Start()
    {
        table1Paths = new Transform[][] { table1Path1, table1Path2 };
        table2Paths = new Transform[][] { table2Path1, table2Path2 };
        table3Paths = new Transform[][] { table3Path1, table3Path2 };
    }
    public void generateNPC(int num, int tableNum)
    {
        currentNum = num;
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
        StartCoroutine(Generate());
    }
    IEnumerator Generate()
    {
        int randomTableIndex = Random.Range(0, 2);
        for (int i = 0; i < currentNum; i++)
        {
            Debug.Log("NPC created");
            GameObject prefab = NPCprefabs[Random.Range(0, NPCprefabs.Length)];
            GameObject newNPC = Instantiate(prefab, npcSpawn.position, Quaternion.identity);
            newNPC.GetComponent<NPCMovement>().setPath(currentTable[randomTableIndex]);
            yield return new WaitForSeconds(1.0f);
            randomTableIndex = 1 - randomTableIndex;
        }
    }
}
