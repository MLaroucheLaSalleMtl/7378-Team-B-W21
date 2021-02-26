using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrents_Test : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private float damage_rate;
    public GameObject Enemy_GameOb;
    private  GoToEndPosition Enemy;
    [SerializeField] public Transform FireBall;
    private Transform fireBall;

    [SerializeField] private Transform FireBall_StartPos;
   
    private Vector3 Enemy_Direction;

    private bool Is_TakenDamage;  //to detect if fireball collide on this gameobject.
    

    public float Hp { get => hp; set => hp = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Damage_rate { get => damage_rate; set => damage_rate = value; }

    void Start()
    {
        Is_TakenDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="enemy")
        {
            Debug.Log("Good");
            Enemy = other.gameObject.GetComponent<GoToEndPosition>();      
            
            fireBall = Instantiate(FireBall, FireBall_StartPos.position, Quaternion.identity);
          

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="enemy")
        {
            fireBall.transform.LookAt(Enemy_GameOb.transform.position);
        }
        
    }
   

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="enemy")
        {
            Enemy = null;
            
        }
    }
    public void Attack(GoToEndPosition enemy)
    {
        enemy.Taken_Damage(this);
    }
    public void Taken_Damage(GoToEndPosition enemy)
    {
        if(Is_TakenDamage)
        {
            enemy.Attack(this);
            Is_TakenDamage = false;
        }
        
    }
}
