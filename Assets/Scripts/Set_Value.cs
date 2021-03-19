using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Value : MonoBehaviour
{
    Turrents_Test MY_turrent;
    [SerializeField] private GameObject Cube_Red;
    [SerializeField] private GameObject Cube_Yellow;
    [SerializeField] private GameObject Cube_Black;
    [SerializeField] private GameObject Cube_Pink;
    
    bool IsUse;

    float red_timer = 0;
    float Yellow_timer = 0;

    float value;
    [SerializeField] private Enemy[] enemy;
    private Turret[] Turret;
    
    private bool Redbegin = false;
    private bool Yellowbegin = false;
    

    void Start()
    {
        IsUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        //m_timer += Time.deltaTime;
        
        if (IsUse)
        {
            MY_turrent.Hp *= 2f;
        }

        // if (Cube_Type == "Yellow")
        // {
        //     //五秒的时长
        //    //turrents attect speed *= 2;
        // }

        //if (Cube_Type =="green")
        // {
        //     //五秒的时长
        //     //Enemy.speed = 0;
        // }
        // if (Cube_Type=="pink")
        // {
        //     //enemy hp *= 0.7;

                 //Enemy.hp *= 0.7;
        // }
        // if (Cube_Type=="black")
        // {

        //     BuildManager.money += 200;
        // }
    }

    public void RedPowerUp()
    {
        enemy = FindObjectsOfType<Enemy>();
        Debug.Log("laolaozai" + enemy.Length);
        if (Redbegin == false)
        {
            // if false .....

        }
        foreach (Enemy element in enemy)
        {
            element.Move_speed = element.Move_speed * 0.2f;
            red_timer += Time.deltaTime;
            Debug.Log("Red begin" + element.Move_speed);
            if (red_timer == 1f)
            {

                Redbegin = false;
                element.Move_speed = element.Move_speed * 10f;
                red_timer = 0f;
                Debug.Log("Red finish" + element.Move_speed);
            }
        }

    }

    public void BlackPowerUp()
    {
        Debug.Log("PowerUp!!!");
        if (Cube_Black)
        {
            BuildManager.money += 200;
        }
    }

    public void YellowPowerUp()//防御塔攻速
    {
        Turret = FindObjectsOfType<Turret>();
        
        if (Yellowbegin == false)
        {
            // if false .....

        }

        foreach (Turret element in Turret)
        {
            element.attackRate = element.attackRate * 2f;
            Yellow_timer += Time.deltaTime;
            Debug.Log("Yellow begin" + element.attackRate);
            if (Yellow_timer == 1f)
            {

                Yellowbegin = false;
                element.attackRate = element.attackRate * 0.5f;
                Yellow_timer = 0f;
                Debug.Log("Yellow finish" + element.attackRate);
            }
        }

    }

    public void PinkPowerUp()//消灭所有敌人
    {
        if (Cube_Pink)
        {
            enemy = FindObjectsOfType<Enemy>();
            foreach (Enemy element in enemy)
            {
                element.Die();
            }

        }
    }

    //public void YellowPowerUp()
    //{
    //    if (Cube_Yellow && Yellowbegin == false)
    //    {
    //        Yellowbegin = true;
    //        if (Yellowbegin == true)
    //        {

    //            enemy.Speed = enemy.Speed * 0.5f;
    //            Yello_timer += Time.deltaTime;
    //            Debug.Log("Red begin" + enemy.Speed);
    //            if (Yello_timer == 5f)
    //            {

    //                Yellowbegin = false;
    //                enemy.Speed = enemy.Speed * 2f;
    //                Yello_timer = 0f;
    //                Debug.Log("Yellow finish" + enemy.Speed);
    //            }
    //        }
    //    }
    //}
}
