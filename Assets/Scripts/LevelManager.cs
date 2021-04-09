using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOverPannel;
    public GameObject gameWinPannel;
    public GameObject settingPannel;
    //public GameObject ingameUIPannel;
    public Text endMessage;
    public static LevelManager Instance;
    private EnemySpawner enemySpawner;
    public GameObject IngameUI;
    [SerializeField] private int sceneIndex;



    //Peixin

    [SerializeField] private float Basement_HP;
    private float Basement_MaxHP;
    [SerializeField] private Text BasementHP_Display;
    [SerializeField] private Image BasementHP_Image;

    private float BasementHP_Percent;

    bool canStop = true;
    public Transform Pause;

   

    public float Basement_HP1 { get => Basement_HP; set => Basement_HP = value; }
    public float Basement_MaxHP1 { get => Basement_MaxHP; set => Basement_MaxHP = value; }

    public void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
        //turretInfoPannel[turretPannelIndex].SetActive(false);

        Basement_MaxHP1 = Basement_HP1;
        Time.timeScale = 1;
    }
    
    void CaculateBasementHP()
    {
        
        
        BasementHP_Percent = Basement_HP1 / Basement_MaxHP1 * 100;
        BasementHP_Image.fillAmount = BasementHP_Percent / 100;
        BasementHP_Display.text = BasementHP_Percent + "%";
    }

    
    public void Btmsetting()
    {
        settingPannel.SetActive(true);
        Pause.gameObject.SetActive(false);
    }
    public void BtmsettingBackToPause()
    {
        settingPannel.SetActive(false);
        Pause.gameObject.SetActive(true);
    }



    public void Win()
    {

        gameWinPannel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Fail()
    {
        enemySpawner.Stop();
        gameOverPannel.SetActive(true);
        endMessage.text = "Failed!";
    }

    public void BtmRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BtmNextGame()
    {
        sceneIndex++;
        SceneManager.LoadScene(sceneIndex);
    }

    public void BtmMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void Backtogame()
    {
        Time.timeScale = 1;
        Pause.gameObject.SetActive(false);

        canStop = true;
        IngameUI.SetActive(true);
        settingPannel.SetActive(false);
        //turretInfoPannel[turretPannelIndex].SetActive(false);
        //enemyInfoPannel[enemyPannelIndex].SetActive(false);
        //ingameUIPannel.SetActive(true);
        AudioListener.volume = 1;
    }
    private void Update()
    {
       
        if (canStop == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                canStop = false;
                Pause.gameObject.SetActive(true);
                IngameUI.SetActive(false);
                //ingameUIPannel.SetActive(false);
                AudioListener.volume = 0;


            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                Backtogame();


            }
        }
        CaculateBasementHP();

        if(Basement_HP <=0)
        {
            Fail();
        }
    }
}
