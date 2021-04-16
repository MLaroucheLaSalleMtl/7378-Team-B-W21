using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_Trigger : MonoBehaviour
{
    public Enemy Bomber;
    private bool IfExplosion;
    private bool IfStartCount;
    private bool IfStopTimeCount;
    private float Timer;
    private float Max_Timer;
    private bool IfEnemyAround;
    private float damage;
    List<Turret> turrents = new List<Turret>();
  

    public GameObject Explosion;
    

    private ParticleSystem[] particleSystems;
    void Start()
    {
        IfExplosion = false;
        Max_Timer = 1f;
        Timer = Max_Timer;
        IfEnemyAround = false;
        damage = Bomber.Normal_damage;
        IfStartCount = false;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTurrent();
        //Delete_Turrents();
        if(Bomber.Energy1>=100)
        {
            IfStartCount = true;
        }
        

        
            Bomber_Behaviour( IfExplosion);
        
       
        //Control_EnemyNavMesh();
        //if(Turrent_Target==null&&IfEnemyAround)
        //{
        //    Bomber.RestartMove();
        //    IfEnemyAround = false;
        //    turrents.Clear();

        //}
       

    }
    private void Delete_Turrents()
    {
        foreach (Turret element in turrents)
        {

           if(element.Hp<=0)
            {
                turrents.Remove(element);
            }
        }
        
    }
    //private void UpdateTurrent()
    //{
    //    if(turrents.Count==1)
    //    {
    //        Turrent_Target = turrents[0];
    //    }
    //    if (Turrent_Target != null)
    //    {
    //        if (Turrent_Target.Hp <= 0)
    //        {
    //            turrents.Remove(Turrent_Target);
    //            if (turrents.Count > 0)
    //            {
    //                int index;
    //                index = turrents.IndexOf(Turrent_Target);
    //                if (index + 1 < turrents.Count)
    //                {
    //                    index++;

    //                }
    //                Turrent_Target = turrents[index];
    //            }
    //        }
    //    }
    //}
    //private void Control_EnemyNavMesh()
    //{
        
    //    if(turrents.Count<=0)
    //    {
    //        Bomber.Agent1.ResetPath();
    //        Bomber.CanMove1 = true;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Turrent")
        {
            
           
                //Turrent_Target = other.gameObject;
                IfEnemyAround = true;

               
            
            turrents.Add(other.gameObject.GetComponent<Turret>());
            //Bomber.StopMove();



        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Turrent")
        {
            if(Timer>0f&&IfStartCount)
            {
                Timer -= Time.deltaTime;
            }
           
            if(Timer<=0f&&!IfStopTimeCount)
            {
                IfExplosion = true;
                IfStopTimeCount = true;
                
            }
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag=="Turrent")
        {
            turrents.Remove(other.gameObject.GetComponent<Turret>());
           
            IfEnemyAround = false;
        }
        
    }

    private void Bomber_Behaviour( bool IfFinishExplosion)
    {
        if (IfEnemyAround)
        {
            if (IfFinishExplosion)
            {
                IfExplosion = false;
                GameObject Explo = Instantiate(Explosion, transform.position, Quaternion.identity);
                
                foreach (Turret element in turrents)
                {
                    if (element != null)
                    {
                        element.Taken_Damage(damage);
                    }


                }

                Destroy(Explo, 3f);



                
                if (this != null)
                {
                    Destroy(this.transform.parent.gameObject);
                }


            }


        }
        
    }
    private void OnDestroy()
    {
        EnemySpawner.EnemyCount--;
    }


}
