using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ViewController : MonoBehaviour
{
    private Vector3 move = new Vector3();
    public float speed = 1;// camera move speed
    public float mouseSpeed = 60;
    float rotationSpeed = -100.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        OnMove();
    }

    public void OnMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h * speed, mouse * mouseSpeed, v * speed) * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, 45 * rotationSpeed * Time.deltaTime * 0.01f, Space.Self);

        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, -45 * rotationSpeed * Time.deltaTime * 0.01f, Space.Self);

        }

    }



}
