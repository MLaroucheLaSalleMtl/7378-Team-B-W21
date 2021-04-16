using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGameObject : MonoBehaviour
{
    [SerializeField] private Transform ThunderTurret;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.LookAt(ThunderTurret);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
