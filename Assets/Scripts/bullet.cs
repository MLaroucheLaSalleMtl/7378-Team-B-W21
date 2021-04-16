using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public AudioClip normal;
    //public AudioClip snow;
    //public AudioClip fire;
    //AudioSource audiosource;

     //Hongfei Liu
    public enum Type 
    {
        standard_bullet,
        fireball,
        fireball_Upgrade,
        snowball,
        snowBall_Upgrade,
        ThunderBall,
        ThunderBall_Upgrade
    }

    public Type type;

    public float damage = 50;
    public float continuesDamage = 10;
    public float speed = 20;
    private Transform target;
    private Enemy enemy;
    public GameObject explosionEffectPrefab;
    private GameObject Burn_Effect;
    private Enemy Current_Enemy;
    private bool IfSetValue;
    private bool FindTarget;
    private List<Enemy> enemylist = new List<Enemy>();
    //-------------------------------------------------

    //Peixin Li
    [SerializeField] private GameObject FireImpact_FX;
    [SerializeField] private GameObject upGradeBulletEffectPrefab;
    [SerializeField] private GameObject ReduceMove_FX;
    [SerializeField] private GameObject ReduceMove_Area;
    [SerializeField] private GameObject ThunderLightingFX;
    [SerializeField] private float StopMoveTime;
    [SerializeField] private int ProbabilityPercentForThunder;
    
    public GameObject Burn_Effect1 { get => Burn_Effect; set => Burn_Effect = value; }
    //-----------------------------------------------

    //Hongfei Liu
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audiosource = GetComponent<AudioSource>();

        IfSetValue = false;
        FindTarget = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        
        
            LookAtEnemy();
        
       
        
        

    }

    public void LookAtEnemy()
    {
        transform.LookAt(target.transform.position,target.transform.forward);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    private void Impact_FX(GameObject FX)
    {
        GameObject Impact = Instantiate(FX, transform.position, Quaternion.identity);
        Destroy(Impact, 1f);
        
    }
    private void ReSetBurning()
    {
      if(Current_Enemy.InBurning1)
        {
            Current_Enemy.InBurning1 = false;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "enemy")
        {
            if(!FindTarget)
            {
                FindTarget = true;
                Current_Enemy = other.GetComponent<Enemy>();
            }
            
            if (type == Type.standard_bullet)
            {
                
                //audiosource.PlayOneShot(clip1);
                other.GetComponent<Enemy>().TakeDamage(damage);
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            if (type == Type.fireball && !IfSetValue)
            {
                IfSetValue = true;

                Impact_FX(FireImpact_FX);
                if (Current_Enemy.InBurning1 == false)
                {
                    Current_Enemy.InBurning1 = true;
                    Burn_Effect1 = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                    Burn_Effect1.transform.parent = other.transform;
                  
                    
                        Invoke("ReSetBurning", 5f);
                }
                   
                
                Destroy(Burn_Effect1, 5f);
                    Destroy(this.gameObject,6f);
               
                
            }
            if (type == Type.fireball_Upgrade)
            {
                IfSetValue = true;
                    Impact_FX(FireImpact_FX);

                //if (Current_Enemy.InBurning1 == false)
                //{
                //    Current_Enemy.InBurning1 = true;
                //    Burn_Effect1 = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                //    Burn_Effect1.transform.parent = other.transform;
                //    Invoke("ResetBurning", 5f);
                //    Destroy(Burn_Effect1, 5f);
                //}
                
                    GameObject upGradeEffect = GameObject.Instantiate(upGradeBulletEffectPrefab, transform.position, transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f));//Euler: to edit rotation
                    Destroy(upGradeEffect, 0.7f);
                
                
                  
                    Destroy(this.gameObject);
               
                
            }
            if (type == Type.snowball||type==Type.snowBall_Upgrade)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                
                    enemy.TakeSnowBallDamage(damage);                                              
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
               
                    if(!enemy.IsReducing1&&enemy.transform.parent==null)
                {
                    //GameObject ReduceFX = Instantiate(ReduceMove_FX, other.transform.position, Quaternion.identity);
                    //ReduceFX.transform.parent = other.transform;
                    //Destroy(ReduceFX, 5f);
                    
                    enemy.IsReducing1 = true;
                }

                
                
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            if(type==Type.snowBall_Upgrade)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                Vector3 ReduceAreaPosition = enemy.transform.position;
                ReduceAreaPosition.y += 1f;
                GameObject ReduceArea = Instantiate(ReduceMove_Area, ReduceAreaPosition, Quaternion.identity);
                Destroy(ReduceArea, 5f);
            }
            if(type==Type.ThunderBall)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                GameObject effect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                //GameObject Lighting = Instantiate(LightingChain, transform.position, Quaternion.identity);
               
                if(CalculateTheProbability(ProbabilityPercentForThunder))      //for example   there is 20% percent to instantiate a lighting to stop enemy moving and clear their energy.
                {
                    GameObject Lighting = Instantiate(ThunderLightingFX, other.transform.position, Quaternion.identity);
                    Destroy(Lighting, 2f);
                    enemy.TakeThunderDamage(damage*0.5f, StopMoveTime);
                }
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }





        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (type == Type.fireball )
            {
                if (other.GetComponent<Enemy>() != null)
                {
                    
                        other.GetComponent<Enemy>().TakeContinuesDamage(damage, continuesDamage);
                    
                 
                }

            }

        }

    }
    //-----------------------------------------------

    //Peixin Li
    private bool CalculateTheProbability(int ProbabilityPercent)
    {
        
        int RandomNum = Random.Range(0, 99);
        if(RandomNum<=ProbabilityPercent-1)
        {
            return true;
        }
        if(RandomNum > ProbabilityPercent - 1)
        {
            return false;
        }
        return false;

    }
    //-----------------------------------------------
    
}
