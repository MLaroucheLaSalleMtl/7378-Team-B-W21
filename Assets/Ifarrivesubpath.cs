using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ifarrivesubpath : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="enemy")
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.IfFinishSubPaths1 = true;
            
        }
    }
}
