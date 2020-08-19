using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Basket : MonoBehaviour
{
    public Ingredient ingredient;
    public Text information;
    public int amount;
    public int expiration;
    public int state = 0; // 0은 basket이 바스켓 미구매 상태, 1은 찬 비스켓, -1는 빈 바스켓
    
    // Start is called before the first frame update
    void Start()
    {
        information.text = "바구니 추가\n" + (GameManager.instance.player.baskets.Count * 100).ToString() + " 원";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBasket(GameObject basketObject)
    {
        UIManager.instance.AddBasket(basketObject);
    }

    public void Init()
    {
        ingredient = Ingredient.defaultIngredient;
        amount = 0;
        expiration = 0;
        state = -1;
        this.GetComponent<Image>().sprite = UIManager.defaultBasketImage;
        this.GetComponentInChildren<Text>().text = "재고 없음\n";
    }
}
