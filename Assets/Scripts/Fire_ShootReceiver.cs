using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_ShootReceiver : MonoBehaviour
{
    private bool FinishShoot;
    private bool StartShoot;

    public bool FinishShoot1 { get => FinishShoot; set => FinishShoot = value; }
    public bool StartShoot1 { get => StartShoot; set => StartShoot = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Finish_BowShoot()
    {
        FinishShoot1 = true;
    }
    public void Start_BowShoot()
    {
        StartShoot1 = true;

    }
}
