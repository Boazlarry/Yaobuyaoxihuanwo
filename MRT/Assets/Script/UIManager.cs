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
    public Text time;
    public GameObject soucePan;
    public GameObject refriPan;
    public GameObject choosePan;
    public GameObject optionPan;
    GameObject ing_PreFab;
    public Bucket currentBucket;
    public int currentUILevel = 0;

    public List<Bucket> buckets;
    public List<Ingredients> ingredients;
    public List<GameObject> UILevel = new List<GameObject>();
    
    public Slider[] souceSliders;

    void Start()
    {
        ing_PreFab = Resources.Load<GameObject>("Prefab/ingUI");
        instance=this;

        // 재료창 동적 생성
        foreach(Ingredients ingredient in ingredients){
            ingredient.ingUI = Instantiate(ing_PreFab);
            RectTransform preFab_tr = ingredient.ingUI.GetComponent<RectTransform>();
            preFab_tr.SetParent(choosePan.transform);
            preFab_tr.localScale = Vector2.one;
            
            IngPan iP = ingredient.ingUI.GetComponent<IngPan>();
            iP.ingredient = ingredient;
            ingredient.ingPan = iP;
            iP.inform.text = ingredient.ingName + "\n" + ingredient.expiration.ToString() + " 시간\n" + ingredient.price.ToString() + " 원\n" + ingredient.people.ToString() + " 명";
            iP.image.sprite = ingredient.img; 

        }
    }

    // Update is called once per frame
    void Update()
    {

        money.text = GameManager.instance.player.money.ToString() + " 원";
        people.text = GameManager.instance.player.peoplePTime.ToString() + " 명";
        time.text = GameManager.instance.player.time.ToString() + " 시간";

        GameManager.instance.player.peoplePTime = (int)(GameManager.instance.player.ingPeople * GameManager.instance.player.playerSouce.cmpSouce(GameManager.instance.gameManagerSouce));
        buckets = GameManager.instance.buckets;

        foreach (Bucket bucket in buckets)
        {
            if (bucket.state == 2)
            {
                bucket.inform.text = bucket.ing.ingName + "\n유통기한 : " + bucket.expiration.ToString() + "\n수량 : " + bucket.amount.ToString();
            }
        }
        
    }
    
    // UI에서 소스창 열고 닫을 때 사용 
    public void buttonClick(GameObject obj){
        currentUILevel = 1;
        UILevel[0].SetActive(true);
    if (obj.activeSelf) obj.SetActive(false);
    else {
        UILevel[currentUILevel-1].transform.SetSiblingIndex(UILevel[currentUILevel].transform.GetSiblingIndex()-1);
        obj.SetActive(true);
         }
    }
    
    public void buttonClickOptPan(GameObject obj){
    /*활성화된 옵션팬이 없는 경우*/
    if (!optionPan) {
        obj.SetActive(true);
        
        optionPan = obj;
    }
    /**************************/
    if (obj.Equals(optionPan)) return;
    else {
        
        obj.SetActive(true);
        optionPan.SetActive(false);
        optionPan = obj;
        }
    
    }

    public void outTouch(){

        if(UILevel[3].activeSelf) {
            UILevel[3].SetActive(false);
            currentUILevel += 1;
        }
        if(currentUILevel==1) {
            UILevel[1].SetActive(false);
            UILevel[0].SetActive(false);

        }
        UILevel[currentUILevel].SetActive(false);
        currentUILevel -= 1;
        UILevel[0].transform.SetSiblingIndex(UILevel[currentUILevel].transform.GetSiblingIndex()-1);
        if(currentUILevel == 2) UILevel[0].transform.SetSiblingIndex(UILevel[currentUILevel].transform.GetSiblingIndex()-1); 
        
    }

    public void souceValChange(int souce){
        Debug.Log(souce+"번째값 변경");
        GameManager.instance.player.playerSouce.souces[souce] = (int)souceSliders[souce].value;
        Debug.Log("플레이어 소스 : "+GameManager.instance.player.playerSouce.souces[souce]);
        Debug.Log("슬라이더 소스 : "+(int)souceSliders[souce].value);
    }
}