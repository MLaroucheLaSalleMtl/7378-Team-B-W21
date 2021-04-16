using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    //numbers of enemy
    public static int EnemyCount = 0;
    private int EnemyCount2 = 0;
    //waves of enemy
    public Wave[] waves;
    public Transform StartPoint;
    public float waveRate;
    //public Text Text_AllWaves;
    //public Text Text_CurrentWave;
    private int waveNumber;
    private Coroutine coroutine;

    [SerializeField] private GameObject Instantiate_EnmeyFX;

    [SerializeField] private Image TimeCounter;


    private float WaveRateTimer;
    private bool IfCountWRT;
    public bool IsLevel3;

    private bool StartCountTimer;

    private bool StartWave;
    private bool SetWinPanel;

    //time count down
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    private bool canSpawnEnemy;
    //
    private float startTime;
    private float resetStartTime;



    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("Enemy Spawn in: " + "{0:00}:{1:00}", minutes, seconds);
    }



    void Start()
    {
        startTime = 3f;
        canSpawnEnemy = false;
        coroutine = StartCoroutine(SpawnEnemy());
        //Text_AllWaves.text = waves.Length.ToString();
        waveNumber = 1;
        IfCountWRT = true;
        WaveRateTimer = 0f;

        StartCountTimer = true;
        TimeCounter.fillAmount = 0;
        StartWave = true;
        SetWinPanel = false;
        
        //time count down
        timerIsRunning = true;
        //
        resetStartTime = startTime;
    }

    public void Stop()
    {
        StopCoroutine(coroutine);

    }

    public void Update()
    {
        Debug.Log("WaveNumber" + waveNumber);
        Debug.Log("EnemyCount" + EnemyCount);
        if (waveNumber == waves.Length && EnemyCount == 0)
        {
            Debug.Log("yes");
            waveRate = 1;
            Debug.Log(waveRate);
        }
        //if (waveNumber == waves.Length + 1)
        //{
        //    if (EnemyCount == 0 && StartWave == false)
        //    {
        //        if (!StartWave)
        //        {
        //            StartWave = true;
        //            if (IsLevel3 == false)
        //            {
        //                SpawnEnemy().MoveNext();
        //            }
        //            else if (IsLevel3 == true)
        //            {
        //                SceneManager.LoadScene("GameEnd");
        //            }
        //        }
        //    }
        //}

        if (EnemyCount>0&&StartWave)
        {
            StartWave = false;
        }
        IfInWaveRateTime();

        //Text_CurrentWave.text = waveNumber.ToString();
        if (waveNumber > waves.Length)
        {
            //Stop();
            waveNumber = waves.Length;
        }
        TimeCount();

        if(waveNumber == waves.Length)
        {
            CheckEnemyTimeCounter(); 
        }
        


    }
    private void IfInWaveRateTime()
    {

        int waveindex = waveNumber - 1;


        if (EnemyCount2 == CountSelectedWaveNum(waveindex))
        {
            if (waveindex != waves.Length - 1)
            {
                CountWaveRateTimer();
                TimeCounter.fillAmount = 1 - WaveRateTimer / waveRate;
            }
            if (IfCountWRT)
            {
                ResetCountWaveRateTimer();
                IfCountWRT = false;
                Debug.Log("Find out the waverate time!!!");
            }
        }
        if (EnemyCount2 != CountSelectedWaveNum(waveindex))
        {
            IfCountWRT = true;
            WaveRateTimer = 0f;
            TimeCounter.fillAmount = 0;
        }
    }
    private void CountWaveRateTimer()
    {

        if (WaveRateTimer > 0f)
        {
            WaveRateTimer -= 1f * Time.deltaTime;
        }
        if (WaveRateTimer < 0f)
        {
            IfCountWRT = true;
            WaveRateTimer = 0f;
            
        }


    }
    private void ResetCountWaveRateTimer()
    {
        WaveRateTimer = waveRate;
    }
    private int CountSelectedWaveNum(int waveindex)
    {
        int CountResult = 0;
        if (waveindex < waves.Length)
        {
            foreach (int wavecount in waves[waveindex].count)
            {
                CountResult += wavecount;
            }

        }
        return CountResult;
    }

    public void TimeCount()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timeText.text = "";
                timerIsRunning = false;
                canSpawnEnemy = true;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(timeRemaining);

         foreach (Wave wave in waves)
         {
            EnemyCount2 = 0;
            for (int i = 0; i < wave.count.Length; i++)
            {
                for (int a = 0; a < wave.count[i]; a++)
                {

                    GameObject.Instantiate(wave.enemyPrefb[i], StartPoint.position, Quaternion.identity);
                    EnemyCount++;

                    EnemyCount2++;
                    if (a != wave.count[i])
                        yield return new WaitForSeconds(wave.rate);
                }


            }
                //while (EnemyCount > 0)
                //{
                //    yield return 0;
                //}


                yield return new WaitForSeconds(waveRate);

                waveNumber += 1;
                waveRate *= 0.8f;   

         }       
       

        //still have enemy in game
        while (EnemyCount > 0)
        {
            yield return 0;
        }

    
    }

    public void CheckEnemyTimeCounter()
    {
        if(startTime>0)
        {
            startTime -= Time.deltaTime;
        }
        if(startTime<=0)
        {
            startTime = resetStartTime;
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");
            if(enemys.Length == 0)
            {
                if (!IsLevel3)
                {
                    LevelManager.Instance.Win();
                }
                else if (IsLevel3)
                {
                    SceneManager.LoadScene("GameEnd");
                }
            }
        }

        
    }




    
}


    