using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpecialEffects : MonoBehaviour
{
    public enum SpecialType
    {
        fire_special
    }
    public float damage;

    public SpecialType specialType;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(specialType == SpecialType.fire_special)
            {
                if(enemy != null||enemy.tag!="Dead")
                {

                    enemy.TakeDamage(damage);
                    
                    
                }
                
            }
        }
    }
    
    
}
