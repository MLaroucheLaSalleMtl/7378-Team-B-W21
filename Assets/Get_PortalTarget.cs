using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_PortalTarget : MonoBehaviour
{
    [SerializeField] private Transform Portal_Target;

    public Transform Portal_Target1 { get => Portal_Target; set => Portal_Target = value; }

    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag=="enemy")
        //{
        //    enemy = other.GetComponent<Enemy>();
        //    enemy.Find_Portal1 = true;
        //    enemy.Portal_Position1 = Portal_Target;
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    public void GoToPortal(Enemy enemy)
    {
        
        enemy.Find_Portal1 = true;
        enemy.Portal_Position1 = Portal_Target1;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
