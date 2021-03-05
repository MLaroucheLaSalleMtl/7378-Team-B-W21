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

    bool isStop = true;
    public Transform Pause;

    public void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
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
        if (isStop == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                isStop = false;
                Pause.gameObject.SetActive(true);
                //AudioListener.volume = 0;

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                isStop = true;
                Pause.gameObject.SetActive(false);
                //AudioListener.volume = 1;

            }
        }
    }
}
