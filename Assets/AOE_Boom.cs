using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Boom : MonoBehaviour
{
    private float damage;
    private List<Turret> turret_list=new List<Turret>();
    Turret current_Turret;
    private Vector3 Move_Position;

    
    bool RunoneTime;

    public float Damage { get => damage; set => damage = value; }

    void Start()
    {
        RunoneTime = true;
        Move_Position = transform.position;
        Move_Position.y += 2f;
        transform.position = Move_Position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turrent")
        {
            
                
                current_Turret = other.gameObject.GetComponent<Turret>();
                current_Turret.Taken_Damage(Damage);
            
                Debug.Log("Turrent Taken damage ,the turrent Rest HP " + current_Turret.Hp);
           
        }
    }
   
    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Turrent")
        //{
        //    if (RunoneTime)
        //    {
        //        RunoneTime = false;
        //        current_Turret = other.GetComponent<Turret>();
        //        current_Turret.Taken_Damage(Damage);
        //        Debug.Log("Turrent Taken damage ,the turrent Rest HP " + current_Turret.Hp);
        //    }
        //}
    }
}
