using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcingAttackRate : MonoBehaviour
{
    private Enemy enemy;
    List<Turret> turrent_list = new List<Turret>();

    [SerializeField] private GameObject ReducingRate_FX;
    [SerializeField] private GameObject ultimate_skill;
    [SerializeField] private float Boom_Damage;
    bool RunOneTime;

    private List<GameObject> reducingRate_FxList=new List<GameObject>();
    void Start()
    {
        enemy = transform.parent.parent.GetComponent<Enemy>();
        ultimate_skill.SetActive(false);
        ReducingRate_FX.SetActive(true);
        RunOneTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(RunOneTime)
        {
            if(enemy.Energy1>=100)
            {
                ultimateSkill();
                RunOneTime = false;
            }
            
        }
    }
    void ultimateSkill()
    {

        ultimate_skill.SetActive(true);
        foreach (Turret element in turrent_list)
        {
            element.Taken_Damage(Boom_Damage);
        }
        
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Turrent")
        {
            turrent_list.Add(other.gameObject.GetComponent<Turret>());
            foreach (Turret element in turrent_list)
            {
                element.attackRate *= 0.7f;
                
            }
            Vector3 Turrent_Position = new Vector3(other.transform.position.x, other.transform.position.y + 2f, other.transform.position.z);
            GameObject new_Fx = Instantiate(ReducingRate_FX, Turrent_Position, Quaternion.identity);
            new_Fx.transform.parent = other.transform;
            reducingRate_FxList.Add(new_Fx);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Turrent")
        {
            turrent_list.Remove(other.gameObject.GetComponent<Turret>());
           GameObject old_Fx= other.transform.Find("FX_Swirl_01(Clone)").gameObject;
            Destroy(old_Fx);
            //reducingRate_FxList.Remove()
        }
        
    }
}
