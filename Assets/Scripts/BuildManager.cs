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
    public TurretData fireBallTurretData;
    public TurretData SnowBallTurretData;
    

    //turret to be build
    private TurretData selectdTurretData;


    // gameobject in game
    private MapCubes selectedMapcube;
    public Text moneyText;
    public Animator moneyAnim;
    public static int money = 0;
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;
    private Set_Value PowerUp;

    private bool canBuild;

    //cards
    public click click;
    public bool IsCard;
    
    void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "$" + money;
    }

    private void Start()
    {
        money = 500;
        moneyText.text = "$" + money;
        //click = new click();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + money;
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
                                moneyAnim.SetTrigger("Flicker");
                            }
                        }
                        else if (mapCube.turretGo != null)
                        {// to upGrade 
                            selectedMapcube = mapCube;

                            if (mapCube == selectedMapcube && upgradeCanvas.activeInHierarchy)
                            {
                                HideUpgradeUI();
                            }
                            else
                            {
                                ShowUpgradeUI(mapCube.transform.position, mapCube.isUpGraded);
                            }

                        }
                    }                
                else
                {
                    //RaycastHit hit;
                    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    //IsCard = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Card"));
                }

            }
        }
    }


    //Select turret to build
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectdTurretData = laserTurretData;

        }
        else
        {
            selectdTurretData = null;
        }

    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            canBuild = true;
            selectdTurretData = missileTurretData;
        }
        else
        {
            canBuild = false;
            selectdTurretData = null;
        }

    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            //canBuild = true;
            selectdTurretData = standardTurretData;
        }
        else
        {
            //canBuild = false;
            selectdTurretData = null;
        }
    }

    public void OnFireBallSelected(bool isOn)
    {
        if (isOn)
        {
            selectdTurretData = fireBallTurretData;
        }
        else
        {
            selectdTurretData = null;
        }

    }
    public void OnSnoWBallSelected(bool isOn)
    {
        if (isOn)
        {
            selectdTurretData = SnowBallTurretData;
        }
        else
        {
            selectdTurretData = null;
        }

    }

    // wait to finish in editor

    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }

    void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtomDown()
    {
        if (money >= selectedMapcube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapcube.turretData.costUpgraded);
            selectedMapcube.UpGradeTurret();
        }
        else
        {
            moneyAnim.SetTrigger("Flicker");
        }

        HideUpgradeUI();
    }

    public void OnDestoryButtonDown()
    {
        selectedMapcube.DestroyTurret();
        HideUpgradeUI();
        money += 30;
    }
}
