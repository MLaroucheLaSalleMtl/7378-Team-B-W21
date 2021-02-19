using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public Transform MainMenu;
    public Transform Summary;
    public Transform WinSummary;

    bool ISPause;
    bool isStop = true;
    private bool InGame;

    public float GameTimer;
    public int totalscore;

    public bool gamestarted = false;
    // Start is called before the first frame update
    void Start()
    {
        CallMainManu();
    }

    void CallMainManu()
    {
        InGame = false;
        Time.timeScale = 0;
        MainMenu.gameObject.SetActive(true);
        gamestarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ISPause)
            Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && ISPause)
            UNpause();
    }

    void OpenSummary()
    {
        Time.timeScale = 0;
        InGame = false;
        Summary.gameObject.SetActive(true);

        Text txtpoint = Summary.Find("Score").GetComponent<Text>();
        txtpoint.text = "Score : " + totalscore;

        Text txtUserTime = Summary.Find("TimeLeft").GetComponent<Text>();
        txtUserTime.text = "Time Left : " + GameTimer;
    }

    void OpenWinSummary()
    {
        Time.timeScale = 0;
        InGame = false;
        WinSummary.gameObject.SetActive(true);

        Text txtpoint = WinSummary.Find("ScoreWin").GetComponent<Text>();
        txtpoint.text = "Score : " + totalscore;

        Text txtUserTime = WinSummary.Find("TimeLeftWin").GetComponent<Text>();
        txtUserTime.text = "Time Left : " + GameTimer;

    }

    void StartGame()
    {
        MainMenu.gameObject.SetActive(false);
        InGame = true;
        
    }

    public void Pause()
    {
        ISPause = true;
        MainMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UNpause()
    {
        ISPause = false;
        MainMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Click_ExitGame()
    {
        Application.Quit();
    }

    public void Click_Menu()
    {

        Application.Quit();

    }

    public void Click_NextGame()
    {


        Summary.gameObject.SetActive(false);

        SceneManager.LoadScene(0);

        
    }

    public void Click_WinNextGame()
    {
        WinSummary.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }
    public void Click_WinMenu()
    {
        WinSummary.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
