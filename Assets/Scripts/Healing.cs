using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [SerializeField] private GameObject Heal_circleFX;
    [SerializeField] private Enemy Heal_Enemy;

    [SerializeField] private float HealTime;

    float timer;
    
    
    void Start()
    {
        Heal_circleFX.SetActive(false);
        timer = HealTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Heal_Enemy.Energy1>=100f)
        {
            Heal_circleFX.SetActive(true);
            Invoke("SetCircleFalse", HealTime);
            Heal_Enemy.Energy1 = 0f;
        }
        
    }
    private void SetCircleFalse()
    {
        Heal_circleFX.SetActive(false);
    }

   
    
    
    
    
}
