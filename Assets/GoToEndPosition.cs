using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToEndPosition : Enemy_property
{
    NavMeshAgent Agent;
    public Transform Target;
    Vector3 Enemy_Velocity;
    Animator Enemy_Anim;
    Enemy_property Enemy_prop;
    bool IsLose;
    bool IsDead;

    bool onetimes_forCards;
    [SerializeField] private GameObject[] Cards;
    private int MyCard;
    void Start()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        Set_NavSpeed();
        Agent.destination=Target.position;
        Enemy_Anim = GetComponent<Animator>();
        IsLose = false;
        Set_Property();
        Debug.Log(Hp);
        IsDead = false;

        onetimes_forCards = false;
        
    }
 
    

    // Update is called once per frame
    void Update()
    {
        Enemy_Anim.SetBool("IsDead",IsDead);
        //Debug.Log(Enemy_Velocity.magnitude);
        Enemy_Velocity = Agent.velocity;
        Enemy_Anim.SetFloat("Magnitude",Enemy_Velocity.magnitude);
        if(Enemy_Velocity.magnitude<=0.1)
        {
            IsLose = true;
        }
        if(this.Hp==0)
        {
            IsDead = true;
            
        }
        if(IsDead)
        {
            //if(!onetimes_forCards)
            //{
            //    create_Card();
            //    onetimes_forCards = true;
            //}
            Destroy(transform.gameObject, 4f);
        }

    }
    private void Set_NavSpeed()
    {
        Agent.speed = Move_speed;
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Home")
        {
            Destroy(collision.gameObject);
        }
    }
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
