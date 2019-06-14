using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    //使用UI的Panel进行添加
    public GameObject[] helpPanel;

    private int currentHelp = 0;

    public GameObject[] hideObj;

    public GameObject growHelp;
    //根据场景改变枚举
    public enum Help
    {
        mainHelp,
        battleHelp,
        atkHelp,
        shopHelp,
        breedHelp,
        hpHelp,
        strongHelp
}
    public Help help;
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        switch(help)
        {
            case Help.mainHelp:
                if(GameSaveNew.Instance.PD.growHelp==1)
                {
                    GameSaveNew.Instance.PD.growHelp++;
                    Time.timeScale = 0;
                    growHelp.SetActive(true);
                }
                currentHelp = GameSaveNew.Instance.PD.mainHelp;
                break;
            case Help.battleHelp:
                currentHelp = GameSaveNew.Instance.PD.battleHelp;
                break;
            case Help.atkHelp:
                currentHelp = GameSaveNew.Instance.PD.atkHelp;
                break;
            case Help.breedHelp:
                currentHelp = GameSaveNew.Instance.PD.breedHelp;
                break;
            case Help.hpHelp:
                currentHelp = GameSaveNew.Instance.PD.hpHelp;
                break;
            case Help.strongHelp:
                currentHelp = GameSaveNew.Instance.PD.strongHelp;
                break;
            case Help.shopHelp:
                currentHelp = GameSaveNew.Instance.PD.shopHelp;
                break;
        }
       
        if(!GameSaveNew.Instance.playerChicken.FirstChicken && currentHelp < helpPanel.Length)
        {
            helpPanel[currentHelp].SetActive(true);
            Time.timeScale = 0;
        }
        if (hideObj.Length>0&& currentHelp==0)
        {
            foreach (GameObject hide in hideObj)
            {
                hide.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (growHelp != null)
        {
            if (growHelp.gameObject.activeSelf == true && Input.GetMouseButtonDown(0))
            {
                growHelp.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (!GameSaveNew.Instance.playerChicken.FirstChicken && Input.GetMouseButtonDown(0) && currentHelp < helpPanel.Length)
        {
            helpPanel[currentHelp].SetActive(false);
            currentHelp++;
            if (currentHelp < helpPanel.Length)
                helpPanel[currentHelp].SetActive(true);
        }
        if(!GameSaveNew.Instance.playerChicken.FirstChicken && currentHelp == helpPanel.Length)
        {
            currentHelp++;
            Time.timeScale = 1;
            if (hideObj.Length > 0)
            {
                foreach (GameObject hide in hideObj)
                {
                    hide.SetActive(true);
                }
            }
            switch (help)
            {
                case Help.mainHelp:
                    GameSaveNew.Instance.PD.mainHelp=currentHelp;
                    break;
                case Help.battleHelp:
                    GameSaveNew.Instance.PD.battleHelp=currentHelp;
                    break;
                case Help.atkHelp:
                   GameSaveNew.Instance.PD.atkHelp=currentHelp;
                    break;
                case Help.breedHelp:
                    GameSaveNew.Instance.PD.breedHelp= currentHelp;
                    break;
                case Help.hpHelp:
                   GameSaveNew.Instance.PD.hpHelp= currentHelp;
                    break;
                case Help.strongHelp:
                    GameSaveNew.Instance.PD.strongHelp= currentHelp ;
                    break;
                case Help.shopHelp:
                     GameSaveNew.Instance.PD.shopHelp=currentHelp ;
                    break;
            }
        }
    }
}
