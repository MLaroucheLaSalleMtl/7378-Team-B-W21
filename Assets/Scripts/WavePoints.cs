using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WavePoints : MonoBehaviour
{
    public static Transform[] position;

    //https://www.youtube.com/playlist?list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0
    void Awake()
    {
        position = new Transform[transform.childCount];
        for (int i = 0; i < position.Length; i++)
        {
            position[i] = transform.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
