using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject gameOverPannel;
    public Text endMessage;
    public static LevelManager Instance;
    private EnemySpawner enemySpawner;

    bool canStop = true;
    public Transform Pause;

    [Header("Info of pannel")]
    //Show Information pannel
    [SerializeField] private GameObject[] turretInfoPannel;
    [SerializeField] private int turretPannelIndex;
    [SerializeField] private GameObject[] enemyInfoPannel;
    [SerializeField] private int enemyPannelIndex;

    public void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
        turretInfoPannel[turretPannelIndex].SetActive(false);
    }

    //Turret Info
    public void BtmTurretInfor()
    {
        turretPannelIndex = 0;
        turretInfoPannel[turretPannelIndex].SetActive(true);
        Pause.gameObject.SetActive(false);
    }

    public void BtmTurretBackToPause()
    {
        turretInfoPannel[turretPannelIndex].SetActive(false);
        Pause.gameObject.SetActive(true);
    }

    public void BtmTurretNextPage()
    {

        if (turretPannelIndex < turretInfoPannel.Length)
        {
            turretInfoPannel[turretPannelIndex].SetActive(false);
            turretPannelIndex += 1;
            turretInfoPannel[turretPannelIndex].SetActive(true);
        }
    }

    public void BtmTurretPreviousPage()
    {
        turretInfoPannel[turretPannelIndex].SetActive(false);
        turretPannelIndex -= 1;
        turretInfoPannel[turretPannelIndex].SetActive(true);
    }

    //Enemy Info
    public void BtmEnemyInfor()
    {
        enemyPannelIndex = 0;
        enemyInfoPannel[enemyPannelIndex].SetActive(true);
        Pause.gameObject.SetActive(false);
    }
    public void BtmEnemyBackToPause()
    {
        enemyInfoPannel[enemyPannelIndex].SetActive(false);
        Pause.gameObject.SetActive(true);
    }
    public void BtmEnemyNextPage()
    {

        if (enemyPannelIndex < enemyInfoPannel.Length)
        {
            enemyInfoPannel[enemyPannelIndex].SetActive(false);
            enemyPannelIndex += 1;
            enemyInfoPannel[enemyPannelIndex].SetActive(true);
        }
    }
    public void BtmEnemyPreviousPage()
    {
        enemyInfoPannel[enemyPannelIndex].SetActive(false);
        enemyPannelIndex -= 1;
        enemyInfoPannel[enemyPannelIndex].SetActive(true);
    }



    public void Win()
    {
        gameOverPannel.SetActive(true);
        endMessage.text = "Victory!";
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

    public void BtmMenu()
    {
        SceneManager.LoadScene(0);
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
                //AudioListener.volume = 0;

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                canStop = true;
                Pause.gameObject.SetActive(false);
                turretInfoPannel[turretPannelIndex].SetActive(false);
                enemyInfoPannel[enemyPannelIndex].SetActive(false);
                //AudioListener.volume = 1;

            }
        }
    }
}
