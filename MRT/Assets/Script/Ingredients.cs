using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Ingredients
{
    public int people=0;
    public int price;
    public Sprite img;
    public string ingName;
    public int expiration;
    public GameObject ingUI;
    public IngPan ingPan;

    public static Ingredients defaultIng = new Ingredients();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
