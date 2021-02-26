using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Value : MonoBehaviour
{
    Turrents_Test MY_turrent;
    [SerializeField] private string Cube_Type;
    bool IsUse;

    float m_timer = 0;

    void Start()
    {
        IsUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.time;

        if (IsUse)
        {
            MY_turrent.Hp *= 2f;
        }
       if(Cube_Type=="Red")
        {
            //五秒的时长
            //enemy move speed *= 0.5;

            //Enemy.speed *= 0.5;
        }
        if (Cube_Type=="Yellow")
        {
            //五秒的时长
           //turrents attect speed *= 2;
        }

       if (Cube_Type =="green")
        {
            //五秒的时长
            //Enemy.speed = 0;
        }
        if (Cube_Type=="pink")
        {
            //enemy hp *= 0.7;

            //Enemy.hp *= 0.7;
        }
       if (Cube_Type=="black")
        {

            //BuildManager.money += 200;
        }
    }
}
