using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_SliderLookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform MyTansform;
    void Start()
    {
        MyTansform = transform.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        MyTansform.LookAt(Camera.current.transform.position);
       
    }
}
