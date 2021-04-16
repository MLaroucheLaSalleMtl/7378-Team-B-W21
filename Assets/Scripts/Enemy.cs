using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : Enemy_property
{
    //Hongfei Liu
    private float halfSpeed;
    public GameObject explosionEffect;
    //Shiyu Lyu
    public Slider hpSlider;
    public Slider EnergySlider;
    [SerializeField] private GameObject SliderCanvas;

    //Hongfei Liu
    private bool CanTakenFirstDamage;//for continue damage to make sure take one damage and take a few times continue damage.
    private float ContinueDamage_Timer;  //how many seconds you want to run the ContinueDamage
    private float Max_Continuedamage_timer;
    private bool IsFinish_ContinueDamage_Timer;

    //Peixin Li
    private bool CantKill;
    [SerializeField] private AudioSource ThunderAudio;
    private bool Find_Portal;
    private Transform Portal_Position;
    [SerializeField] private GameObject Destination_FX;
    [SerializeField] private GameObject ReducingMoveFX;
    [Header("Revived")]
    private bool IfArriveFirstDes;
    private Get_PortalTarget Get_Portal;
    private bool IsarrivePortal;
    public int rate;
    private NavMeshAgent Agent;
    Vector3 Enemy_Velocity;
    Animator Enemy_Anim;
    Enemy_property Enemy_prop;
    bool IsLose;
    bool IsDead;
    bool IsFinishDead;

    //Shiyu Lyu
    bool onetimes_forCards;

    //Peixin Li
    //Enemy State
    private bool IsReducing;
    private Transform[] positions;
    private int Point_Index;
    private GameObject[] point_gameobject;
    private SetsubpathBySubPath[] MainPath;
    private bool IfarrivePoint;              //if this is true that mean enemy arrive any points position. if this is false that mean enemy exit any points position.
    private bool IfFinishSubPaths;
    private bool InSubPath;        //if in subpath so only run relative subpath fuction.
    private Vector3 Current_Destination;
    [SerializeField] private bool CanMove;

    //for undead enemy
    [SerializeField] private bool IfUndead;

    [SerializeField] private bool IsBeRevived;

    //Enemy Status
    private bool InBurning;

    public bool IfFinishSubPaths1 { get => IfFinishSubPaths; set => IfFinishSubPaths = value; }
    public bool CanMove1 { get => CanMove; set => CanMove = value; }
    public NavMeshAgent Agent1 { get => Agent; set => Agent = value; }
    public bool Find_Portal1 { get => Find_Portal; set => Find_Portal = value; }
    public Transform Portal_Position1 { get => Portal_Position; set => Portal_Position = value; }
    public Animator Enemy_Anim1 { get => Enemy_Anim; set => Enemy_Anim = value; }
    public bool Start_Bow1 { get => Start_Bow; set => Start_Bow = value; }
    public bool Finsh_Bow1 { get => Finsh_Bow; set => Finsh_Bow = value; }
    public bool IsReducing1 { get => IsReducing; set => IsReducing = value; }
    public bool InSubPath1 { get => InSubPath; set => InSubPath = value; }
    public bool IfarrivePoint1 { get => IfarrivePoint; set => IfarrivePoint = value; }
    public int Point_Index1 { get => Point_Index; set => Point_Index = value; }
    public bool IsDead1 { get => IsDead; set => IsDead = value; }
    public Vector3 Current_Destination1 { get => Current_Destination; set => Current_Destination = value; }
    public bool IsBeRevived1 { get => IsBeRevived; set => IsBeRevived = value; }
    public bool InBurning1 { get => InBurning; set => InBurning = value; }
    public bool CantKill1 { get => CantKill; set => CantKill = value; }


    //Shiyu Lyu
    [SerializeField] private GameObject[] Cards;
    //-------------------------------------

    //Peixin Li
    private bool IfResetEnergy;
    private bool Start_Bow;
    private bool Finsh_Bow;
    //-------------------------------------
    private void Awake()
    {
        if(IsBeRevived1)
        { CantKill1 = true; }
        
        
    }
    void Start()
    {
        ThunderAudio = GetComponent<AudioSource>();
        //Peixin Li
        InBurning1 = false;
        IfArriveFirstDes = false;
        if(!IsBeRevived1)
        {
            IsarrivePortal = false;
            InSubPath1 = false;
            Point_Index1 = 0;
            IfFinishSubPaths1 = false;
        }
        IsReducing1 = false;
        IsFinishDead = false;
        
        positions = WavePoints.position;
        
            Max_hp = Hp;
        
        
        halfSpeed = Move_speed * 0.5f;
        CanTakenFirstDamage = true;
        //PeiXin
        
        Agent1 = GetComponent<NavMeshAgent>();
        Enemy_Anim1 = GetComponent<Animator>();
        IsDead1 = false;

        onetimes_forCards = false;
        ContinueDamage_Timer = 5f;
        Max_Continuedamage_timer = ContinueDamage_Timer;

        Set_NavSpeed();
        IsLose = false;
        Set_Level();
        IsDead1 = false;
        

        point_gameobject = WavePoints.points_gameobject;
        MainPath = new SetsubpathBySubPath[point_gameobject.Length];
        if(IsBeRevived1)
        {
            Invoke("ResetCantKill", 3f);
        }
        

        
      

        //Bomber

        for (int i = 0; i < point_gameobject.Length; i++)
        {
            MainPath[i] = point_gameobject[i].GetComponent<SetsubpathBySubPath>();

        }

      

        Max_Energy1 = 100f;
        Energy1 = 0f;
        if (EnemyType == enemy_type.SkeletonArcher || EnemyType == enemy_type.GhostShooter)
        {
            Energy1 = 90f;
        }

        //bow
        Start_Bow1 = false;
        Finsh_Bow1 = false;



    }
    //-------------------------------------

    //Hongfei Liu
    private void SetSlider()
    {
        if(!IsDead1)
        {
            hpSlider.value = Hp / Max_hp;
            if(IfUsingEnergy1)
            {
                EnergySlider.value = Energy1 / Max_Energy1;
            }
            
        }
        
    }
    //-------------------------------------

    // Peixin Li
    void Update()
    {
        SetSlider();
        if (IfUsingEnergy1)
        {
            EnergyIncreasing();
            if (Energy1 >= 100f)
            {
                //Invoke("ResetEnergy", 2f);
            }
        }
        if (!Find_Portal1)
        {
            
            if (CanMove1)
            {

                Move();
            }
        }
        if (Find_Portal1)
        {
            if (Portal_Position1 != null)
            {
                if (CanMove1)
                {
                    Move(Portal_Position1);
                }
            }
        }
        //if(Find_Portal1)
        //{
        //    if(Portal_Position1!=null)
        //    {
        //        Move(Portal_Position1);
        //    }
        //}

        Enemy_Anim1.SetBool("IsDead", IsDead1);
        //Debug.Log(Enemy_Velocity.magnitude);
        Enemy_Velocity = Agent1.velocity;
        Enemy_Anim1.SetFloat("Magnitude", Enemy_Velocity.magnitude);
        if (Enemy_Velocity.magnitude <= 0.1)
        {
            IsLose = true;
        }
        if (this.Hp <= 0)
        {
            IsDead1 = true;

        }
        if (IsDead1&&!IsFinishDead)
        {
            IsFinishDead = true;
            Die();
            
            
            
            
        }
        
        
    }
    public void GetMovingValueFromOthers(Enemy enemy)
    {
        
        this.InSubPath1 = enemy.InSubPath1;
        this.IfFinishSubPaths1 = enemy.IfFinishSubPaths1;
        this.IfarrivePoint1 = enemy.IfarrivePoint1;
        this.Point_Index1 = enemy.Point_Index1;
        this.Find_Portal1 = enemy.Find_Portal1;
        this.Portal_Position1 = enemy.Portal_Position1;

        this.Current_Destination1 = enemy.Current_Destination1;

        if (Current_Destination1 != null)
        {
            
            Invoke("SetCurrentDestination", 1f);


        }
        EnemySpawner.EnemyCount++;
        






        Debug.Log(Point_Index1);
        
        //this.Agent1.destination = enemy.Agent1.destination;
        
        
    }
    private void SetCurrentDestination()
    {
        Agent1.destination = Current_Destination1;
    }
    private void Set_NavSpeed()
    {
        Agent1.speed = Move_speed;

    }
    private void EnergyIncreasing()
    {
        if (Energy1 >= Max_Energy1) return;

        if (Energy1 < Max_Energy1)
        {
            Energy1 += Time.deltaTime * EnergySpeed1;

        }
        EnergySlider.value = Energy1 / Max_Energy1;
    }
    //private void ResetEnergy()
    //{
    //    if(Energy1>=Max_Energy1)
    //    {
    //        Energy1 = 0f;
    //    }
    //    Debug.Log("Reset!!!!");
    //}
    public void StopMove()
    {
        if(CanMove1)
        {
            CanMove1 = false;
            Current_Destination1 = Agent1.destination;
            Agent1.ResetPath();
        }
       
    }
    public void RestartMove()
    {
        if(!IsDead1)
        {
            CanMove1 = true;
            if (Current_Destination1 != null)
            {
                Agent1.destination = Current_Destination1;
            }
            if (Current_Destination1 == null)
            {

            }
        }
        
       

    }

    private void Destroy_Gameobject()
    {
        Destroy(this.gameObject);
    }



    void Move()
    {

        if(!IsBeRevived1||IfArriveFirstDes)
        {
            if (!InSubPath1)
            {
                Current_Destination1= positions[Point_Index1].position;
                Agent1.destination = Current_Destination1;



            }
        }
       if(IsBeRevived1&&!IfArriveFirstDes)
        {
            Agent1.destination = Current_Destination1;
            if(Vector3.Distance(transform.position,Current_Destination1)<=2f)
            {
                IfArriveFirstDes = true;
            }
        }
        if (IfFinishSubPaths1)
        {
            IfFinishSubPaths1 = false;
            InSubPath1 = false;

        }


        if (IfarrivePoint1)
        {
            IfarrivePoint1 = false;
            if (MainPath[Point_Index1].NextPathIsSubpath)
            {
                InSubPath1 = true;
                int subpath_length = MainPath[Point_Index1].Subpaths.Length;
                Current_Destination1= MainPath[Point_Index1].Subpaths[Random_Fromsubpaths(subpath_length)].transform.position;
                Agent1.destination = Current_Destination1;
                if (Point_Index1 < positions.Length)
                {
                    Point_Index1++;
                }
            }
            if (!MainPath[Point_Index1].NextPathIsSubpath)
            {
                if (Point_Index1 < positions.Length - 1)
                {
                    Point_Index1++;
                }
            }
        }
    }
    void Move(Transform Target)
    {
        Agent1.destination = Target.position;
    }
    private int Random_Fromsubpaths(int Subpaths_indexlength)
    {
        int Get_RandomIndex;

        Get_RandomIndex = Random.Range(0, Subpaths_indexlength);
        return Get_RandomIndex;
    }

    /// <summary>
    //reach to the final target and game over
    /// <summary>
    public void ReachDestination()
    {

        //LevelManager.Instance.Fail();
        GameObject.Destroy(this.gameObject); 

    }
    private void ArrivePortal()
    {
        Vector3 Destination_Position;
        Destination_Position = transform.position;
        Destination_Position.y += 2;
        GameObject Portal_FX = Instantiate(Destination_FX, Destination_Position, Quaternion.identity);
        LevelManager.Instance.Basement_HP1 -= this.basement_damage;
        Debug.Log("Reduce Basement HP " + this.basement_damage);

        
            EnemySpawner.EnemyCount--;
        
        
        GameObject.Destroy(Portal_FX, 2f);
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        //EnemySpawner.EnemyCount--;
    }
    //-------------------------------------

    //Hongfei Liu
    public void TakeDamage(float damage)
    {
        if(this.tag!="Dead")
        {
            if (Hp <= 0||CantKill1) return;

            Hp = Hp - DefenceDamage(damage);

            hpSlider.value = (float)Hp / Max_hp;
        }
        
        
    }
    private void ResetCantKill()
    {
        CantKill1 = false;
    }
    public float DefenceDamage(float damage)
    {
        float NewDamage = 0f;
        float Taken_DamagePercentage = (100 - Defence)/100 ;
        if(Taken_DamagePercentage>=0&&Taken_DamagePercentage<=1)
        {
             NewDamage= damage * Taken_DamagePercentage;
            
        }
        return NewDamage;

    }
    public void TakenDamagePerSeconds(float damage)
    {
        if (Hp <= 0||this.tag=="dead") return;
        Hp -= DefenceDamage(damage) *Time.deltaTime;

        hpSlider.value = (float)Hp / Max_hp;
        
    }
    public void TakeContinuesDamage(float First_damage, float Continues_damage)
    {
        if (this.tag == "Dead") return;
            if (CanTakenFirstDamage && ContinueDamage_Timer > 0f)
        {
            TakeDamage(First_damage);
            CanTakenFirstDamage = false;
            IsFinish_ContinueDamage_Timer = false;
        }
        if (!CanTakenFirstDamage)
        {

            if (!IsFinish_ContinueDamage_Timer)
            {
                ContinueDamage_Timer -= Time.deltaTime;
                TakenDamagePerSeconds(Continues_damage);

            }
            if (ContinueDamage_Timer <= 0f)
            {
                IsFinish_ContinueDamage_Timer = true;
                Reset_ContinueDamage_Timer();

            }
        }


    }
    //-------------------------------------


    //Peixin Li
    public void ReSetMoveSpeed()
    {
        Agent1.speed = Move_speed;
        IsReducing1 = false;
    }
    public void ResetReStartMove(float ResetTime)
    {
        Invoke("RestartMove", ResetTime);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Turrent")
        //{
        //    Use_FireBall = true;
        //}
        if (other.tag == "WayPoints" && !InSubPath1)
        {
            IfarrivePoint1 = true;
        }
        if (other.tag == "PortalTrigger")
        {
            Get_Portal = other.GetComponent<Get_PortalTarget>();
            Portal_Position1 = Get_Portal.Portal_Target1;
            Find_Portal1 = true;
        }
        if (other.tag == "Portal")
        {

            ArrivePortal();

        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WayPoints" && !InSubPath1)
        {
            IfarrivePoint1 = false;
        }
    }
    
    public void Reset_ContinueDamage_Timer()
    {
        ContinueDamage_Timer = Max_Continuedamage_timer;
        CanTakenFirstDamage = true;
    }
    //-------------------------------------

    //Hongfei Liu
    public void TakeSnowBallDamage(float damage)
    {

        if (Hp <= 0||this.tag=="Dead") return;
        TakeDamage(damage);
        if(!IsReducing1)
        {
            Agent1.speed = halfSpeed;
            GameObject ReducingFX = Instantiate(ReducingMoveFX, transform.position, Quaternion.identity);
            ReducingFX.transform.parent = transform;
            Destroy(ReducingFX, 5f);
            Invoke("ReSetMoveSpeed", 5f);
            IsReducing1 = true;
        }
        
    }
    //-------------------------------------

    // Peixin Li
    public void TakeThunderDamage(float damage,float StopTime)
    {
        if(this.tag!="Dead")
        {
            TakeDamage(damage);
            Energy1 = 0f;
            StopMove();
            ResetReStartMove(StopTime);
            ThunderAudio.Play();
        }
        
    }
    public void Die()
    {
        //Drop();
        StopMove();
        BuildManager.money += Cost1;
        Vector3 explosionEffectPosition = transform.position;
        explosionEffectPosition.y += 2f;
        //Current_Destination1 = Agent1.destination;
        GameObject effect = GameObject.Instantiate(explosionEffect, explosionEffectPosition, transform.rotation);
        Destroy(effect, 1);
        Drop();
        if (!IfUndead)
        {
           
            Destroy(this.gameObject, 2f);
            
           
        }
        if(IfUndead)
        {
           
            
            SliderCanvas.SetActive(false);
            tag = "Dead";
            Destroy(this.gameObject, 30f);
        }


        EnemySpawner.EnemyCount--;

    }
    //-------------------------------------

    //Shiyu Lyu
    private int Get_Random()
    {
        int Index;
        Index = Random.Range(0, 4);
        return Index;
    } 

    private void create_Card()
    {
        int index;
        index = Get_Random();
        Vector3 cardposition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        Instantiate(Cards[index], cardposition, Quaternion.identity);

    }

    private void Drop()
    {

        rate = Random.Range(0, 101);
        if (rate <= 5)
        {
            create_Card();
        }
    }



    //public GameObject[] dropitems;
    //float droprate = 0.25f;

    //public void DropItem()
    //{
    //    if (Random.Range(0f, 1f) <= droprate)
    //    {
    //        int indexToDrop = Random.Range(0, dropItems.Length);
    //        Instantiate(dropitems[indexToDrop], this.transform.position, this.transform.rotation);
    //    }
    //}


    //-------------------------------------

    //Peixin Li
    //animation events
    public void Finish_BowShoot()
    {
        Finsh_Bow1 = true;
    }
    public void Start_BowShoot()
    {
        Start_Bow1 = true;

    }
}
