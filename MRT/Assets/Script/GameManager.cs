using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // 이 클래스의 스태틱 인스턴스 생성
    public static GameManager instance;
    // 플레이어매니저 클래스를 생성
    public PlayerManager player = new PlayerManager();
    public UIManager UI = UIManager.instance;
    // 재료 바구니들을 참조할 수 있는 리스트
    public List<Bucket> buckets = new List<Bucket>();

    int price;

    // Start is called before the first frame update
    void Start()
    {
        // 객체 유지 선언
        DontDestroyOnLoad (gameObject);
        // 이 클래스의 스태틱 인스턴스를 선언
        instance = this;
        // 스크린 비율 설정
		Screen.SetResolution(1920,1080, true);
		Debug.Log("게임시작");
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
     * void GameStart(string sceneName)
     * sceneName을 문자열로 받아 해당 씬으로 전환하는 함수
     * 1.scene에서 main으로 레벨 전환할 때 사용
     * 
     */
	public void GameStart(string sceneName)
	{
        Debug.Log(sceneName+"으로 레벨 이동");
		SceneManager.LoadScene (sceneName);

        // 타이머 시작
        if (sceneName.Equals("game")) StartCoroutine("Timer");
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

        price = 0;
        player.peoplePTime = 0;
        player.time++;
        foreach(Bucket bucket in buckets)
        {
            if (bucket.expiration > 0)
            {
                bucket.expiration -= 1;
                if (bucket.amount >= player.peoplePTime) bucket.amount -= player.peoplePTime;
                else continue;
                player.peoplePTime += bucket.ing.people;
                price += bucket.ing.price;
            }
            if (bucket.expiration == 0 || bucket.amount == 0) buckets.Remove(bucket);
         
        }
        player.money += player.peoplePTime * (price*100);
        player.money += 1;
        StartCoroutine("Timer");
    }
}


