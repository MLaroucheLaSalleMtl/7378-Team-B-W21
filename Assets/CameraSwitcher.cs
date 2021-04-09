using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Animator anim;
    private int cam = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    void SwitchCam(int val)
    {
        cam = val;
        anim.SetInteger("POV", cam);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && cam != 0)
        {
            SwitchCam(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && cam != 1)
        {
            SwitchCam(1);
        }
    }
}
