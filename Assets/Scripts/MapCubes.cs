using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCubes : MonoBehaviour
{
    //[HideInInspector]//hide data
    public GameObject turretGo;// save turret data on that cube
    [HideInInspector]
    public TurretData turretData;
    public GameObject buildeffect;
    private Renderer renderer;
    [HideInInspector]
    public bool isUpGraded = false;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpGraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPerfab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    //wait to finish in editor

    public void UpGradeTurret()
    {
        if (isUpGraded == true) return;
        Destroy(turretGo);//destroy current perfab
        //build upgraded perfab
        isUpGraded = true;
        turretGo = GameObject.Instantiate(turretData.TurretUpgradedPrefab, transform.position, Quaternion.identity);

    }

    public void DestroyTurret()
    {
        Destroy(turretGo);//destroy current perfab
        isUpGraded = false;
        turretGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildeffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
    }

    private void OnMouseEnter()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false && gameObject.tag != "RoadCube")
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.tag != "RoadCube")
            renderer.material.color = Color.white;
    }
}
