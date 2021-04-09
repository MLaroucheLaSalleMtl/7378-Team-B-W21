using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivieEnemy : MonoBehaviour
{
    [SerializeField] private GameObject ReviveFX;
    [SerializeField] private GameObject Ghost;
    [SerializeField] private GameObject Ghost_Archer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Dead")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy!=null)
            {
                Vector3 ReviveFXPosition = transform.position;
                ReviveFXPosition.y += 2f;
                GameObject RevivFX = Instantiate(ReviveFX, ReviveFXPosition, Quaternion.identity);
                Destroy(RevivFX, 2f);
                Destroy(other.gameObject, 1f);
                if (enemy.EnemyType == Enemy.enemy_type.Skeleton)
                {

                    GameObject GhostEnemy = Instantiate(Ghost, transform.position, Quaternion.identity);
                    GhostEnemy.GetComponent<Enemy>().GetMovingValueFromOthers(other.GetComponent<Enemy>());


                }
                if (enemy.EnemyType == Enemy.enemy_type.SkeletonArcher)
                {
                    GameObject GhostEnemy_Archer = Instantiate(Ghost_Archer, transform.position, Quaternion.identity);
                    GhostEnemy_Archer.GetComponent<Enemy>().GetMovingValueFromOthers(other.GetComponent<Enemy>());
                }
                EnemySpawner.EnemyCount++;
            }
           
            
        }
    }
}
