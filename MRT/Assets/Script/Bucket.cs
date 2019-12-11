using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Bucket : MonoBehaviour
{
    public Ingredients ing;
    public static int amountBuy = 30;
    public int amount;
    public int expiration;
    public static Sprite defaultImg;
    public int state = 0;
    public GameObject bucketUI;
    static GameObject bucket_PreFab;
    public Text inform;

    // Start is called before the first frame update
    void Start()
    {
        UIManager UIInstance = UIManager.instance;
        defaultImg = Resources.Load<Sprite>("UI/버튼");
        bucket_PreFab = Resources.Load<GameObject>("Prefab/Bucket");
        inform = gameObject.GetComponentInChildren<Text>();
        gameObject.GetComponentInChildren<Text>().text = "바구니 추가\n" + (GameManager.instance.buckets.Count * 100).ToString() + " 원";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bucketAdd(GameObject emptySpace){

        
        if (state == 0){
            if(GameManager.instance.player.money< GameManager.instance.buckets.Count * 100){
                GameManager.instance.alert("돈이 부족합니다.");
                return;
            }
        
        GameManager.instance.player.money -= GameManager.instance.buckets.Count * 100;
        bucketUI = gameObject;
        UIManager.instance.buckets.Add(this);

        init();
        
        GameObject newEmptySpace = Instantiate(bucket_PreFab);
        RectTransform preFab_tr = newEmptySpace.GetComponent<RectTransform>();
        preFab_tr.SetParent(UIManager.instance.refriPan.transform);
        preFab_tr.localScale = Vector2.one;
        }
        else ingAdd(emptySpace);
    }

    void ingAdd(GameObject emptyBucket){
 
        UIManager.instance.currentUILevel = 2;
        UIManager.instance.UILevel[0].transform.SetSiblingIndex(UIManager.instance.UILevel[UIManager.instance.currentUILevel].transform.GetSiblingIndex()-1);
        UIManager.instance.UILevel[UIManager.instance.currentUILevel].SetActive(true);
        UIManager.instance.currentBucket = emptyBucket.GetComponent<Bucket>();

    }
    public void init(){
        ing = Ingredients.defaultIng;
        amount = 0;
        expiration = 0;
        state = 1;
        bucketUI.GetComponent<Image>().sprite = defaultImg;
        bucketUI.GetComponentInChildren<Text>().text = "재고 없음\n";
    }
}
