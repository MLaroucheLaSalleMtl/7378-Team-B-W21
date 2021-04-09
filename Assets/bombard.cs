using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombard : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private List<GameObject> TurrentGameObject_List;
    private List<Turret> turrent_list;
    

    [SerializeField] private GameObject Boom_FX;
    [SerializeField] private GameObject Fire_FX;
    [SerializeField] Transform Fire_Position;
    private AOE_Boom AOE_Boom;

    public List<Turret> Turrent_list { get => turrent_list; set => turrent_list = value; }

    void Start()
    {
        TurrentGameObject_List = new List<GameObject>();
        turrent_list = new List<Turret>();
        Boom_FX.SetActive(false);
        Fire_FX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
           
        Debug.Log(Turrent_list.Count);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Turrent")
        {
            Turrent_list.Remove(other.gameObject.GetComponent<Turret>());
            TurrentGameObject_List.Remove(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Turrent")
        {
            Turrent_list.Add(other.gameObject.GetComponent<Turret>());
            TurrentGameObject_List.Add(other.gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Turrent")
        {
            if(Turrent_list.Contains(other.GetComponent<Turret>()))
            {
                if (enemy.Energy1 >= 100)
                {
                    enemy.Energy1 = 0;
                    BombardToTurrents();
                }
            } 
        }
        
    }
    
    
    void BombardToTurrents()
    {
        if (Turrent_list.Count == 0) return;
        if(Turrent_list.Count==1)
        {
            Boom(0);
        }
        if(Turrent_list.Count>1)
        {
            int index;
            for(int i=0; i < turrent_list.Count; i++)
            {
                if(turrent_list[i]==null)
                {
                    turrent_list.Remove(turrent_list[i]);
                }
            }
            
           
                index = Random_TurrentFromList();
           
            
            Boom(index);
        }
    }
    int Random_TurrentFromList()
    {
        return Random.Range(0, Turrent_list.Count - 1);
    }
    void Boom(int index)
    {
        //foreach (Turret element in turrent_list)
        //{
        //    if (element.Hp <= 0)
        //    {
        //        turrent_list.Remove(element);
        //    }
        //}
       
        
        if (Turrent_list[index] != null)
        {
            Vector3 Boom_Position;
            Boom_Position = Turrent_list[index].transform.position;
            
            

            //GameObject Fire_fx = Instantiate(Fire_FX, Fire_Position.position, Quaternion.identity);
            //Destroy(Fire_fx, 3f);
            Fire_FX.SetActive(true);
            Invoke("SetFalse_FireFX", 3f);
            
            GameObject Boom_Area = Instantiate(Boom_FX, Boom_Position, Quaternion.identity);
            Boom_Area.SetActive(true);
            AOE_Boom = Boom_Area.GetComponent<AOE_Boom>();
            AOE_Boom.Damage = enemy.Normal_damage;
            Debug.Log(AOE_Boom.Damage);
            
            Destroy(Boom_Area,3f);
        }
    }
    private void SetFalse_FireFX()
    {
        Fire_FX.SetActive(false);
    }
}
