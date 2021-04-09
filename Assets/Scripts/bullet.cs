using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public AudioClip normal;
    //public AudioClip snow;
    //public AudioClip fire;
    //AudioSource audiosource;

    public enum Type 
    {
        standard_bullet,
        fireball,
        fireball_Upgrade,
        snowball,
        snowBall_Upgrade
    }

    public Type type;

    public int damage = 50;
    public float continuesDamage = 10;
    public float speed = 20;
    private Transform target;
    private Enemy enemy;
    public GameObject explosionEffectPrefab;
    private GameObject Burn_Effect;
    private Enemy Current_Enemy;
    [SerializeField] private GameObject FireImpact_FX;
    [SerializeField] private GameObject upGradeBulletEffectPrefab;

    [SerializeField] private GameObject ReduceMove_FX;
    [SerializeField] private GameObject ReduceMove_Area;
    

    public GameObject Burn_Effect1 { get => Burn_Effect; set => Burn_Effect = value; }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audiosource = GetComponent<AudioSource>();
        
    }

    //public void Playnormalvoice()
    //{
    //    audiosource.PlayOneShot(normal);
    //}

    //public void Playsnowvoice()
    //{
    //    audiosource.PlayOneShot(snow);
    //}

    //public void Playfirevoice()
    //{
    //    audiosource.PlayOneShot(fire);
    //}

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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "enemy")
        {
            Current_Enemy = other.GetComponent<Enemy>();
            if (type == Type.standard_bullet)
            {
                
                //audiosource.PlayOneShot(clip1);
                other.GetComponent<Enemy>().TakeDamage(damage);
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            if (type == Type.fireball)
            {
                Impact_FX(FireImpact_FX);
                Burn_Effect1 = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Burn_Effect1.transform.parent = this.transform;
                Destroy(Burn_Effect1, 5f);
                Destroy(this.gameObject, 6f);
            }
            if (type == Type.fireball_Upgrade)
            {
                Impact_FX(FireImpact_FX);
                Burn_Effect1 = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Burn_Effect1.transform.parent = this.transform;
                GameObject upGradeEffect = GameObject.Instantiate(upGradeBulletEffectPrefab, transform.position, transform.rotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f));//Euler: to edit rotation
                Destroy(upGradeEffect, 1f);
                Destroy(Burn_Effect1, 5f);
                Destroy(this.gameObject, 6f);
            }
            if (type == Type.snowball||type==Type.snowBall_Upgrade)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeSnowBallDamage(damage);
                
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
               
                    if(!enemy.IsReducing1&&enemy.transform.parent==null)
                {
                    GameObject ReduceFX = Instantiate(ReduceMove_FX, other.transform.position, Quaternion.identity);
                    ReduceFX.transform.parent = other.transform;
                    Destroy(ReduceFX, 5f);
                    
                    enemy.IsReducing1 = true;
                }

                
                
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            if(type==Type.snowBall_Upgrade)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                Vector3 ReduceAreaPosition = enemy.transform.position;
                ReduceAreaPosition.y += 4f;
                GameObject ReduceArea = Instantiate(ReduceMove_Area, ReduceAreaPosition, Quaternion.identity);
                Destroy(ReduceArea, 5f);
            }




        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (type == Type.fireball || type == Type.fireball_Upgrade)
            {              
                if(Current_Enemy!=null)
                {
                    Current_Enemy.TakeContinuesDamage(damage, continuesDamage);
                }
                                          
                                             
            }
            //if (type == Type.fireball_Upgrade)
            //{
            //    //audiosource.PlayOneShot(clip3);
            //    Current_Enemy.TakeContinuesDamage(damage, continuesDamage);
            //    Destroy(this.gameObject, 5f);
            //}
            


        }

    }
}
