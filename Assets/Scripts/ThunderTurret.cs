using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTurret : Turret
{
    [SerializeField] private GameObject Lighting;
    private bool InChain;
    [SerializeField] private GameObject ChainArea;

    [Header("Thunder Chain")]
    [SerializeField] private int ProbabilityPercentForThunder;
    [SerializeField] private GameObject ThunderLightingFX;
    [SerializeField] private float StopMoveTime;
    [SerializeField] private GameObject ThunderImpactFX;
    [SerializeField] private float Damage;
     private GameObject InChainTurret;
    private GameObject Light2;
    private bool InChainByOther;

    private GameObject InChainByOtherTrans;

    List<GameObject> SubLightingList = new List<GameObject>();
    List<GameObject> InChainByOherList = new List<GameObject>();

    private bool FindOutNullTurret;

    public bool InChain1 { get => InChain; set => InChain = value; }
    public GameObject Lighting1 { get => Lighting; set => Lighting = value; }
    public bool InChainByOther1 { get => InChainByOther; set => InChainByOther = value; }

    public GameObject InChainTurret1 { get => InChainTurret; set => InChainTurret = value; }
    public GameObject InChainByOtherGameOb { get => InChainByOtherTrans; set => InChainByOtherTrans = value; }
    public int ProbabilityPercentForThunder1 { get => ProbabilityPercentForThunder; set => ProbabilityPercentForThunder = value; }
    public GameObject ThunderLightingFX1 { get => ThunderLightingFX; set => ThunderLightingFX = value; }
    public float StopMoveTime1 { get => StopMoveTime; set => StopMoveTime = value; }
    public GameObject ThunderImpactFX1 { get => ThunderImpactFX; set => ThunderImpactFX = value; }
    public float Damage1 { get => Damage; set => Damage = value; }

    void Start()
    {
        InChain1 = false;
        
        Lighting1.SetActive(false);
        FindOutNullTurret = false;

        InChainByOther1 = false;
       
        //lighting2.GetComponent<Transform>().localScale = new Vector3(0, 0, 2);
        //Lighting2.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            Invoke("DestroySelf", 0.5f);
        }
       
        CheckDeadTurret();
        if (InChainTurret1==null)
        {
            InChain1 = false;
            Lighting1.SetActive(false);
        }
        if(InChainByOther1)
        {

            GameObject SubLighting = Instantiate(Lighting1, Lighting1.transform.position, Quaternion.identity);
            SubLighting.SetActive(true);
            SubLighting.transform.LookAt(InChainByOtherGameOb.transform);
            SetLightingScale(SubLighting, transform, InChainByOtherGameOb.transform.parent);
            SubLighting.transform.parent = transform;
            SubLightingList.Add(SubLighting);
            InChainByOherList.Add(InChainByOtherGameOb);

            //SubLighting.transform.parent = InChainByOtherGameOb.transform.parent;
           

            InChainByOther1 = false;
        }
        
        
        
    }
    private void CheckDeadTurret()
    {
        for(int i=0; i < InChainByOherList.Count;i++)
        {
            if(InChainByOherList[i]==null)
            {
                Destroy(SubLightingList[i]);
                InChainByOherList.Remove(InChainByOherList[i]);
            }
        }
        if(InChainByOherList.Count==0)
        {
            if(SubLightingList.Count!=0)
            {
                foreach(var element in SubLightingList)
                {
                    Destroy(element);
                }
            }
        }
        
    }
    private void OnDestroy()
    {
        
    }
    private void SetTransform()
    {
        //float offest = Vector3.Distance(transform.position, InChainTurret1.transform.position);
        //float NewTransform = offest * 0.23f;
        //Lighting1.transform.localScale = new Vector3(0, 0, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Turrent")
        {
            Debug.Log("Find  Turret");
            if(other.GetComponent<ThunderTurret>()!=null)
            {
                CheckDeadTurret();
                ThunderTurret TheTurret = other.GetComponent<ThunderTurret>();
                if(TheTurret.turretType==TurretType.ThunderTurretUpgrade)
                {
                    if (InChain1 == false)
                    {
                        if (TheTurret.InChainTurret1 != this.gameObject)
                        {
                            //SetLightingScale(other.transform);
                            Lighting1.SetActive(true);
                            Lighting1.transform.LookAt(TheTurret.Lighting1.transform);

                            InChainTurret1 = other.gameObject;
                            TheTurret.InChainByOther1 = true;
                            TheTurret.InChainByOtherGameOb = Lighting1;
                            
                            Debug.Log("ooo");
                            InChain1 = true;
                            SetLightingScale(Lighting1,transform, other.transform);

                        }



                        


                    }
                }
                
            }
            
            
        }
        
    }
    private void SetLightingScale(GameObject Lighting,Transform MyTrans,Transform Target)
    {
        
        
       
        float Offset = Vector3.Distance(MyTrans.position , Target.position);
        float NormalOffset = 18f;//this mean the lighting z size is 4 due to that can connect offset is 18 turret.
        float ChangeScale = Offset / NormalOffset;

        //Lighting.transform.localScale = new Vector3(0, 0, Lighting.transform.localScale.z * ChangeScale);
        Lighting.GetComponent<ParticleSystem>().startSpeed = 30f * ChangeScale;

        
    }
    
}
