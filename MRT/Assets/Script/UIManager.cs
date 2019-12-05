using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;
    public Text money;
    public Text people;

    void Start()
    {
        instance=this;
    }

    // Update is called once per frame
    void Update()
    {
        money.text = GameManager.instance.player.money.ToString();
        people.text = GameManager.instance.player.personPTime.ToString();
        
    }
}
