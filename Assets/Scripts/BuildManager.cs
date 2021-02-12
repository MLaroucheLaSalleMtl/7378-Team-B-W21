using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    //turret to be build
    private TurretData selectdTurretData;
    // gameobject in game
    private MapCubes selectedMapcube;
    public Text moneyText;
    //public Animator moneyAnim;
    private int money = 1000;
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;


    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }

    private void Start()
    {
        moneyText.text = "$" + money;
    }

    // Update is called once per frame
    void Update()
    {
        //Check mouse
        if (Input.GetMouseButtonDown(0))
        {
            //when mouse cilck on UI
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //Buil Turret
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));

                if (isCollider)
                {
                    MapCubes mapCube = hit.collider.GetComponent<MapCubes>();

                    if (selectdTurretData != null && mapCube.turretGo == null)// be able to build
                    {
                        if (money > selectdTurretData.cost)
                        {

                            mapCube.BuildTurret(selectdTurretData);
                            ChangeMoney(-selectdTurretData.cost);
                        }
                        else
                        {
                            // not enough money                           
                            //moneyAnim.SetTrigger("Flicker");
                        }
                    }
                    else if (mapCube.turretGo != null)
                    {// to upGrade 
                        //selectedMapcube = mapCube;


                        //if (mapCube.isUpGraded)
                        //{
                        //    ShowUpgradeUI(mapCube.transform.position, true);
                        //}
                        //else
                        //{
                        //    ShowUpgradeUI(mapCube.transform.position, false);
                        //}
                        //if (mapCube == selectedMapcube && upgradeCanvas.activeInHierarchy)
                        //{
                        //    HideUpgradeUI();
                        //}
                        //else
                        //{
                        //    ShowUpgradeUI(mapCube.transform.position, mapCube.isUpGraded);
                        //}

                    }
                }

            }
        }
    }
        public void OnLaserSelected(bool isOn)
        {
            if(isOn)
            {
                selectdTurretData = laserTurretData;

            }
            else
            {

            }
        }

        public void OnMissileSelected(bool isOn)
        {
            if (isOn)
            {
                selectdTurretData = missileTurretData;
            }
            else
            {

            }
        }

        public void OnStandardSelected(bool isOn)
        {
            if (isOn)
            {
                selectdTurretData = standardTurretData;
            }
            else
            {

            }
        }

    // wait to finish in editor

    //void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    //{
    //    upgradeCanvas.SetActive(true);
    //    upgradeCanvas.transform.position = pos;
    //    buttonUpgrade.interactable = !isDisableUpgrade;
    //}

    //void HideUpgradeUI()
    //{
    //    upgradeCanvas.SetActive(false);
    //}

    //public void OnUpgradeButtomDown()
    //{
    //    if (money >= selectedMapcube.turretData.costUpgraded)
    //    {
    //        ChangeMoney(-selectedMapcube.turretData.costUpgraded);
    //        selectedMapcube.UpGradeTurret();
    //    }
    //    else
    //    {
    //        //moneyAnim.SetTrigger("Flicker");
    //    }

    //    HideUpgradeUI();
    //}

    //public void OnDestoryButtonDown()
    //{
    //    selectedMapcube.DestroyTurret();
    //    HideUpgradeUI();
    //}
}
