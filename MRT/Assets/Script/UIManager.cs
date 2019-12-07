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
    GameObject optionPan;
    //public List<GameObject> buckets_UI;
    public List<Bucket> buckets;
    public List<Ingredients> ingredients;
    

    void Start()
    {
        instance=this;
    }

    // Update is called once per frame
    void Update()
    {

        money.text = GameManager.instance.player.money.ToString();
        people.text = GameManager.instance.player.peoplePTime.ToString();
        time.text = GameManager.instance.player.time.ToString() + " 시간";

        buckets = GameManager.instance.buckets;

        foreach (Bucket bucket in buckets)
        {
            if (bucket.state == 2)
            {
                bucket.GetComponentInChildren<Text>().text = bucket.ing.ingName + "\n유통기한 : " + bucket.expiration.ToString() + "\n수량 : " + bucket.amount.ToString();
            }
        }
        
    }
    
    // UI에서 소스창 열고 닫을 때 사용 
    public void buttonClick(GameObject obj){
    if (obj.activeSelf) obj.SetActive(false);
    else obj.SetActive(true);
    }
    
    public void buttonClickOptPan(GameObject obj){
    if (!optionPan) {
        obj.SetActive(true);
        optionPan = obj;
    }
    if (obj.Equals(optionPan));
    else {
        obj.SetActive(true);
        optionPan.SetActive(false);
        optionPan = obj;
        }
    
    }

}