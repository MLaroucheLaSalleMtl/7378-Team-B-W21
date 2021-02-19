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
    private Transform FireBall1;

    [SerializeField] private Transform FireBall_StartPos;
   
    private Vector3 Enemy_Direction;

    public float Hp { get => hp; set => hp = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Damage_rate { get => damage_rate; set => damage_rate = value; }

    void Start()
    {
        
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
            
            FireBall1 = Instantiate(FireBall, FireBall_StartPos.position, Quaternion.identity);
            FireBall1.transform.LookAt(Enemy_GameOb.transform.position);

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
        enemy.Attack(this);
    }
}
