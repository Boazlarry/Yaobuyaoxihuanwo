using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerManager
{
    public int money = 100;
    public int peoplePTime = 0;
    public int time = 0;
    public List<Basket> baskets = new List<Basket>();
    public Souce souce = new Souce();
}
