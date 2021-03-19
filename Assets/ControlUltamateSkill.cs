using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUltamateSkill : MonoBehaviour
{
    private List<GameObject> Ultamateskill_List=new List<GameObject>();

    [Header ("what ultimate you want to use")]
   [SerializeField] private GameObject[] Selected_ult;

    void Start()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            Ultamateskill_List.Add(transform.GetChild(i).gameObject);
        }
        Set_Selected_ultimateSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Set_Selected_ultimateSkill()
    {
        foreach(GameObject element in Ultamateskill_List)
        {
            element.SetActive(false);
        }
        foreach(GameObject element in Selected_ult)
        {
            element.SetActive(true);
        }
    }
}
