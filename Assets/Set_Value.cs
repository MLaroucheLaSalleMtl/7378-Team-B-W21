using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Value : MonoBehaviour
{
    Turrents_Test MY_turrent;
    [SerializeField] private string Cube_Type;
    bool IsUse;
    void Start()
    {
        IsUse = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(IsUse)
        {
            MY_turrent.Hp *= 2f;
        }
       if(Cube_Type=="Red")
        {

        }
       if(Cube_Type=="Yellow")
        {

        }
    }
}
