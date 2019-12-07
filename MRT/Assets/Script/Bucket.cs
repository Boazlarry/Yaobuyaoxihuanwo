using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Bucket : MonoBehaviour
{
    public Ingredients ing;
    public int amount;
    public int expiration;
    public static Sprite defaultImg;
    public int state = 0;
    public GameObject bucketUI;
    static GameObject preFab;

    // Start is called before the first frame update
    void Start()
    {
        UIManager UIInstance = UIManager.instance;
        defaultImg = Resources.Load<Sprite>("UI/ings/고수");
        preFab = Resources.Load<GameObject>("Prefab/Bucket");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bucketAdd(GameObject emptySpace){

        if (state == 0){
        state = 1;
        this.GetComponent<Image>().sprite = defaultImg;
        bucketUI = gameObject;
        UIManager.instance.buckets.Add(this);
        bucketUI.GetComponentInChildren<Text>().text = "재고 없음";

        GameObject newEmptySpace = Instantiate(preFab);
        RectTransform preFab_tr = newEmptySpace.GetComponent<RectTransform>();
        preFab_tr.SetParent(UIManager.instance.refriPan.transform);
        preFab_tr.localScale = Vector2.one;
        }
        else ingAdd(emptySpace);
    }

    void ingAdd(GameObject emptyBucket){

        
        // 재료 선택창, 이후 수량선택 띄우고 해당 오브젝트의 
    }
    public void init(){
        ing = null;
        amount = 0;
        expiration = 0;
        state = 1;
        bucketUI.GetComponent<Image>().sprite = defaultImg;
        bucketUI.GetComponentInChildren<Text>().text = "재고 없음";
    }
}
