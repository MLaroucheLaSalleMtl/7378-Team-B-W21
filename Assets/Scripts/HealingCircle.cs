using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : MonoBehaviour
{
    [SerializeField] private float HealValue;
    [SerializeField] private GameObject HealEnmeyFX;
    [SerializeField] private Enemy heal_Enemy;

    

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Healing(heal_Enemy);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="enemy")
        {
            Healing(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="enemy")
        {
            GameObject HealFX = Instantiate(HealEnmeyFX, other.transform.position, Quaternion.identity);
            HealFX.transform.parent = other.transform;
            Destroy(HealFX, 10f);
            
            
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.tag=="enemy")
        {
            if(other.transform.Find("AuraChargeGreen(Clone)") !=null)
            {
                Destroy(other.transform.Find("AuraChargeGreen(Clone)").gameObject);
            }
        }
    }
    private void Healing(Enemy enemy)
    {

        
        if (enemy.Hp >= enemy.Max_hp) return;
        
        if (enemy.Hp < enemy.Max_hp&&enemy.Hp>0f)
        {
            enemy.Hp += HealValue * Time.deltaTime;
            if (enemy.Hp > enemy.Max_hp)
            {
                enemy.Hp = enemy.Max_hp;
            }
        }
    }

}
