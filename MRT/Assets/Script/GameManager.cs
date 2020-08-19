using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
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
    public Souce systemSouce = new Souce();

    public string dataFileName = ".json";

    
    // Start is called before the first frame update
    void Start()
    {
        player = LoadData();
        // 객체 유지 선언
        DontDestroyOnLoad(gameObject);
        // 이 클래스의 스태틱 인스턴스를 선언
        instance = this;
        // 스크린 비율 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Screen.SetResolution(1920,1080, true);
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
        player = SaveData();
        yield return new WaitForSeconds(3.0f);
        player.time++;
        if (player.time % 24 == 0) systemSouce.SetRandom();
        
        int totalPeople = 0;
        int totalPrice = 0;
        
        foreach(Basket basket in player.baskets)
        {
            totalPeople += basket.ingredient.people;
            totalPrice += basket.ingredient.benefit;
        }
        player.peoplePTime = (int)(totalPeople * systemSouce.CmpSouce(player.souce));
        player.money += player.peoplePTime * totalPrice;
        //Debug.Log(systemSouce.CmpSouce(playerSouce));
        foreach(Basket basket in player.baskets)
        {
            if (basket.expiration > 0)
            {
                basket.expiration -= 1;
                basket.amount -= player.peoplePTime;
            }
            if (basket.expiration == 0 || basket.amount <= 0)
            {
                basket.Init();
            };
         
        }
        StartCoroutine("Timer");
    }

    public PlayerManager SaveData()
    {
        Debug.Log("파일 저장");
        string filePath = Application.persistentDataPath + dataFileName;
        string toFile = JsonUtility.ToJson(player);
        File.WriteAllText(filePath, toFile);
        return player;
    }

    public PlayerManager LoadData()
    {
        string filePath = Application.persistentDataPath + dataFileName;
        Debug.Log(filePath);
        if(File.Exists(filePath))
        {
            Debug.Log("로드 성공");
            string fromFile = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerManager>(fromFile);
        }
        else
        {
            Debug.Log("로드 실패, 파일 생성");
            return new PlayerManager();
        }
    }

}