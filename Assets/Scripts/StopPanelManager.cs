using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    enum Menu_Type { EnemyMenu,TurretsMenu,Setting};

    [SerializeField] private GameObject EnemyPanel;
    [SerializeField] private GameObject TurretsPanel;
    [SerializeField] private GameObject SettingPanel;

    [SerializeField] private GameObject StopMenuPanel;
    
    
    

    


    //Main Panel
    private List<GameObject> panels;

    [SerializeField] private Dropdown Enemy_PanelsDropDown;

    [SerializeField] private Dropdown Turrets_PanelsDropDown;

    private Menu_Type Current_MenuType;

    

    

    //enemy panel
    private List<GameObject> EnemyPanel_List = new List<GameObject>();


    [SerializeField] List<EnemyStopPanel> enemys = new List<EnemyStopPanel>();

    private EnemyStopPanel Current_enemy;

    private int CurrentEnemyIndex;

    [Header("The is the Enemy panel compoents set parts")]
    [SerializeField] private Text[] SetEnemyTextName;
    [SerializeField] private Image[] SetEnemyImage;
    [SerializeField] private Text SetEnemyIntroduction;
    [SerializeField] private Image SetHp;
    [SerializeField] private Image SetAttack;
    [SerializeField] private Image SetArmor;
    [SerializeField] private Image SetMovement;


    private bool InPanel1or2;   //False 1   true 2

    //Turrets
    private int CurrentTurretIndex;

    private List<GameObject> Turrets_List = new List<GameObject>();

    private int Selected_PanelIndex;

    [SerializeField] List<TurretsPanelValue> Turrets = new List<TurretsPanelValue>();

    [Header("The is the Enemy panel compoents set parts")]
    [SerializeField] private Text[] TurretName;
    [SerializeField] private Image[] TurretImage;
    [SerializeField] private Text TurretIntroduction;
    [SerializeField] private Image TurretDamage;
    [SerializeField] private Image TurretAttackspeed;
    [SerializeField] private Image TurretCost;
    [SerializeField] private Image Upgrade_TurretCost;
    void Start()
    {
        Current_MenuType = StopPanelManager.Menu_Type.EnemyMenu;

        CurrentEnemyIndex = 0;
        
        
        panels = new List<GameObject>();

        panels.Add(EnemyPanel);
        panels.Add(TurretsPanel);
        panels.Add(SettingPanel);

        SetAllPanelsFalse();
        

        EnemyPanel_List.Add(EnemyPanel.transform.GetChild(0).gameObject);  //0 for introduction
        EnemyPanel_List.Add(EnemyPanel.transform.GetChild(1).gameObject);  // 1 for combat information
        EnemyPanel_List.Add(EnemyPanel.transform.GetChild(2).gameObject);  //2 for enemy type 

        DefaultPanel(EnemyPanel);

        Turrets_List.Add(TurretsPanel.transform.GetChild(0).gameObject);
        Turrets_List.Add(TurretsPanel.transform.GetChild(1).gameObject);
        Turrets_List.Add(TurretsPanel.transform.GetChild(2).gameObject);



        
        Enemy_PanelsDropDown.onValueChanged.AddListener(delegate {
            EnemyDropDownValueC(Enemy_PanelsDropDown);
            
        });

        Turrets_PanelsDropDown.onValueChanged.AddListener(delegate {
            TurretsDropDownValueC(Turrets_PanelsDropDown);

        });



        Current_MenuType = StopPanelManager.Menu_Type.EnemyMenu;
        SetSelectedPanel(EnemyPanel);

        //Initialise the Text to say the first value of the Dropdown




    }

    // Update is called once per frame
    void Update()
    {
        
        if (Current_MenuType == StopPanelManager.Menu_Type.EnemyMenu) 
        {
            //SetSelectedPanel(EnemyPanel);
            SetEnemyValue(CurrentEnemyIndex,InPanel1or2);

        }
        if(Current_MenuType == StopPanelManager.Menu_Type.TurretsMenu)
        {
            //SetSelectedPanel(TurretsPanel);
            
            SetTurretsValue(CurrentTurretIndex, InPanel1or2);
        }
        if (Current_MenuType == StopPanelManager.Menu_Type.Setting)
        {
            //SetSelectedPanel(SettingPanel);
        }


    }
    public void Btn_ExitStopMenu()
    {
        StopMenuPanel.SetActive(false);
    }
    public void Btn_Enemy()
    {
        Current_MenuType = StopPanelManager.Menu_Type.EnemyMenu;
        SetSelectedPanel(EnemyPanel);
        DefaultPanel(EnemyPanel);
        SetEnemyValue(CurrentEnemyIndex, InPanel1or2);
    }
    public void Btn_Turrets()
    {
        Current_MenuType = StopPanelManager.Menu_Type.TurretsMenu;
        SetSelectedPanel(TurretsPanel);
        DefaultPanel(TurretsPanel);
        SetTurretsValue(CurrentTurretIndex, InPanel1or2);
        
    }
    public void Btn_Setting()
    {
        Current_MenuType = StopPanelManager.Menu_Type.Setting;
        SetSelectedPanel(SettingPanel);
    }
    void EnemyDropDownValueC(Dropdown change)
    {
        CurrentEnemyIndex = change.value;
        
    }
    void TurretsDropDownValueC(Dropdown change)
    {
        CurrentTurretIndex = change.value;
    }
    private void SetDropDownToSelectPanel()
    {
        //if(panels_Dropdown.)
    }
    private void SetAllPanelsFalse()
    {
        foreach(GameObject element in panels)
        {
            element.SetActive(false);
        }

    }
    private void SetSelectedPanel(GameObject Selectedpanel)
    {
        SetAllPanelsFalse();
        Selectedpanel.SetActive(true);
        
    }
    private void DefaultPanel(GameObject panel)
    {
        if(Current_MenuType == StopPanelManager.Menu_Type.EnemyMenu)
        {
            EnemyPanel_List[0].SetActive(true);
            EnemyPanel_List[1].SetActive(false);
            EnemyPanel_List[2].SetActive(true);
            InPanel1or2 = false;
        }
        if(Current_MenuType == StopPanelManager.Menu_Type.TurretsMenu)
        {
            Turrets_List[0].SetActive(true);
            Turrets_List[1].SetActive(false);
            Turrets_List[2].SetActive(true);
            InPanel1or2 = false;
        }
    }
    private void SetEnemyValue(int enemyindex,bool Inpanel1or2)
    {
        
        if(!Inpanel1or2)
        {
            SetEnemyIntroduction.text = enemys[enemyindex].Introduction1;
            SetEnemyTextName[0].text = enemys[enemyindex].Name1;
            SetEnemyImage[0].sprite = enemys[enemyindex].Sprite1;
        }
        if(Inpanel1or2)
        {
            SetHp.fillAmount = enemys[enemyindex].Hp1;
            SetMovement.fillAmount = enemys[enemyindex].Movement1;
            SetAttack.fillAmount = enemys[enemyindex].Damage1;
            SetArmor.fillAmount = enemys[enemyindex].Shield;
            SetEnemyTextName[1].text = enemys[enemyindex].Name1;
            SetEnemyImage[1].sprite = enemys[enemyindex].Sprite1;
        }
        
        
        
    }
    private void SetTurretsValue(int turretsindex,bool inpanel1or2)
    {
        if (!inpanel1or2)
        {
      
            TurretIntroduction.text = Turrets[turretsindex].Introduction1;
            TurretName[0].text = Turrets[turretsindex].Name1;
            TurretImage[0].sprite = Turrets[turretsindex].Image1;


        }
        if(inpanel1or2)
        {
          

            TurretDamage.fillAmount = Turrets[turretsindex].Damage;
            TurretAttackspeed.fillAmount = Turrets[turretsindex].Attack_Speed1;
            TurretCost.fillAmount = Turrets[turretsindex].Cost1;
            Upgrade_TurretCost.fillAmount = Turrets[turretsindex].Upgrade_Cost1;
            TurretName[1].text = Turrets[turretsindex].Name1;
            TurretImage[1].sprite = Turrets[turretsindex].Image1;
        }
    }

    public void Btn_Next()
    {
        bool RunOneTime = true ;
        if (Current_MenuType == StopPanelManager.Menu_Type.EnemyMenu)
        {
            if (!InPanel1or2 && RunOneTime)
            {
                EnemyPanel_List[1].SetActive(true);
                EnemyPanel_List[0].SetActive(false);
                InPanel1or2 = true;
                RunOneTime = false;
            }
            if (InPanel1or2 && RunOneTime)
            {
                EnemyPanel_List[1].SetActive(false);
                EnemyPanel_List[0].SetActive(true);
                Debug.Log(InPanel1or2);
                InPanel1or2 = false;
                RunOneTime = false;
            }
            SetEnemyValue(CurrentEnemyIndex, InPanel1or2);
        }

            if(Current_MenuType == StopPanelManager.Menu_Type.TurretsMenu)
            {
                if (!InPanel1or2 && RunOneTime)
                {
                    Turrets_List[0].SetActive(false);
                    Turrets_List[1].SetActive(true);
                    
                    InPanel1or2 = true;
                    RunOneTime = false;
                }
                if (InPanel1or2 && RunOneTime)
                {
                    Turrets_List[0].SetActive(true);
                    Turrets_List[1].SetActive(false);
                    InPanel1or2 = false;
                    RunOneTime = false;
                }
               
                SetTurretsValue(CurrentTurretIndex, InPanel1or2);
            }

        }
    }

