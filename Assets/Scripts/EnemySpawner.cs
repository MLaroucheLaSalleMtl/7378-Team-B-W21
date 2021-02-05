using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //numbers of enemy
    public static int EnemyCount = 0;
    //waves of enemy
    public Wave[] waves;
    public Transform StartPoint;
    public float waveRate = 0.3f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void Stop()
    {
        StopCoroutine("SpawnEnemy");
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
        }

        //still have enemy in game
        while (EnemyCount > 0)
        {
            yield return 0;
        }
        //GameManager.Instance.Win();
    }
}
