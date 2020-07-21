using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 이 클래스의 스태틱 인스턴스 생성
    public static GameManager instance;
    // 플레이어매니저 클래스를 생성
    public PlayerManager player;
    public UIManager UI = UIManager.instance;
    public List<Basket> baskets;
    public Souce gameManagerSouce = new Souce();
    int price;

    
    // Start is called before the first frame update
    void Start()
    {
        player = new PlayerManager();
        // 객체 유지 선언
        DontDestroyOnLoad(gameObject);
        // 이 클래스의 스태틱 인스턴스를 선언
        instance = this;
        // 스크린 비율 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Screen.SetResolution(1920,1080, true);
        baskets = player.baskets;

		Debug.Log("게임시작");
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
     * void changeScene(string sceneName)
     * sceneName을 문자열로 받아 해당 씬으로 전환하는 함수
     * 
     * 
     */
	public void ChangeScene(string sceneName)
	{
        Debug.Log(sceneName+"으로 레벨 이동");
		SceneManager.LoadScene(sceneName);
        
        
        if (sceneName.Equals("Ingame")) StartCoroutine("Timer"); // 타이머 시작
    }

    /* 
     * IEnumerator Timer()
     * Timer 코루틴을 이용해 3초당 1시간의 개념으로 실행시간 게임 진행 됨
     * 이를 통해 수익 반영, 재료 유통기한 감소, 특정 이벤트 발생을 처리함
     * 
     */
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3.0f);
        
        if (player.time % 24 == 0) gameManagerSouce.setRandom();
        player.time++;

        price = 0;
        int temp = 0;
        
        foreach(Basket basket in baskets)
        {
            
            if (basket.expiration > 0)
            {
                basket.expiration -= 1;
                basket.amount -= player.peoplePTime;
                price += basket.ingredient.price;
                if(basket.ingredient.price == 0) price +=1;
            }
            if (basket.expiration == 0 || basket.amount <= 0){
                temp += basket.ingredient.people;
                basket.Init();
            };
         
        }
        
        player.money += player.peoplePTime * (price);
        player.ingredientPeople -= temp;

        
        StartCoroutine("Timer");
    }


}


