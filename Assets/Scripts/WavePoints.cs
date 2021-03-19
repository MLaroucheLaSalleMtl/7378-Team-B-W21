using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WavePoints : MonoBehaviour
{
    public static Transform[] position;
    public static GameObject[] points_gameobject;
    public static SetsubpathBySubPath[] paths_controller;

    //https://www.youtube.com/playlist?list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0
    void Awake()
    {
        
        points_gameobject = new GameObject[transform.childCount];
        position = new Transform[transform.childCount];
        paths_controller = new SetsubpathBySubPath[transform.childCount];

        for (int i = 0; i < position.Length; i++)
        {
            points_gameobject[i] = transform.GetChild(i).gameObject;
            paths_controller[i] = transform.GetChild(i).GetComponent<SetsubpathBySubPath>();
            position[i] = transform.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
