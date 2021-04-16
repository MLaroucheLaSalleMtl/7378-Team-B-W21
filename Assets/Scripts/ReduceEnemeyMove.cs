using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceEnemeyMove : MonoBehaviour
{
    [SerializeField] private GameObject Reduce_FX;
    private List<Enemy> enemylist;
    private List<GameObject> ReduceFX_List;

    private float damage;
    private float continueDamage;
    [SerializeField] private bullet IceBullet;
    private bool SetValueD;

    private float timer;
    void Start()
    {
        enemylist = new List<Enemy>();
        ReduceFX_List = new List<GameObject>();
        SetValueD = false;
        damage = IceBullet.damage;
        continueDamage = IceBullet.continuesDamage;
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ReduceMove(Enemy enemy)
    {
        enemy.Agent1.speed *= 0.5f;
    }
    void ReSetMove(Enemy enemy)
    {
        enemy.Agent1.speed *= 2f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemylist.Add(enemy);
            if(!enemy.IsReducing1)
            {
                enemy.TakeSnowBallDamage(damage);
            }
                
                
                  
           
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="enemy")
        {
            
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.IsReducing1)
            {
                enemy.ReSetMoveSpeed();
                Destroy(enemy.transform.Find("ReduceMove_Area"));  //when enemy out of the triggerarea destroy the ReduceMove FX.
            }
            

            

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            
                TakenDamagePerSeconds(enemy, continueDamage);


           
        }
        }
    private void TakenDamagePerSeconds(Enemy enemy,float damage)
    {
        if(timer<1f)
        {
            timer += Time.deltaTime;
        }
        if(timer>=1f)
        {
            timer = 0f;
            enemy.TakeDamage(damage);
        }
    }
    


}
