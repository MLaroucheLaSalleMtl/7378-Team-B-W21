using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private List<GameObject> enemys = new List<GameObject>();


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    public float attackRate = 1; //times per second
    private float timer = 0;
    public GameObject bulletPerfab;
    public Transform firePosition;
    public Transform head;


    public bool useLaser = false;
    public float damageRate = 70;// Laser attack rate
    public LineRenderer laserRanderer;
    public GameObject laserEffect;


    void Start()
    {
        timer = attackRate;
    }


    void Update()
    {
        //heads direction
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }

        if (useLaser == false)
        {
            //bullet attack
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRate)
            {
                timer = 0;
                Attack();
            }

        }
        else if (enemys.Count > 0)
        {
            //update laser attack
            if (laserRanderer.enabled == false)
            {
                laserRanderer.enabled = true;
                laserEffect.SetActive(true);
            }

            if (enemys[0] == null)
            {
                UpdateEnemy();

            }
            if (enemys.Count > 0)
            {
                laserRanderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                //allow lasereffect look at laser turret
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);

            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRanderer.enabled = false;
        }






    }

    public void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemy();

        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPerfab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRate;
        }

    }

    void UpdateEnemy()//save all empty enemys
    {
        
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                emptyIndex.Add(i);
            }
        }
        for (int j = 0; j < emptyIndex.Count; j++)
        {
            enemys.RemoveAt(emptyIndex[j] - j);
        }
    }
}
