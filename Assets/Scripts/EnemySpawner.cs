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
    public Text Text_AllWaves;
    public Text Text_CurrentWave;
    public int waveNumber;
    private Coroutine coroutine;

    [SerializeField] private GameObject Instantiate_EnmeyFX;

    [SerializeField] private Image TimeCounter;


    private float WaveRateTimer;
    private bool IfCountWRT;
    public bool IsLevel3;

    private bool StartCountTimer;

    void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
        Text_AllWaves.text = waves.Length.ToString();
        waveNumber = 1;
        IfCountWRT = true;
        WaveRateTimer = 0f;

        StartCountTimer = true;
        TimeCounter.fillAmount = 0;


    }

    public void Stop()
    {
        StopCoroutine(coroutine);

    }

    public void Update()
    {
        Debug.Log("EnemyCount = " + EnemyCount);
        Debug.Log("WaveSum+EnemyCount" + CountSelectedWaveNum(waveNumber - 1) + " , " + EnemyCount2);
        IfInWaveRateTime();

        Text_CurrentWave.text = waveNumber.ToString();
        if (waveNumber > waves.Length)
        {
            //Stop();
            waveNumber = waves.Length;
        }



        //if(SpawnEnemy().MoveNext())
        //{
        //    IfCountWRT = true;
        //}


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

    IEnumerator SpawnEnemy()
    {


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

            //for (int i = 0; i < wave.count; i++)
            //{
            //        GameObject.Instantiate(wave.enemyPrefb, StartPoint.position, Quaternion.identity);
            //        EnemyCount++;
            //        if (i != wave.count - 1)
            //            yield return new WaitForSeconds(wave.rate);
            //}
            //for (int i = 0; i < wave.count2; i++)
            //{
            //    GameObject.Instantiate(wave.enemyPrefb2, StartPoint.position, Quaternion.identity);
            //    EnemyCount++;
            //    if (i != wave.count2 - 1)
            //        yield return new WaitForSeconds(wave.rate);
            //}





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
        if(!IsLevel3)
        {
            LevelManager.Instance.Win();
        }
        else if(IsLevel3)
        {
            SceneManager.LoadScene("");
        }
        
    }
}


    