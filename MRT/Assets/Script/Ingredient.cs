using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Ingredient
{
    public Sprite image;
    public string ingredientName;
    public int people=0;
    public int price;
    public int expiration;
    public static Ingredient defaultIngredient = new Ingredient();
}
