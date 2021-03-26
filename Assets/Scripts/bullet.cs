using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //public AudioClip normal;
    //public AudioClip snow;
    //public AudioClip fire;
    //AudioSource audiosource;

    public enum Type 
    {
        standard_bullet,
        fireball,
        snowball
    }

    public Type type;

    public int damage = 50;
    public float continuesDamage = 10;
    public float speed = 20;
    private Transform target;
    private Enemy enemy;
    public GameObject explosionEffectPrefab;
    private GameObject Burn_Effect;
    private Enemy Current_Enemy;
    [SerializeField] private GameObject FireImpact_FX;

    public GameObject Burn_Effect1 { get => Burn_Effect; set => Burn_Effect = value; }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audiosource = GetComponent<AudioSource>();
    }

    //public void Playnormalvoice()
    //{
    //    audiosource.PlayOneShot(normal);
    //}

    //public void Playsnowvoice()
    //{
    //    audiosource.PlayOneShot(snow);
    //}

    //public void Playfirevoice()
    //{
    //    audiosource.PlayOneShot(fire);
    //}

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        LookAtEnemy();

    }

    public void LookAtEnemy()
    {
        transform.LookAt(target.transform.position,target.transform.forward);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    private void Impact_FX(GameObject FX)
    {
        GameObject Impact = Instantiate(FX, transform.position, Quaternion.identity);
        Destroy(Impact, 2f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "enemy")
        {
            Current_Enemy = other.GetComponent<Enemy>();
            if (type == Type.standard_bullet)
            {
                
                //audiosource.PlayOneShot(clip1);
                other.GetComponent<Enemy>().TakeDamage(damage);
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            if (type == Type.snowball)
            {
                
                //audiosource.PlayOneShot(clip2);
                other.GetComponent<Enemy>().TakeSnowBallDamage(damage);
                GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(effect, 1);
                Destroy(this.gameObject);
            }
            if (type == Type.fireball)
            {

                Impact_FX(FireImpact_FX);
                Burn_Effect1 = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Burn_Effect1.transform.parent = this.transform;
                Destroy(Burn_Effect1, 5f);
            }




        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "enemy")
        {
            if (type == Type.fireball)
            {
                
                 //audiosource.PlayOneShot(clip3);
                 Current_Enemy.TakeContinuesDamage(10, continuesDamage);
                
                
                 Destroy(this.gameObject,5f);
                            
            }

           
        }

    }
}
