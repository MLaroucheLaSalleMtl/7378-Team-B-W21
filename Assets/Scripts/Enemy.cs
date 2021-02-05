using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public float hp = 150;
    private float totalHp;
    public GameObject explosionEffect;
    public Slider hpSlider;
    private Transform[] positions;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        positions = WavePoints.position;
        totalHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        //判断enemy是否到达了最后的一个点
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    /// <summary>
    //reach to the final target and game over
    /// </summary>
    void ReachDestination()
    {
       
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.EnemyCount--;
    }

    //public void TakeDamage(float damage)
    //{
    //    if (hp <= 0) return;
    //    hp -= damage;
    //    hpSlider.value = (float)hp / totalHp;
    //    if (hp <= 0)
    //    {
    //        Die();
    //    }
    //}
    public void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
