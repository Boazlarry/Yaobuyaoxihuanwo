using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    public int money = 100;
    public int peoplePTime = 0;
    
    public int time = 0;
    public List<Bucket> buckets = new List<Bucket>();
    
    public Souce playerSouce = new Souce();
    
    public int ingPeople = 0;
}
