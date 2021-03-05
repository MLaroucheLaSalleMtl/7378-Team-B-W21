using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //numbers of enemy
    public static int EnemyCount = 0;
    //waves of enemy
    public Wave[] waves;
    public Transform StartPoint;
    public float waveRate = 0.3f;
    public Text Text_AllWaves;
    public Text Text_CurrentWave;
    public int waveNumber;
    private Coroutine coroutine;

    void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());
        Text_AllWaves.text = waves.Length.ToString();
        waveNumber = 1;
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    public void Update()
    {
        Text_CurrentWave.text = waveNumber.ToString();
        if(waveNumber > waves.Length)
        {
            //Stop();
            waveNumber = waves.Length;
        }
    }


    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefb, StartPoint.position, Quaternion.identity);
                EnemyCount++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
                
            }
            while (EnemyCount > 0)
            {
                yield return 0;               
            }
            yield return new WaitForSeconds(waveRate);

            waveNumber += 1;

        }

        //still have enemy in game
        while (EnemyCount > 0)
        {
            yield return 0;
        }
        LevelManager.Instance.Win();
    }
}
