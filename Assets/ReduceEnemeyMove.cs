using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceEnemeyMove : MonoBehaviour
{
    [SerializeField] private GameObject Reduce_FX;
    private List<Enemy> enemylist;
    private List<GameObject> ReduceFX_List;
    void Start()
    {
        enemylist = new List<Enemy>();
        ReduceFX_List = new List<GameObject>();
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
                ReduceMove(enemy);
                GameObject ReduceEnemy_FX = Instantiate(Reduce_FX, other.transform.position, Quaternion.identity);
                ReduceEnemy_FX.transform.parent = other.transform;
                enemy.IsReducing1 = true;
                  
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
                ReSetMove(enemy);
                Destroy(enemy.transform.GetChild(enemy.transform.childCount - 1).gameObject);  //when enemy out of the triggerarea destroy the ReduceMove FX.
                enemylist.Remove(enemy);
                
                enemy.IsReducing1 = false;
            }

            

        }
    }
  
}
