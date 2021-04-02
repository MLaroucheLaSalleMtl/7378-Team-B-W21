using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_Shooting : MonoBehaviour
{
    [SerializeField] private GameObject Enemy_GameObject;
     private Enemy enemy;
    private float Arrow_Damage;
    private float Distance;
    private bool Fire;
    [SerializeField]private GameObject Arrow;
    private GameObject Arrow1;
    [SerializeField] Transform Arrow_StartPosition;
    [SerializeField] GameObject Arrow_Effect;
    private GameObject turrent_Target;
    private Vector3 Arrow_ArrivePosition;
    private bool Finish_Fire;
    private bool Find_Turret;
    private bool In_Shooting;
    private bool IfArrowImpact;
    private float ReMove_Timer;
    private float Reset_ReMoveTimer;
    void Start()
    {
        enemy = Enemy_GameObject.GetComponent<Enemy>();
        Fire = false;
        Finish_Fire = false;
        Find_Turret = false;

        In_Shooting = false;
        IfArrowImpact = false;
        Arrow1 = Arrow;
        Arrow_Damage = enemy.Normal_damage;
        ReMove_Timer = 3f;
        Reset_ReMoveTimer = ReMove_Timer;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag=="Turrent")
        {
            
            Distance = Vector3.Distance(transform.position, other.transform.position);
            if(Distance>20f)
            {
                
                if(enemy.Energy1>=100&&!Find_Turret)
                {
                    Debug.Log("In trigger");
                    Find_Turret = true;
                    enemy.Energy1 = 0;
                    enemy.StopMove();
                    Invoke("Reset_move",2f);

                    turrent_Target = other.transform.gameObject;
                    Arrow_ArrivePosition = turrent_Target.transform.position;
                    Arrow_ArrivePosition.y += 2f;
                    
                }
                
            }
        }
    }
    

    private void Shooting(Transform Target)
    {
        if(Find_Turret)
        {
            
                Enemy_GameObject.transform.LookAt(Target);
                Invoke("Set_BowShootAnim", 0.5f);

                Find_Turret = false;
            

        }
        
        if (enemy.Start_Bow1)
        {
            Debug.Log(Fire);

                Arrow1 = Instantiate(Arrow, Arrow_StartPosition.position, Quaternion.identity);
            Destroy(Arrow1, 3f);
            if(Arrow1!=null)
            {
                Arrow1.transform.LookAt(Target);
            }
               
          

              
            In_Shooting = true;
            enemy.Start_Bow1 = false;
        }
        if (enemy.Finsh_Bow1)
        {
            Debug.Log("Restart");
            enemy.RestartMove();
            enemy.Finsh_Bow1 = false;
        }
        if(In_Shooting)
        {
            IfArrowImpact = true;
            if (Arrow1 != null)
            {
                Arrow1.transform.position = Vector3.Lerp(Arrow1.transform.position, Arrow_ArrivePosition, 5f * Time.deltaTime);
            }
            

        }
        if (Arrow1 != null)
        {
            if (Vector3.Distance(Arrow1.transform.position, Target.transform.position) <= 2f)
            {
                if (IfArrowImpact)
                {
                    GameObject Arrow_Impact = Instantiate(Arrow_Effect, Arrow1.transform.position, Quaternion.identity);
                    Destroy(Arrow_Impact, 2f);
                    
                    Turret Turret= turrent_Target.GetComponent<Turret>();
                    Turret.Taken_Damage(Arrow_Damage);
                    In_Shooting = false;
                    IfArrowImpact = false;
                }

            }
        }



    }
    private void Reset_move()
    {
        enemy.RestartMove();
    }
    private void Set_BowShootAnim()
    {
        enemy.Enemy_Anim1.SetTrigger("Bow");
    }
    

    // Update is called once per frame
    void Update()
    {
        
        
        if (turrent_Target != null)
        {
            Shooting(turrent_Target.transform);
        }
            
        
    }
    
    
}
