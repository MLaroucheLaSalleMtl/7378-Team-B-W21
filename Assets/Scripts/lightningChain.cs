using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningChain : MonoBehaviour
{
    Transform Current_enemyPos;

    
    List<Enemy> CollidedEnemyList = new List<Enemy>();
    [SerializeField] private ThunderTurret MyTurret;
    private int ProbabilityPercentForThunder;
    private GameObject ThunderLightingFX;
    private GameObject ThunderImPactFX;
    private float StopMoveTime;
    private float damage;

    

    private float Timer;
    void Start()
    {
        ProbabilityPercentForThunder = MyTurret.ProbabilityPercentForThunder1;
        ThunderLightingFX = MyTurret.ThunderLightingFX1;
        ThunderImPactFX = MyTurret.ThunderImpactFX1;
        StopMoveTime = MyTurret.StopMoveTime1;
        Timer = 0f;
        damage = MyTurret.Damage1;
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="enemy")
        {
            TakenThunder(other.GetComponent<Enemy>(),false);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="enemy")
        {
            GameObject ImpactFX = Instantiate(ThunderImPactFX, other.transform.position, Quaternion.identity);
            Destroy(ImpactFX, 2f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="enemy")
        {
            
                Timer += Time.deltaTime;
           
            
            if(Timer>=1f)
            {
                TakenThunder(other.GetComponent<Enemy>(), true);
                Timer = 0f;

            }


        }
        
    }
    private void TakenThunder(Enemy enemy,bool IsPerSeconds)
    {
        if(enemy!=null)
        {
            if (IsPerSeconds)
            {

                enemy.TakeDamage(damage * 0.2f);
            }
            if (!IsPerSeconds)
            {
                enemy.TakeDamage(damage * 0.5f);
            }
            GameObject ImpactFX = Instantiate(ThunderImPactFX, enemy.transform.position, Quaternion.identity);
            Destroy(ImpactFX, 2f);

            if (CalculateTheProbability(ProbabilityPercentForThunder))      //for example   there is 20% percent to instantiate a lighting to stop enemy moving and clear their energy.
            {
                GameObject Lighting = Instantiate(ThunderLightingFX, enemy.transform.position, Quaternion.identity);
                Destroy(Lighting, 2f);
                enemy.TakeThunderDamage(damage * 0.1f, StopMoveTime);
            }
        }
        
    }

    private bool CalculateTheProbability(int ProbabilityPercent)
    {

        int RandomNum = Random.Range(0, 99);
        if (RandomNum <= ProbabilityPercent - 1)
        {
            return true;
        }
        if (RandomNum > ProbabilityPercent - 1)
        {
            return false;
        }
        return false;

    }
}
