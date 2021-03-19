using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GoToEndPosition : Enemy_property
{
    NavMeshAgent Agent;
    public Transform Target;
    Vector3 Enemy_Velocity;
    Animator Enemy_Anim;
    Enemy_property Enemy_prop;
    bool IsLose;
    bool IsDead;
    GameObject next_subpath1;
    GameObject next_subpath2;


    public Slider Enemy_slider;
    


    //FireBall
    [SerializeField] private  GameObject FireBall;
    private Vector3 FireBall_Position;
    private bool Use_FireBall;  //when the enemy be close with a turrent the enemy will stop walking and use fireball once then walk away.
    

    bool onetimes_forCards;
    [SerializeField] private GameObject[] Cards;
    private int MyCard;


    //wavepoints

    private Transform[] positions;
    private int Point_Index;
    private GameObject[] point_gameobject;
    private SetsubpathBySubPath[] subpath;
    private bool IfarrivePoint;              //if this is true that mean enemy arrive any points position. if this is false that mean enemy exit any points position.
    private bool IfFinishSubPaths;
    private bool InSubPath;        //if in subpath so only run relative subpath fuction.
    
    public bool IfFinishSubPaths1 { get => IfFinishSubPaths; set => IfFinishSubPaths = value; }

    void Start()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        Set_NavSpeed();
        
        Enemy_Anim = GetComponent<Animator>();
        IsLose = false;
        Set_Property();
        IsDead = false;
        Use_FireBall = false;
        positions = WavePoints.position;
        onetimes_forCards = false;
        Point_Index = 0;

        point_gameobject = WavePoints.points_gameobject;
        subpath = new SetsubpathBySubPath[point_gameobject.Length];
        
        InSubPath = false;


        for (int i=0; i < point_gameobject.Length;i++)
        {
            // subpath=point_gameobject.getcompoent
           
            subpath[i] = point_gameobject[i].GetComponent<SetsubpathBySubPath>();
            //Debug.Log("GOOD");
        }


        IfFinishSubPaths1 = false;
    }
 
    
    void move()
    {

        Debug.Log(Point_Index);
        Debug.Log(IfFinishSubPaths1);
        if (!InSubPath)
        {
           
                Agent.destination = positions[Point_Index].position;
                Debug.Log("Go to next position");
            
           
        }
        if (IfFinishSubPaths1)
        {
            IfFinishSubPaths1 = false;
         

            
            InSubPath = false;
        }


        if (IfarrivePoint)
        {
            IfarrivePoint = false;
            if (subpath[Point_Index].NextPathIsSubpath)
            {
                InSubPath = true;
                int subpath_length = subpath[Point_Index].Subpaths.Length;

                Agent.destination = subpath[Point_Index].Subpaths[Random_Fromsubpaths(subpath_length)].transform.position;
                Debug.Log("go to random subpath position");
                if (Point_Index < positions.Length)
                {
                    Point_Index++;
                }
                //;
                //int subpath_length = subpath[Point_Index].Subpaths.Length;
                //Debug.Log(Random_Fromsubpaths(subpath_length));
                //subpath[Point_Index].Block_other_subpaths(Random_Fromsubpaths(subpath_length));  //block the other 


            }
            if(!subpath[Point_Index].NextPathIsSubpath)
            {
                if (Point_Index < positions.Length-1)
                {
                    Point_Index++;
                }
            }
            
           
        }
        


    }
   
    private int Random_Fromsubpaths(int Subpaths_indexlength)
    {
     
        int Get_RandomIndex;
       
        Get_RandomIndex = Random.Range(0, Subpaths_indexlength);
        return Get_RandomIndex;



    }
    // Update is called once per frame
    void Update()
    {
        move();
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
            Drop_rate();

            if (!onetimes_forCards)
            {
                
                create_Card();
                onetimes_forCards = true;
            }
            Destroy(transform.gameObject, 4f);
        }

    }
    private void Attack()
    {
        
        if (Use_FireBall)
        {
            StopMoving();
            FireBall_Position = Vector3.forward * 1.5f;
            Instantiate(FireBall, FireBall_Position, Quaternion.identity);
            Use_FireBall = false;
        }
    }
    public void Taken_Damage(float damage)
    {
        if (this.Hp > 0)
        {
            this.Hp -= damage;
            if(this.Hp<=0)
            {
                this.Hp = 0;
            }
        }
    }
    private void Reset_Move()
    {
        Agent.destination = Target.position;
    }
    private void StopMoving()
    {
        Agent.ResetPath();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turrent")
        {
            Use_FireBall = true;
        }
        if(other.tag=="WayPoints"&&!InSubPath)
        {
            IfarrivePoint = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WayPoints" && !InSubPath)
        {
            IfarrivePoint = false;
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
    
    private bool Drop_rate()
    {

        //rate = Random.Range(0, 100);
        //return rate <= 5;r
        return false;

    }


    private int Get_Random()
    {
        
        int Index;
        Index = Random.Range(0, 3);
        return Index;
    }

    private void create_Card()
    {
        Debug.Log("create");
        int index;
        index = Get_Random();
        Instantiate(Cards[index], transform.position, Quaternion.identity);

        //if (index == 0)
        //{
        //    Hp *= 2;
        //    Move_speed *= 10;
        //}
        //if (index == 1)
        //{
        //    Hp *= 3;
        //    Move_speed *= 10;
        //}
        //if (index == 2)
        //{
        //    Hp *= 4;
        //    Move_speed *= 10;
        //}

    }
    
   
}
