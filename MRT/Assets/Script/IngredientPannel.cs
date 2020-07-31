using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPannel : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager = GameManager.instance;
    private UIManager uiManager = UIManager.instance;

    public GameObject ingredientPannelObject;
    public Text information;
    public Image image;
    public Button button;
    public Ingredient ingredient;
    public int buyAmount = 30;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient()
    {
        GameObject basketObject = uiManager.currentBasketObject;
        Basket basketClass = basketObject.GetComponent<Basket>();
        if(gameManager.player.money < ingredient.price * buyAmount)  // 재료값 * 구매수량 보다 보유 금액이 적을 경우 팝업
        {
            uiManager.Alert("금액이 부족합니다");
            return;
        }
        else if(gameManager.player.money >= ingredient.price * buyAmount)    // 보유 금액이 더 많을 경우 구매
        {
           gameManager.player.money -= ingredient.price * buyAmount;
        } 
        else
        {
            Debug.Log("IngredientPannel AddIngredient method error");
            //throw;
            return;
        }
        basketClass.amount = buyAmount;
        basketClass.ingredient = ingredient;
        basketClass.expiration = ingredient.expiration;
        basketClass.state = 1;
        basketObject.GetComponent<Image>().sprite = ingredient.image;
        uiManager.AddIngredientIntoBasket();
    }
}
