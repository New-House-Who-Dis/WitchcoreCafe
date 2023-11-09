using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour
{
    public Image[] recipeImages;
    public Image[] recipeImagesBack;

    public GameObject[] recipePrefabs; //image prefabs with their recipeArray/RecipeData to instantiate


    // Update is called once per frame
    void Update()
    {
        
    }

    public RecipeData createRecipe()
    {
        int randomIndex = Random.Range(0,recipePrefabs.Length);
        RecipeData data = recipePrefabs[randomIndex].GetComponent<RecipeData>();

        //take data and add to our images
        addImage(data.recipeSprite);

        //return list
        return data;
    }

    public void addImage(Sprite recipeSprite)
    {
        Debug.Log("adding");
        for (int i = 0; i < recipeImages.Length; i++)
        {
            if (recipeImages[i].sprite == null)
            {
                Debug.Log("null");
                recipeImages[i].sprite = recipeSprite;
                return;
            }
        }
    }
    public void moveImagesUp(int index)
    {
        for (int i = index; i < recipeImages.Length - 1; i++)
        {
            if (!Object.ReferenceEquals(recipeImages[i+1].sprite, null)) //if next image is not empty
            {
                recipeImages[i].sprite = recipeImages[i + 1].sprite; //moving the bottom image to the top image
            }
            else
            {
                recipeImages[i].sprite = null;
                return;
            }
            recipeImages[recipeImages.Length - 1].sprite = null;
        }
    }

    public Sprite returnSprite(GameObject go)
    {
        return go.GetComponent<Sprite>();
    }

    public void removeSprite(Sprite sprite)
    {
        for (int i = 0; i < recipeImages.Length; i++)
        {
            if (recipeImages[i].sprite == sprite)
            {
                recipeImages[i].sprite = null;
                moveImagesUp(i);
                return;
            }
        }
        
    }
}
