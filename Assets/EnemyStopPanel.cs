using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class EnemyStopPanel
{
    public enum enemytype
    {
        Skeleton, SkeletonArcher, Bomber, ArcherCarrier, CannonCarrier, DeadWizard, dogman, Ghost, GhostShooter, GreenArcher, DeathKnight, Healer
    }
    [SerializeField] private string Index;

    [SerializeField] private string Name;
    [SerializeField] private Sprite Sprite;
    [SerializeField] private string Introduction;
    [SerializeField] private float Hp;
    [SerializeField] private float Damage;
    [SerializeField] private float shield;
    [SerializeField] private float Movement;
    [SerializeField] private enemytype EnemyType;

    

   
    public Sprite Sprite1 { get => Sprite; set => Sprite = value; }
    public string Introduction1 { get => Introduction; set => Introduction = value; }
    public float Hp1 { get => Hp; set => Hp = value; }
    public float Damage1 { get => Damage; set => Damage = value; }
    public float Shield { get => shield; set => shield = value; }
    public float Movement1 { get => Movement; set => Movement = value; }
    public enemytype EnemyType1 { get => EnemyType; set => EnemyType = value; }
    public string Name1 { get => Name; set => Name = value; }
    public string Index1 { get => Index; set => Index = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
