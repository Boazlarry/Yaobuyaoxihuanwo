using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Souce 
{
    public int[] souces = {0,0,0,0,0};
    
    public float CmpSouce(Souce playerSouce)
    {
        int count = 0;
        for(int i = 0; i<souces.Length;i++ )
        {
            count += Mathf.Abs(playerSouce.souces[i] - souces[i]);
        }
        return (10/Mathf.Pow(500,3))*Mathf.Pow(count,3) + (-(3.0f/50000.0f)*Mathf.Pow(count,2)) + 5;
    }

    public void SetRandom()
    {
        for(int i = 0; i < souces.Length;i++)
        {
            souces[i] = Random.Range(0,101);
        }
    } 
}
