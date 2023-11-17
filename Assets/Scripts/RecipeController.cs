using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour
{
    public Image[] recipeImages;
    public RecipeData[] recipes; //storing each of the recipedata objects

    public bool showingDefault = true;

    public GameObject[] recipePrefabs; //image prefabs with their recipeArray/RecipeData to instantiate
    
    public bool day = true; //indicates if we should be using day or night recipes

    // Update is called once per frame
    void Update()
    {
        
    }

    public RecipeData createRecipe()
    {
        int randomIndex = day ? Random.Range(0,16) : Random.Range(16, recipePrefabs.Length); //changes based on day vs night
        RecipeData data = recipePrefabs[randomIndex].GetComponent<RecipeData>();

        //take data and add to our images
        Sprite spriteToShow = showingDefault ? data.recipeSprite : data.recipeSpriteBack;
        addRecipe(data, spriteToShow);

        //return list
        return data;
    }

    public void addRecipe(RecipeData recipe, Sprite recipeSprite)
    {
        Debug.Log("adding");
        for (int i = 0; i < recipeImages.Length; i++)
        {
            if (recipeImages[i].sprite == null)
            {
                Debug.Log("null");
                recipeImages[i].sprite = recipeSprite;
                recipes[i] = recipe;
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
                recipes[i] = recipes[i+1];
            }
            else
            {
                recipeImages[i].sprite = null;
                recipes[i] = null;
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
                recipes[i] = null;
                moveImagesUp(i);
                return;
            }
        }
        
    }

    public void flipImages() 
    { 
        for (int i = 0; i < recipeImages.Length; i ++)
        {
            if (recipeImages[i].sprite == null) //if there is nothing being displayed on the image
            {
                showingDefault = !showingDefault;
                return;
            }
            Sprite spriteToShow = showingDefault ? recipes[i].recipeSpriteBack : recipes[i].recipeSprite;
            recipeImages[i].sprite = spriteToShow;//show the front
        }
        showingDefault = !showingDefault;
    }
}
