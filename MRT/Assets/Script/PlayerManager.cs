using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public int money = 100;
    public int peoplePTime = 0;
    
    public int time = 0;
    public List<Basket> baskets = new List<Basket>();
    
    public Souce playerSouce = new Souce();
    
    public int ingredientPeople = 0;   // 소스의 배합으로 인한 보정
}
