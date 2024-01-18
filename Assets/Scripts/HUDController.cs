using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public RectTransform catPanel;
    public RectTransform crowPanel;
    public GameObject imagePrefab;

    private float currCatPanelWidth, currCrowPanelWidth;

    // Start is called before the first frame update
    void Start()
    {
        currCatPanelWidth = 0f;
        catPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currCatPanelWidth);

        currCrowPanelWidth = 0f;
        crowPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currCrowPanelWidth);
    }

    public void AddIngredient(Player player, Sprite ingredientSprite)
    {
        switch (player)
        {
            case Player.Cat:
                currCatPanelWidth += catPanel.childCount > 0 ? 80f : 90f;

                catPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currCatPanelWidth + 10f);

                GameObject newCatIngredient = Instantiate(imagePrefab, catPanel, false);
                newCatIngredient.GetComponent<Image>().sprite = ingredientSprite;

                newCatIngredient.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, catPanel.childCount > 0 ? currCatPanelWidth - 80f : 10f, 80f);

                break;
            case Player.Crow:
                currCrowPanelWidth += crowPanel.childCount > 0 ? 80f : 90f;

                crowPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currCrowPanelWidth + 10f);

                GameObject newCrowIngredient = Instantiate(imagePrefab, crowPanel, false);
                newCrowIngredient.GetComponent<Image>().sprite = ingredientSprite;

                newCrowIngredient.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, crowPanel.childCount > 0 ? currCrowPanelWidth - 80f : 10f, 80f);

                break;
        }
    }

    public void RemoveAllIngredients(Player player)
    {
        switch (player)
        {
            case Player.Cat:
                for (int i = 0; i < catPanel.childCount; i++)
                {
                    Destroy(catPanel.GetChild(i).gameObject);
                }

                currCatPanelWidth = 0f;
                catPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currCatPanelWidth);
                break;
            case Player.Crow:
                for (int i = 0; i < crowPanel.childCount; i++)
                {
                    Destroy(crowPanel.GetChild(i).gameObject);
                }

                currCrowPanelWidth = 0f;
                crowPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currCrowPanelWidth);
                break;
        }
    }
}
