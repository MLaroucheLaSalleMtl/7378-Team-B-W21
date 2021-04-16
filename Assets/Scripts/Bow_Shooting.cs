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

    //for children enemy
    private Animator Anim;
    [SerializeField] private float Damage;
    [SerializeField] bool IsChildrenEnemy;
     private float Energy;
    [SerializeField] private float EnergySpeed;
    private bool StartShoot;
    private bool FinishShoot;
    private float Max_AttackRate;
    private Fire_ShootReceiver Fire_receiver;
    [SerializeField] private Enemy ParentEnemy;
    void Start()
    {
        
        StartShoot = false;
        FinishShoot = false;
        if(IsChildrenEnemy)
        {
            Anim = Enemy_GameObject.GetComponent<Animator>();
            Arrow_Damage = Damage;
            Max_AttackRate = Energy;
            Fire_receiver = Enemy_GameObject.GetComponent<Fire_ShootReceiver>();
        }
        if(!IsChildrenEnemy)
        {
            enemy = Enemy_GameObject.GetComponent<Enemy>();
            Arrow_Damage = enemy.Normal_damage;
        }
        
        Fire = false;
        Finish_Fire = false;
        Find_Turret = false;

        In_Shooting = false;
        IfArrowImpact = false;
        Arrow1 = Arrow;
        
        ReMove_Timer = 3f;
        Reset_ReMoveTimer = ReMove_Timer;
    }
  
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Turrent")
        {
            
           

                if (!IsChildrenEnemy)
                {

                    if (enemy.Energy1 >= 100 && !Find_Turret)
                    {
                        Find_Turret = true;
                        enemy.Energy1 = 0;
                        enemy.StopMove();
                        Invoke("Reset_move", 2f);

                        turrent_Target = other.transform.gameObject;
                        Arrow_ArrivePosition = turrent_Target.transform.position;
                        Arrow_ArrivePosition.y += 2f;

                    }

                }
                if (IsChildrenEnemy)
                {

                    if (ParentEnemy.Energy1 >= 100 && !Find_Turret)
                    {
                        Find_Turret = true;
                        ParentEnemy.Energy1 = 0f;
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
        
        if(!IsChildrenEnemy)
        {
            if (enemy.Start_Bow1)
            {
                Arrow1 = Instantiate(Arrow, Arrow_StartPosition.position, Quaternion.identity);
                Destroy(Arrow1, 2f);
                if (Arrow1 != null)
                {
                    Arrow1.transform.LookAt(Target);
                }
                In_Shooting = true;
                enemy.Start_Bow1 = false;
            }
            if (enemy.Finsh_Bow1)
            {
                enemy.RestartMove();
                enemy.Finsh_Bow1 = false;
            }
        }
        if(IsChildrenEnemy)
        {
            if (Fire_receiver.StartShoot1)
            {
                Arrow1 = Instantiate(Arrow, Arrow_StartPosition.position, Quaternion.identity);
                Destroy(Arrow1, 2f);
                if (Arrow1 != null)
                {
                    Arrow1.transform.LookAt(Target);
                }
                In_Shooting = true;
                Fire_receiver.StartShoot1 = false;
            }
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
                    Destroy(Arrow_Impact, 1f);
                    
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
        if(!IsChildrenEnemy)
        {
            enemy.Enemy_Anim1.SetTrigger("Bow");
        }
        if(IsChildrenEnemy)
        {
            Anim.SetTrigger("Bow");
        }
        
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
