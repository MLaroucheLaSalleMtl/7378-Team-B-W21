using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float hp = 150;
    private float Max_HP;
    public GameObject explosionEffect;
    public Slider hpSlider;
    private Transform[] positions;
    private int index = 0;

    //By PeiXin
    private bool IsAlive;
    Vector3 Enemy_Velocity;
    Animator Enemy_Anim;

    //Card
    [SerializeField] private GameObject[] Cards;

    public float Speed { get => speed; set => speed = value; }
    public float Hp { get => hp; set => hp = value; }
    public float Max_HP1 { get => Max_HP; set => Max_HP = value; }

    // Start is called before the first frame update
    void Start()
    {      
        positions = WavePoints.position;
        Max_HP1 = Hp;
        

        //PeiXin
        Enemy_Anim = GetComponent<Animator>();
        IsAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            Move();
            
            
        }
    }
    
    private void Set_Alive()   //when the enemy HP decrease to 0 ,the enemy die by PeiXin
    {
        
        if (this.Hp<=0)
        {
            IsAlive = false;
        }
       
    }
    private void Enemy_die()   //PeiXin
    {
        Enemy_Anim.SetBool("IsDead", true);
        Invoke("Destroy_Gameobject", 3f);
    }
    private void Destroy_Gameobject()
    {
        Destroy(this.gameObject);
    }

    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * Speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            
            index++;
            

        }
        
        
        //ckeck enemy reach last point
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    /// <summary>
    //reach to the final target and game over
    /// <summary>
    void ReachDestination()
    {
       
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.EnemyCount--;
    }

    public void TakeDamage(float damage)
    {
        if (Hp <= 0) return;
        Hp -= damage;
        hpSlider.value = (float)Hp / Max_HP1;
        if (Hp <= 0)
        {
            Die();
            BuildManager.money += 10;
        }
    }
    public void Die()
    {

        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);

    }


    //Card systems  ||↓ ↓ ↓ ↓ ↓ ↓ ↓ 
    private int Get_Random()
    {
        int Index;
        Index = Random.Range(0, 3);
        return Index;
    }
    private void create_Card()
    {
        int index;
        index = Get_Random();
        Instantiate(Cards[index], transform.position, Quaternion.identity);

        //if(index==0)
        //{
        //    Hp *= 2;
        //    Move_speed *= 10;
        //}
        //if(index==1)
        //{
        //    Hp *= 3;
        //    Move_speed *= 10;
        //}
        //if(index==2)
        //{
        //    Hp *= 4;
        //    Move_speed *= 10;
        //}

    }
}
