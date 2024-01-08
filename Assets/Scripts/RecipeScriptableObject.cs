using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RecipeScriptableObject", order = 2)]
public class RecipeScriptableObject : ScriptableObject
{
    /**
    This is a recipe class. Use this class to create workstation objects by right-clicking in the Project file explorer in the Unity editor.
    **/
    public Recipe[] recipes;
    public RecipeScriptableObject(Recipe[] recipes) {
        this.recipes = recipes;
    }
}
