using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_ContinueDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private float damage;
    private float ContinueDamage;
    public float Damage { get => damage; set => damage = value; }
    public float ContinueDamage1 { get => ContinueDamage; set => ContinueDamage = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            if(other!=null)
            {
                other.GetComponent<Enemy>().TakeContinuesDamage(Damage, ContinueDamage1);
            }
            
        }
    }
}
