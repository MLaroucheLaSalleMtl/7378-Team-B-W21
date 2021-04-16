using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childrenEnemy : MonoBehaviour
{
    [SerializeField] private Enemy ParentEnemy;
    [SerializeField] private GameObject ChildrenEnemy;

    [SerializeField] private GameObject instantiateFX;

    [SerializeField] private GameObject[] childrensEnemy;  //for destroy gameobject when the parent dead.

    private bool Set_DEADvalue;
    void Start()
    {
        Set_DEADvalue = true;
    }
     
    
    // Update is called once per frame
    void Update()
    {
        if(ParentEnemy.IsDead1&&Set_DEADvalue)
        {
            Set_DEADvalue = false;
            Vector3 position1;
            Vector3 position2;
            position1 = transform.position;
            position1.x += 2f;
            position2 = transform.position;
            position2.x -= 2f;
            foreach (GameObject element in childrensEnemy)
            {
                Destroy(element);
            }
            GameObject Smoke = Instantiate(instantiateFX, transform.position, Quaternion.identity);
            Destroy(Smoke, 4f);
            Create_ChildrenEnemy(position1);
            Create_ChildrenEnemy(position2);
            
        }
    }
    void Create_ChildrenEnemy(Vector3 position)
    {
        
        GameObject Green_Enemy = Instantiate(ChildrenEnemy, position, Quaternion.identity);
        Enemy Genemy = Green_Enemy.GetComponent<Enemy>();
        Genemy.GetMovingValueFromOthers(ParentEnemy);
        
    }
}
