using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    private Set_Value PowerUp;
    public int cardIndex;

    private void Start()
    {
       
        PowerUp = transform.GetComponent<Set_Value>();
        //PowerUp = Set_Value;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //bool checkCard = Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Card"));
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Card")))
            {
                bool checkCard = hit.collider.gameObject == gameObject;

                if (checkCard && cardIndex == 1)
                {
                    Debug.Log("Card index =" + cardIndex);
                    PowerUp.RedPowerUp();
                    Destroy(gameObject);
                }
                else if (checkCard && cardIndex == 2)
                {
                    Debug.Log("Card index =" + cardIndex);
                    PowerUp.BlackPowerUp();
                    Destroy(gameObject);
                }
                else if (checkCard && cardIndex == 3)
                {
                    Debug.Log("Card index =" + cardIndex);
                    PowerUp.YellowPowerUp();
                    Destroy(gameObject);
                }
                else if (checkCard && cardIndex == 4)
                {
                    Debug.Log("Card index =" + cardIndex);
                    PowerUp.PinkPowerUp();
                    Destroy(gameObject);
                }
                //if (checkCard && cardIndex == 3)
                //{
                //    Debug.Log("Card index =" + cardIndex);
                //    PowerUp.YellowPowerUp();
                //}
                //if (checkCard && cardIndex == 4)
                //{
                //    Debug.Log("Card index =" + cardIndex);
                //    PowerUp.PinkPowerUp();
                //}
            }
        }
    }
}
