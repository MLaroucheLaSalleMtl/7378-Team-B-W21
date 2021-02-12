using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToEndPosition : MonoBehaviour
{
    NavMeshAgent Agent;
    public Transform Target;
    Vector3 Boss_Velocity;
    Animator Boss_Anim;
    bool IsLose;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.destination=Target.position;
        Boss_Anim = GetComponent<Animator>();
        IsLose = false;
    }
 
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Boss_Velocity.magnitude);
        Boss_Velocity = Agent.velocity;
        Boss_Anim.SetFloat("Magnitude",Boss_Velocity.magnitude);
        if(Boss_Velocity.magnitude<=0.1)
        {
            IsLose = true;
        }
    }
}
