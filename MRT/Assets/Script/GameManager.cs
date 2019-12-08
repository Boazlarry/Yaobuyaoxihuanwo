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
    public PlayerManager player = new PlayerManager();
    public UIManager UI = UIManager.instance;
    public List<Bucket> buckets;
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
        buckets = player.buckets;

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
        

        price = 0;
        
        
        /* 버켓에 재료를 추가하는 순간부터 표시하도록 제거
        foreach(Bucket bucket in buckets){
            /**MoveNext오류, 래퍼런스 참조 오류 -> 초기화된 Bucket의 ing을 할당함으로 해결
            Debug.Log(player.time);
            Debug.Log(buckets);
            Debug.Log(bucket);
            Debug.Log(player.peoplePTime);
            Debug.Log(bucket.ing.people);
            /****************************** /
            player.peoplePTime += bucket.ing.people;
        }
        */
        int temp = 0;
        player.time++;
        foreach(Bucket bucket in buckets)
        {
            if (bucket.expiration > 0)
            {
                bucket.expiration -= 1;
                bucket.amount -= player.peoplePTime;
                price += bucket.ing.price;
            }
            if (bucket.expiration == 0 || bucket.amount <= 0){
                temp += bucket.ing.people;
                bucket.init();
            };
         
        }
        player.money += player.peoplePTime * (price);
        player.peoplePTime -= temp;

        yield return new WaitForSeconds(5.0f);
        StartCoroutine("Timer");
    }

    public void alert(string s){
        
        UIManager.instance.UILevel[3].GetComponentInChildren<Text>().text = s;
        UIManager.instance.UILevel[3].SetActive(true);
        UIManager.instance.UILevel[0].transform.SetSiblingIndex(UIManager.instance.UILevel[3].transform.GetSiblingIndex()-1);
        
        
    }
}


