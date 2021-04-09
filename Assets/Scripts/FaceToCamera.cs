using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    [SerializeField]private Transform target;
    private Vector3 pos = new Vector3();

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(target.position.x*37, transform.position.y*90, target.position.z);
        //transform.position = pos;
        transform.rotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, target.rotation.eulerAngles.z);
        transform.LookAt(pos);
    }
}
