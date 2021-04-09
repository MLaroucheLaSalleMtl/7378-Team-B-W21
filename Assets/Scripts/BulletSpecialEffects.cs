using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpecialEffects : MonoBehaviour
{
    public enum SpecialType
    {
        fire_special
    }
    public int damage = 50;
    public float continuesDamage = 10;

    public SpecialType specialType;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "enemy")
        {
            if(specialType == SpecialType.fire_special)
            {
                other.GetComponent<Enemy>().TakeContinuesDamage(damage, continuesDamage);
            }
        }
    }
}
