using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private GameManager gameManager = GameManager.instance;
    private GameObject ingredientPreFab;
    

    public Text money;
    public Text customer;
    public Text time;

    public GameObject ingredientSelctionPannel;
    public GameObject basketsPannel;
    public GameObject currentPannel;
    public GameObject alertPannel;

    public GameObject currentBasketObject;

    public static GameObject basketPreFab;
    public static Sprite defaultBasketImage;

    //public static List<Basket> baskets;
    public List<Ingredient> ingredients;
    public List<GameObject> uiStack = new List<GameObject>();
    
    public Slider[] souceSliders;

    void Start()
    {
        basketPreFab = Resources.Load<GameObject>("Prefab/Basket");
        defaultBasketImage = Resources.Load<Sprite>("UI/버튼");
        ingredientPreFab = Resources.Load<GameObject>("Prefab/ingredientPreFab");
        instance = this;

        // 재료창 동적 생성
        foreach(Ingredient ingredient in instance.ingredients)
        {
            GameObject ingredientPannelObject = Instantiate(ingredientPreFab);
            IngredientPannel ingredientPannel = ingredientPannelObject.GetComponent<IngredientPannel>();
            ingredientPannel.ingredient = ingredient;
            ingredientPannel.information.text = ingredient.ingredientName + "\n" + ingredient.expiration.ToString() + " 시간\n" + ingredient.price.ToString() + " 원\n" + ingredient.people.ToString() + " 명";
            ingredientPannel.image.sprite = ingredient.image; 
            
            RectTransform ingredientTransform = ingredientPannel.GetComponent<RectTransform>();
            //Debug.Log(instance.ingredientSelctionPannel.transform.GetChild(0)); // 디버그
            ingredientTransform.SetParent(instance.ingredientSelctionPannel.transform.GetChild(0).transform);
            ingredientTransform.localScale = Vector2.one;
        }
    }

    // Update is called once per frame
    void Update()
    {
        money.text = gameManager.player.money.ToString() + " 원";
        customer.text = gameManager.player.peoplePTime.ToString() + " 명";
        time.text = gameManager.player.time.ToString() + " 시간";

        GameManager.instance.player.peoplePTime = (int)(GameManager.instance.player.ingPeople * GameManager.instance.player.playerSouce.cmpSouce(GameManager.instance.gameManagerSouce));
        baskets = GameManager.instance.baskets;

        foreach (Basket basket in gameManager.baskets)
        {
            if (basket.state == 1)
            {
                basket.inform.text = basket.ing.ingName + "\n유통기한 : " + basket.expiration.ToString() + "\n수량 : " + basket.amount.ToString();
            }
        }
    }
    
    // method for open a pannel. using stack
    public void OpenPannel(GameObject targetPannel)
    {
        int uiStackLength = uiStack.Count;
        
        if(uiStackLength == 2)
        {
            uiStack[1].SetActive(true);
            
        }
        else if(uiStackLength > 2)
        {
            GameObject temp = uiStack[uiStackLength - 1];   // set BackgroundPannel as a top
            uiStack[uiStackLength - 1] = uiStack[uiStackLength - 2];
            uiStack[uiStackLength - 2] = temp;
            uiStack[uiStackLength - 1].transform.SetSiblingIndex(uiStack[uiStackLength - 2].transform.GetSiblingIndex());
        }
        else
        {
            Debug.Log("UIManager OpenPannel method error");
            //throw;
        }
        uiStack.Add(targetPannel);
        uiStack[uiStackLength - 1].transform.SetParent(targetPannel.transform.parent.transform, false);
        
        uiStack[uiStackLength].transform.SetAsLastSibling();
        uiStack[uiStackLength - 1].transform.SetSiblingIndex(targetPannel.transform.GetSiblingIndex() - 1); // move BackgroundPannel to the behind of top pannel
        targetPannel.SetActive(true);
    }

    public void ChangePannel(GameObject targetPannel)
    {
        currentPannel.SetActive(false);
        currentPannel = targetPannel;
        currentPannel.SetActive(true);

    }
    // method for close a pannel. using stack
    public void ClosePannel()
    {
        int uiStackLength = uiStack.Count;
        
        uiStack[uiStackLength - 2].transform.SetParent(uiStack[uiStackLength - 3].transform.parent.transform, false);
        uiStack[uiStackLength - 2].transform.SetSiblingIndex(uiStack[uiStackLength - 3].transform.GetSiblingIndex()); // move BackgroundPannel to the behind of top pannel
        uiStack[uiStackLength - 1].SetActive(false);
        uiStack.RemoveAt(uiStackLength - 1);

        if(uiStackLength == 3)  // when only one pannel is opened
        {
            uiStack[1].SetActive(false);    // disable BackgrounPannel
            uiStack[1].transform.SetAsLastSibling();
        }
        else if(uiStackLength > 3)  // when more than one pannel is opened
        {
            GameObject temp = uiStack[uiStackLength - 3];
            uiStack[uiStackLength - 3] = uiStack[uiStackLength - 2];
            uiStack[uiStackLength - 2] = temp;
        }
        else
        {
            Debug.Log("UIManager ClosePannel method error");
            //throw;
        }

        
    }

    public void SouceValChange(int souce)
    {
        Debug.Log(souce+"번째값 변경");
        GameManager.instance.player.playerSouce.souces[souce] = (int)souceSliders[souce].value;
        Debug.Log("플레이어 소스 : "+GameManager.instance.player.playerSouce.souces[souce]);
        Debug.Log("슬라이더 소스 : "+(int)souceSliders[souce].value);
    }

    public void AddBasket(GameObject basketObject)
    {
        Basket basketClass = basketObject.GetComponent<Basket>();

        if (basketClass.state == 0)
        {
            if(gameManager.player.money < gameManager.baskets.Count * 100)
            {
                Alert("돈이 부족합니다.");
                return;
            }
        
            gameManager.player.money -= gameManager.baskets.Count * 100;
            gameManager.baskets.Add(basketClass.GetComponent<Basket>());

            basketClass.Init();
            GameObject newBasket = Instantiate(basketPreFab);
            RectTransform basketTransform = newBasket.GetComponent<RectTransform>();
            basketTransform.SetParent(instance.basketsPannel.transform);
            basketTransform.localScale = Vector2.one;
        }
        else if(basketClass.state == -1 || basketClass.state == 1)
        {
            instance.currentBasketObject = basketObject;
            OpenPannel(ingredientSelctionPannel);
        }
    }
    
    public void Alert(string alertMessage)
    {
        alertPannel.GetComponentInChildren<Text>().text = alertMessage;
        OpenPannel(alertPannel);
    }

    public void AddIngredientIntoBasket(){
        ClosePannel();
    }
}