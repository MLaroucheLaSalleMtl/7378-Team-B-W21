using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

           
            if (Physics.Raycast(ray, out hit, 100.0f) && hit.transform.tag == "Cube")
            {

                GameObject.Destroy(hit.transform.gameObject);

            }

        }
    }
}
