using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_BlockArea : MonoBehaviour
{
    public bool Block;
    private GameObject Block1;
    private GameObject Block2;
    private bool RunOneTimeForBlock;   //run one time the block fuction.just in case,dont make update run the block fuction many times.
    void Start()
    {
        Block = false;
        RunOneTimeForBlock = true;
        Block1 = transform.GetChild(0).gameObject;
        Block2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Block)
        {
            Block_path();
        
        }
        if(!Block)
        {
            Dont_Block_path();
           
        }
        
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            Block = false;
        }
    }
    private void Block_path()
    {
        Block1.SetActive(true);
        Block2.SetActive(true);
        RunOneTimeForBlock = false;
    }
    private void Dont_Block_path()
    {
        Block1.SetActive(false);
        Block2.SetActive(false);
        RunOneTimeForBlock = false;
    }
   
    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="enemy")
        {
            
            RunOneTimeForBlock = true;
        }
    }

}
