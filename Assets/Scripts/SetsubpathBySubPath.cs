using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsubpathBySubPath : MonoBehaviour
{
    // Start is called before the first frame update
     public bool NextPathIsSubpath;
    private GameObject[] subpaths;
    private Set_BlockArea[] Blocks;

    public GameObject[] Subpaths { get => subpaths; set => subpaths = value; }
    public Set_BlockArea[] Blocks1 { get => Blocks; set => Blocks = value; }

    void Start()
    {
        Subpaths = new GameObject[transform.childCount];
        //Blocks1 = new Set_BlockArea[transform.childCount];
        if(transform.childCount>0)
        {
           
            for (int i = 0; i < transform.childCount; i++)
            {

                //Debug.Log(i);
                //Debug.Log(transform.GetChild(i).gameObject);
                Subpaths[i] = transform.GetChild(i).gameObject;



            }
            //for (int i = 0; i < Subpaths.Length; i++)
            //{
                
            //    {
            //        Blocks1[i] = Subpaths[i].GetComponent<Set_BlockArea>();
            //    }

            //} //get all block  bool from subpaths gameobject//get all subpath gameobject from children
        }
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Block_other_subpaths(int selected_Index)
    {
        Blocks1[selected_Index].Block = false;
        for(int i=0;i<Subpaths.Length;i++)
        {
            if(i!=selected_Index)
            {
                Blocks1[i].Block = true;   //block all subpaths except selected subpath
            }
        }
    }
}
