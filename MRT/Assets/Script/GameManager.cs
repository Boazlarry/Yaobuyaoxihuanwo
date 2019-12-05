using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerManager player = new PlayerManager();

    public 

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad (gameObject);
        instance = this;

		Screen.SetResolution(1920,1080, true);
		Debug.Log("게임시작");

        player.money = 0;
        player.date = 0;
        player.personPTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public void GameStart(string sceneName)
	{
        Debug.Log(sceneName+"으로 레벨 이동");
		SceneManager.LoadScene (sceneName);
	}

}


