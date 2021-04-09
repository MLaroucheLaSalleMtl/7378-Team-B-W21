using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretsPanelValue
{
    [SerializeField] private string Name;
    [SerializeField] private Sprite Image;
    [SerializeField] private string Introduction;
    [SerializeField] private float damage;
    [SerializeField] private float Attack_Speed;
    [SerializeField] private float Cost;
    [SerializeField] private float Upgrade_Cost;

    public string Name1 { get => Name; set => Name = value; }
    public Sprite Image1 { get => Image; set => Image = value; }
    public string Introduction1 { get => Introduction; set => Introduction = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Attack_Speed1 { get => Attack_Speed; set => Attack_Speed = value; }
    public float Cost1 { get => Cost; set => Cost = value; }
    public float Upgrade_Cost1 { get => Upgrade_Cost; set => Upgrade_Cost = value; }
}
