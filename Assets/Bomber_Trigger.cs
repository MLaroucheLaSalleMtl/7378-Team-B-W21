using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_Trigger : MonoBehaviour
{
    public Enemy Bomber;
    private bool IfExplosion;
    private float Timer;
    private float Max_Timer;
    private bool IfEnemyAround;
    private float damage;
    GameObject Turrent_Target;
    List<Turret> turrents = new List<Turret>();
  

    public GameObject Explosion;

    private ParticleSystem[] particleSystems;
    void Start()
    {
        IfExplosion = false;
        Max_Timer = 3f;
        Timer = Max_Timer;
        IfEnemyAround = false;
        damage = Bomber.Normal_damage;

        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTurrent();
        //Delete_Turrents();

        
            Bomber_Behaviour(Turrent_Target, IfExplosion);
        
       
        //Control_EnemyNavMesh();
        if(Turrent_Target=null)
        {
            Debug.Log(Bomber.CanMove1);
        }
        if(!IfEnemyAround)
        {
            Bomber.CanMove1 = true;
        }

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
            Turrent_Target = other.gameObject;
            Bomber.CanMove1 = false;
            
            IfEnemyAround = true;

            turrents.Add(other.gameObject.GetComponent<Turret>());

        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Turrent")
        {
            Timer -= Time.deltaTime;
            if(Timer<=0f)
            {
                IfExplosion = true;
                
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag=="Turrent")
        {
            Debug.Log("Disappear!");
            turrents.Remove(other.gameObject.GetComponent<Turret>());
           
            IfEnemyAround = false;
        }
        
    }

    private void Bomber_Behaviour(GameObject Target, bool IfFinishExplosion)
    {
        if (IfEnemyAround)
        {
            
            if (Target != null)
            {
                Bomber.Agent1.destination = Target.transform.position;
            }
            if(Target==null)
            {
           
                Bomber.CanMove1 = true;
                
            }
            
        }
        if (IfFinishExplosion)
        {
            IfExplosion = false;
            Explosion=Instantiate(Explosion, transform.position, Quaternion.identity);
            foreach(Turret element in turrents)
            {
                element.Taken_Damage(damage);

            }

            Destroy(Explosion, 2f);
            
           
           
                Bomber.Die();
            Debug.Log("boom!!!");
            
            
        }
    }


}
