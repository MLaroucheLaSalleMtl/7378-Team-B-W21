using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resurgence : MonoBehaviour
{
    [SerializeField] private Enemy My_enemy;
    private List<GameObject> enemy_list;

    [SerializeField] private GameObject ResurgenceStartFX;
    [SerializeField] private GameObject ResurgenceCircleFX;
    


    void Start()
    {
        enemy_list = new List<GameObject>();
        ResurgenceCircleFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(enemy_list.Count>0)
        {
            if (My_enemy.Energy1 >= 100f)
            {
                My_enemy.Energy1 = 0f;
                ReviveEnemy();
               

            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        
            //if (My_enemy.Energy1 >= 100f)
            //{
            //    ReviveEnemy();
            //    My_enemy.Energy1 = 0f;

            //}
       
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dead")
        {
            enemy_list.Add(other.gameObject);
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Dead")
        {
            enemy_list.Remove(other.gameObject);
        }
    }
    private void ReviveEnemy()
    {
        My_enemy.StopMove();
        My_enemy.Enemy_Anim1.SetTrigger("Spell");
        Invoke("Instantiate1", 0.3f);
        Invoke("Instantiate2", 1f);
        Invoke("RestartMove", 2f); 
    }
    private void Instantiate1()
    {
        
        GameObject ReSur1 = Instantiate(ResurgenceStartFX, transform.position, Quaternion.identity);
        ReSur1.transform.localScale *= 15f;
        Destroy(ReSur1, 2f);
    }
    private void Instantiate2()
    {
        
        GameObject ReSur2 = Instantiate(ResurgenceCircleFX, transform.position, Quaternion.identity);
        ReSur2.SetActive(true);
        ReSur2.transform.parent = transform;
        
        
        Destroy(ReSur2, 5f);
    }
    private void RestartMove()
    {
        My_enemy.RestartMove();
    }

}
