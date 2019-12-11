using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IngPan : MonoBehaviour
{
    // Start is called before the first frame update
    public Text inform;
    public Image image;
    public Button button;
    public Ingredients ingredient;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ingBuy(){
        if(GameManager.instance.player.money<ingredient.price * Bucket.amountBuy) {
            GameManager.instance.alert("돈이 부족합니다");
            return;
        }
        else GameManager.instance.player.money -= ingredient.price * Bucket.amountBuy;
        Bucket b = UIManager.instance.currentBucket;
        
        if (b.state == 2) GameManager.instance.player.ingPeople -= b.ing.people;
        
        b.amount = Bucket.amountBuy;
        b.ing = ingredient;
        GameManager.instance.player.ingPeople += b.ing.people;
        b.expiration = ingredient.expiration;
        b.state = 2;
        b.bucketUI.GetComponent<Image>().sprite = ingredient.img;
        
        

        UIManager.instance.UILevel[UIManager.instance.currentUILevel].SetActive(false);
        UIManager.instance.currentUILevel -=1;
        UIManager.instance.UILevel[0].transform.SetSiblingIndex(UIManager.instance.UILevel[UIManager.instance.currentUILevel].transform.GetSiblingIndex()-1);
        


    }
}
