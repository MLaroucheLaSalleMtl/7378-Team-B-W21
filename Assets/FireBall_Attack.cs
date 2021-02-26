using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Attack : MonoBehaviour
{
    
    private bool runOneTime;
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag=="Turrent")
        {
            if(runOneTime)
            {
                runOneTime = false;

            }
        }
    }
    void Start()
    {
        runOneTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
