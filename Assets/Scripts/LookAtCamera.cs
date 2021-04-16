using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
     RectTransform myCanvasTransform;
     Transform Camera;
     public Vector3 offset = new Vector3(0,180,0);
    void Start()
    {
        myCanvasTransform = GetComponent<RectTransform>();

        Camera = GameObject.FindWithTag("Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(myCanvasTransform!=null)
        {
            myCanvasTransform.LookAt(Camera);
            myCanvasTransform.rotation *= Quaternion.Euler(offset);
        }
        
    }
}
