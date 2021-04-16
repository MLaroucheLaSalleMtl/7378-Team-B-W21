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
        
    }

    public void RedPowerUp()
    {
        enemy = FindObjectsOfType<Enemy>();
        Debug.Log("PowerUp" + enemy.Length);
        if (Redbegin == false)
        {
            // if false .....

        }
        foreach (Enemy element in enemy)
        {
            element.TakeSnowBallDamage(0);
        }

    }

    public void BlackPowerUp()
    {
        Debug.Log("PowerUp!!!");
        if (Cube_Black)
        {
            BuildManager.money += 300;
        }
    }

    public void YellowPowerUp()
    {
        Turret = FindObjectsOfType<Turret>();
        
        if (Yellowbegin == false)
        {
            // if false .....

        }

        foreach (Turret element in Turret)
        {
            element.AttackRateChange(0.5f, 5f);
        }

    }

    public void PinkPowerUp()
    {
        if (Cube_Pink)
        {
            enemy = FindObjectsOfType<Enemy>();
            foreach (Enemy element in enemy)
            {
                element.StopMove();
                element.ResetReStartMove(5f);
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
