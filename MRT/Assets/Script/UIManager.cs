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
    public GameObject optionPan;
    public List<GameObject> buckets_UI;
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
        int index = 0;

        foreach (GameObject bucket_UI in buckets_UI)
        {
            if (index + 1 <= buckets_UI.Count)
            {
                bucket_UI.GetComponent<Image>().sprite = buckets[index].ing.img;
                bucket_UI.GetComponentInChildren<Text>().text = buckets[index].ing.ingName + "\n유통기한 : " + buckets[index].expiration.ToString() + "\n수량 : " + buckets[index].amount.ToString();
            }
            else
            {
                bucket_UI.GetComponent<Image>().sprite = Bucket.defaultImg;
                bucket_UI.GetComponentInChildren<Text>().text = "";
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
    if (obj.Equals(optionPan)) return;
    else {
        obj.SetActive(true);
        optionPan.SetActive(false);
        optionPan = obj;
        }
    }
}