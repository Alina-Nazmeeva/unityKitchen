using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu()] this was commented out because we need it only once to create 1 RecipeListSO
public class RecipeListSO : ScriptableObject
{
    // the idea is that any script that needs to know about all of the recipes just needs a reference to the RecipeListSO
    // whenever we need to add or remove a recipe we just need this RecipeListSO
    // otherwise we need create the same list for every script and update all of them in case of changes
    public List<RecipeSO> recipeSOList; // the list of all recipes
}
