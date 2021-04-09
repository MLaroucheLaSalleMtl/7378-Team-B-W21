using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProtection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject ShieldFX;
    [SerializeField] private Enemy enemy;

    float enemyResetDefence;
    void Start()
    {
        enemyResetDefence = enemy.Defence;
        ShieldFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.Energy1>=100f)
        {
            
            enemy.Energy1 = 0f;
            ShieldFX.SetActive(true);
            Invoke("SetShieldFalse", 10f);


            GainDefence(10f);
        }
    }
    private void GainDefence(float GainTime)
    {
        enemy.Defence = 100f;
        Invoke("ResetDefence", GainTime);
    }
    private void SetShieldFalse()
    {
        ShieldFX.SetActive(false);
    }
    private void ResetDefence()
    {
        enemy.Defence = enemyResetDefence;
    }
}
