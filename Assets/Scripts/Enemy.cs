using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : Enemy_property
{
    private float halfSpeed;
    public GameObject explosionEffect;
    public Slider hpSlider;
    public Slider EnergySlider;


    private bool CanTakenFirstDamage;//for continue damage to make sure take one damage and take a few times continue damage.
    private float ContinueDamage_Timer;  //how many seconds you want to run the ContinueDamage
    private float Max_Continuedamage_timer;
    private bool IsFinish_ContinueDamage_Timer;
    private bool Find_Portal;
    private Transform Portal_Position;
    [SerializeField] private GameObject Destination_FX;


    private Get_PortalTarget Get_Portal;
    private bool IsarrivePortal;

    public int rate;
    bool onetimes_forCards;


    //By PeiXin
    private NavMeshAgent Agent;
    Vector3 Enemy_Velocity;
    Animator Enemy_Anim;

    Enemy_property Enemy_prop;
    bool IsLose;
    bool IsDead;

    //wavepoints

    private Transform[] positions;
    private int Point_Index;
    private GameObject[] point_gameobject;
    private SetsubpathBySubPath[] subpath;
    private bool IfarrivePoint;              //if this is true that mean enemy arrive any points position. if this is false that mean enemy exit any points position.
    private bool IfFinishSubPaths;
    private bool InSubPath;        //if in subpath so only run relative subpath fuction.
    private Vector3 Current_Destination;

    public bool IfFinishSubPaths1 { get => IfFinishSubPaths; set => IfFinishSubPaths = value; }
    public bool CanMove1 { get => CanMove; set => CanMove = value; }
    public NavMeshAgent Agent1 { get => Agent; set => Agent = value; }
    public bool Find_Portal1 { get => Find_Portal; set => Find_Portal = value; }
    public Transform Portal_Position1 { get => Portal_Position; set => Portal_Position = value; }
    public Animator Enemy_Anim1 { get => Enemy_Anim; set => Enemy_Anim = value; }
    public bool Start_Bow1 { get => Start_Bow; set => Start_Bow = value; }
    public bool Finsh_Bow1 { get => Finsh_Bow; set => Finsh_Bow = value; }


    //Card
    [SerializeField] private GameObject[] Cards;


    //Energy
    private bool IfResetEnergy;

    //bow
    private bool Start_Bow;
    private bool Finsh_Bow;

    public bool CanMove;
    // Start is called before the first frame update
    void Start()
    {
        IsarrivePortal = false;
        positions = WavePoints.position;
        Max_hp = Hp;
        halfSpeed = Move_speed * 0.5f;
        CanTakenFirstDamage = true;
        //PeiXin
        Agent1 = GetComponent<NavMeshAgent>();
        Enemy_Anim1 = GetComponent<Animator>();
        IsDead = false;

        onetimes_forCards = false;
        ContinueDamage_Timer = 5f;
        Max_Continuedamage_timer = ContinueDamage_Timer;

        Set_NavSpeed();
        IsLose = false;
        Set_Property();
        IsDead = false;
        Point_Index = 0;

        point_gameobject = WavePoints.points_gameobject;
        subpath = new SetsubpathBySubPath[point_gameobject.Length];

        InSubPath = false;

        CanMove1 = true;

        //Bomber

        for (int i = 0; i < point_gameobject.Length; i++)
        {
            subpath[i] = point_gameobject[i].GetComponent<SetsubpathBySubPath>();

        }

        IfFinishSubPaths1 = false;

        Max_Energy1 = 100f;
        Energy1 = 0f;

        //bow
        Start_Bow1 = false;
        Finsh_Bow1 = false;



    }

    // Update is called once per frame
    void Update()
    {
        if (Isultimate1)
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
                Debug.Log("Go");
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

        Enemy_Anim1.SetBool("IsDead", IsDead);
        //Debug.Log(Enemy_Velocity.magnitude);
        Enemy_Velocity = Agent1.velocity;
        Enemy_Anim1.SetFloat("Magnitude", Enemy_Velocity.magnitude);
        if (Enemy_Velocity.magnitude <= 0.1)
        {
            IsLose = true;
        }
        if (this.Hp == 0)
        {
            IsDead = true;

        }
        if (IsDead)
        {
            //Drop_rate();

            //if (!onetimes_forCards)
            //{

            //    Drop();
            //    onetimes_forCards = true;
            //}
            Destroy(transform.gameObject, 4f);
        }
        //Hp = Hp;

        //Debug.Log(Hp);

        Enemy_Anim1.SetBool("IsDead", IsDead);
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
        CanMove1 = false;
        Current_Destination = Agent1.destination;
        Agent1.ResetPath();
        Debug.Log("Stop");
    }
    public void RestartMove()
    {
        CanMove1 = true;
        Agent1.destination = Current_Destination;

    }

    private void Enemy_die()   //PeiXin
    {
        Enemy_Anim1.SetBool("IsDead", true);
        Invoke("Destroy_Gameobject", 3f);

    }
    private void Destroy_Gameobject()
    {
        Destroy(this.gameObject);
    }



    void Move()
    {


        if (!InSubPath)
        {

            Agent1.destination = positions[Point_Index].position;



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

                Agent1.destination = subpath[Point_Index].Subpaths[Random_Fromsubpaths(subpath_length)].transform.position;
                if (Point_Index < positions.Length)
                {
                    Point_Index++;
                }
            }
            if (!subpath[Point_Index].NextPathIsSubpath)
            {
                if (Point_Index < positions.Length - 1)
                {
                    Point_Index++;
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

        LevelManager.Instance.Fail();
        GameObject.Destroy(this.gameObject);
    }
    private void ArrivePortal()
    {
        Debug.Log("Reach");
        Vector3 Destination_Position;
        Destination_Position = transform.position;
        Destination_Position.y += 2;
        GameObject Portal_FX = Instantiate(Destination_FX, Destination_Position, Quaternion.identity);
        LevelManager.Instance.Basement_HP1 -= this.basement_damage;
        Debug.Log("Reduce Basement HP " + this.basement_damage);


        GameObject.Destroy(Portal_FX, 2f);
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

        hpSlider.value = (float)Hp / Max_hp;
        if (Hp <= 0)
        {
            Die();
            BuildManager.money += 10;

        }
    }
    public void TakenDamagePerSeconds(float damage)
    {
        if (Hp <= 0) return;
        Hp -= damage * Time.deltaTime;

        hpSlider.value = (float)Hp / Max_hp;
        if (Hp <= 0)
        {
            Die();
            BuildManager.money += 10;

        }
    }



    public void TakeContinuesDamage(float First_damage, float Continues_damage)
    {
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
    private void StopMoving()
    {
        Agent1.ResetPath();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Turrent")
        //{
        //    Use_FireBall = true;
        //}
        if (other.tag == "WayPoints" && !InSubPath)
        {
            IfarrivePoint = true;
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
        if (other.tag == "WayPoints" && !InSubPath)
        {
            IfarrivePoint = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Home")
        {
            Destroy(collision.gameObject);
        }
    }


    public void Reset_ContinueDamage_Timer()
    {
        ContinueDamage_Timer = Max_Continuedamage_timer;
        CanTakenFirstDamage = true;
    }

    public void TakeSnowBallDamage(float damage)
    {
        if (Hp <= 0) return;
        Hp -= damage;
        Move_speed = halfSpeed;
        hpSlider.value = (float)Hp / Max_hp;
        if (Hp <= 0)
        {
            Die();
            BuildManager.money += 10;

        }
    }
    public void Die()
    {
        Drop();
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);


    }


    //Card systems  ||↓ ↓ ↓ ↓ ↓ ↓ ↓ 

    //private bool Drop_rate()
    //{

    //    rate = Random.Range(0, 101);
    //    //if (rate <= 5)
    //    //{
    //    //    return true;
    //    //}
    //    return rate <= 5;

    //    //int RandomNumber = Random.Range(0, 20);
    //    //if (RandomNumber == 1)
    //    //{

    //    //}
    //}


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
        //Instantiate(Cards[index], cardposition, Quaternion.identity);

    }

    private void Drop()
    {

        rate = Random.Range(0, 101);
        if (rate <= 100)
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
