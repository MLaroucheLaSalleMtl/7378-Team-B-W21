using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_property : MonoBehaviour
{
    [SerializeField] private float hp;
    private float max_hp;
    [SerializeField] private float lv;
    [SerializeField] private float move_speed;
    [SerializeField] private float normal_damage;
    [SerializeField] private float magic_damage;
    [SerializeField] private float defence;
    [SerializeField] private bool CanAttackTurrents;

    public float Hp { get => hp; set => hp = value; }
    public float Max_hp { get => max_hp; set => max_hp = value; }
    public float Lv { get => lv; set => lv = value; }
    public float Move_speed { get => move_speed; set => move_speed = value; }
    public float Normal_damage { get => normal_damage; set => normal_damage = value; }
    public float Magic_damage { get => magic_damage; set => magic_damage = value; }
    public float Defence { get => defence; set => defence = value; }
    public bool CanAttackTurrents1 { get => CanAttackTurrents; set => CanAttackTurrents = value; }


    

    
    


    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Set_Property()    //if enemy lv>lv1   it's property will be changed
    {
       
        if(Lv==2)
        {
            Normal_damage *= 1.3f;
            Magic_damage *= 1.3f;
            Defence *= 1.2f;
            Hp *= 1.5f;
            
        }
        if(Lv==3)
        {
            Normal_damage *= 1.6f;
            Magic_damage *= 1.6f;
            Defence *= 2f;
            Hp *= 2f;
        }
    }
    public void Attack(Turrents_Test Target)
    {
        Target.Hp -= this.Magic_damage + this.Normal_damage;
    }
    public void Taken_Damage(Turrents_Test Target)
    {
        Target.Damage -= this.Hp;
    }
}
