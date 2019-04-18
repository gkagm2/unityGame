using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

//직렬화
[System.Serializable]
public class Ingredient : System.Object
{
    public string name = "";
    public int amount = 1;
    public IngredientUnit unit = IngredientUnit.Cup;
}

public class Recipe : MonoBehaviour {
    public Ingredient potionResult;
    public Ingredient[] potionIngredients;
    private void Update()
    {
        
    }

}
