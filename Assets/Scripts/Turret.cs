using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //audio
    private AudioSource audiosource;


    private bullet bullet;

    //Reset
    private float ResetAttackRate1;
    private float ResetTimer;


    private List<GameObject> enemys = new List<GameObject>();
    public float hp;
    private float max_hp;
    private bool IsAlive;
    [SerializeField] private GameObject destroySelf_FX;
    private Vector3 DestorySelf_Position;

    GameObject Current_enemy;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            
            enemys.Add(other.gameObject);
        }
       
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    public float attackRate; //times per second
    private float timer = 0;
    public GameObject bulletPerfab;
    public Transform firePosition;
    public Transform head;


    public bool useLaser = false;
    public float damageRate;// Laser attack rate
    public LineRenderer laserRanderer;
    public GameObject laserEffect;
    public TurretType turretType;

    public float Hp { get => hp; set => hp = value; }
    public float Timer { get => timer; set => timer = value; }

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        Timer = attackRate;
        IsAlive = true;

        ResetAttackRate1 = attackRate;
        ResetTimer = timer;
        
    }

  

    void Update()
    {
        if (Hp<=0)
        {
            IsAlive = false;
        }     
        if(!IsAlive)
        {

            Invoke("DestroySelf", 0.5f);
            
            IsAlive = true;
        }
         //heads direction
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        //basic turret attack
        if (useLaser == false)
        {
            //bullet attack
            Timer += Time.deltaTime;
            if (enemys.Count > 0 && Timer >= attackRate)
            {
                Timer = 0;
                Attack();
            }

        }
        else if (enemys.Count > 0)
        {
            //update laser attack behavior
            if (laserRanderer.enabled == false)
            {
                laserRanderer.enabled = true;
                laserEffect.SetActive(true);
            }

            if (enemys[0] == null)
            {
                UpdateEnemy();

            }
            if (enemys.Count > 0)
            {
                audiosource.Play();
                LaserTurretAttack();

            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRanderer.enabled = false;
            audiosource.Stop();
        }
    }
    private void DestroySelf()
    {
        Destroy(transform.gameObject);
        DestorySelf_Position = transform.position;
        DestorySelf_Position.y += 2f;
        Instantiate(destroySelf_FX, DestorySelf_Position, Quaternion.identity);
    }

    public void LaserTurretAttack()
    {
        laserRanderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
        if (turretType == TurretType.LaserTurret)
        {
            enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);//basic damage per second
        }
        if (turretType == TurretType.LaserTurretUpgrade)
        {
            enemys[0].GetComponent<Enemy>().TakeDamage(damageRate + Time.deltaTime*0.001f);//Update damage per second
        }

        laserEffect.transform.position = enemys[0].transform.position;
        //allow lasereffect look at laser turret
        Vector3 pos = transform.position;
        pos.y = enemys[0].transform.position.y;
        laserEffect.transform.LookAt(pos);
    }

    public void Attack()
    {
        if(enemys[0]!=null)
        {
            if (enemys[0].tag == "Dead")
            {
                enemys.Remove(enemys[0]);
            }

        }
        
       
        if (enemys[0] == null||enemys[0].tag=="Dead")
        {
            UpdateEnemy();

        }
        if (enemys.Count > 0)
        {
            
            GameObject bullet = GameObject.Instantiate(bulletPerfab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<bullet>().SetTarget(enemys[0].transform);
            bullet.GetComponent<bullet>().LookAtEnemy();
            audiosource.Play();
            
           
        }
        else
        {
            Timer = attackRate;
        }
        
        
       
        


    }

    void UpdateEnemy()//save all empty enemys
    {
        
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                emptyIndex.Add(i);
            }
        }
        for (int j = 0; j < emptyIndex.Count; j++)
        {
            enemys.RemoveAt(emptyIndex[j] - j);
        }
    }
    public void Taken_Damage(float damage)
    {
        if (Hp <= 0) return;
        Hp -= damage;
        
        
    }
    public void ResetAttackRate()
    {
        attackRate = ResetAttackRate1;
        timer = ResetTimer;
    }
    public void AttackRateChange(float changeValue,float ResetTime)
    {
        attackRate *= changeValue;
        

        Invoke("ResetAttackRate", ResetTime);
    }
}
