using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableData : MonoBehaviour
{
    public int tableId;
    public bool regenerate = false;

    public List<GameObject> NPCs;

    public NPCManager npcManager;

    // Update is called once per frame
    void Update()
    {
        clear();
    }

    public void addNPC(GameObject npc)
    {
        NPCs.Add(npc);
    }
    public void clear()
    {
        StartCoroutine(ClearObjects());
    }

    IEnumerator ClearObjects()
    {
        if (NPCs.Count == 0 && regenerate)
        {
            regenerate = false;
            npcManager.BeginGeneration();
        }
        
        if (NPCs.Count == 1) //only one NPC at table
        {
             if (NPCs[0].GetComponent<NPCInteraction>().orderComplete) //NPC's order is ready
             {
                GameObject npc1 = NPCs[0];
                NPCs.Remove(NPCs[0]);
                yield return new WaitForSeconds(Random.Range(2, 6));
                //leave table
                npc1.GetComponent<NPCMovement>().leave = true;
                regenerate = true;
             }

         }
         else if (NPCs.Count == 2)
         {
             if (NPCs[0].GetComponent<NPCInteraction>().orderComplete && NPCs[1].GetComponent<NPCInteraction>().orderComplete) //both NPC's orders are ready
             {
                GameObject npc1 = NPCs[0];
                GameObject npc2 = NPCs[1];
                NPCs.Remove(NPCs[0]);
                NPCs.Remove(NPCs[1]);
                yield return new WaitForSeconds(Random.Range(2, 6));
                //leave table
                npc1.GetComponent<NPCMovement>().leave = true;
                npc2.GetComponent<NPCMovement>().leave = true;
                regenerate = true;
             }

         }
    }
}
