using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxController : MonoBehaviour
{
    public TooltipScriptableObject tooltipConstant;
    public Vector3 toolTipOffset;
    public GameObject toolTipPrefab;
    public GameObject collidedPlayer;

    public void Toggle()
    {
        FindObjectOfType<AudioManager>().Play("JukeBox");
        if (FindObjectOfType<AudioManager>().isPlaying("BGM"))
        {
            FindObjectOfType<AudioManager>().Pause("BGM");
        } else
        {
            FindObjectOfType<AudioManager>().Play("BGM");
        }
    }

    public void TriggerToolTip()
    {
        GameObject newToolTip = Instantiate(toolTipPrefab, transform.position + toolTipOffset, Quaternion.identity);

        for (int i = 0; i < tooltipConstant.tooltips.Length; i++)
        {
            if (tooltipConstant.tooltips[i].player == collidedPlayer.GetComponent<DrinkData>().currentPlayer)
            {
                newToolTip.GetComponent<SpriteRenderer>().sprite = tooltipConstant.tooltips[i].workstationTooltip;
                break;
            }
        }

        newToolTip.transform.parent = transform;
        newToolTip.name = "ToolTip";
    }

    public void DestroyToolTip()
    {
        Destroy(transform.Find("ToolTip").gameObject);
    }
}
