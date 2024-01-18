using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour
{
    public Image[] recipeImages;
    public List<Recipe> recipes; //storing each of the recipedata objects

    public bool showingDefault = true;

    public RecipeScriptableObject recipeConstant;
    
    public bool day = true; //indicates if we should be using day or night recipes

    // Update is called once per frame
    void Update()
    {
        
    }

    public Recipe createRecipe()
    {
        int randomIndex = Random.Range(0,15);
        Recipe data = recipeConstant.recipes[randomIndex];

        //take data and add to our images
        Sprite spriteToShow = showingDefault ? data.recipeSprite : data.recipeSpriteBack;
        addRecipe(data, spriteToShow);

        //return list
        return data;
    }

    public void addRecipe(Recipe recipe, Sprite recipeSprite)
    {
        for (int i = 0; i < recipeImages.Length; i++)
        {
            if (recipeImages[i].sprite == null)
            {
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
                recipeImages[i].sprite = recipeImages[i + 1].sprite; // moving the bottom image to the top image
                // recipes[i] = recipes[i+1];
            }
            else
            {
                recipeImages[i].sprite = null;
                recipes.RemoveAt(i);
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
        // TODO: figure out a better way of doing this
        for (int i = 0; i < recipeImages.Length; i++)
        {
            Debug.Log("Checking if " + recipeImages[i].sprite.name + " is equal to " + sprite.name);
            if (recipeImages[i].sprite == sprite)
            {
                recipeImages[i].sprite = null;
                recipes.RemoveAt(i);
                moveImagesUp(i);
                return;
            }
        }
    }

    public void flipImages() 
    { 
        for (int i = 0; i < recipeImages.Length; i ++)
        {
            if (recipeImages[i].sprite == null) // if there is nothing being displayed on the image
            {
                showingDefault = !showingDefault;
                return;
            }
            Sprite spriteToShow = showingDefault ? recipes[i].recipeSpriteBack : recipes[i].recipeSprite;
            recipeImages[i].sprite = spriteToShow; // show the front
        }
        showingDefault = !showingDefault;
    }
}
