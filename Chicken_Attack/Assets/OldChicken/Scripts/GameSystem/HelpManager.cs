using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    //使用UI的Panel进行添加
    public GameObject[] helpPanel;

    private int currentHelp=0;

    public GameObject[] hideObj;
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
        if(hideObj.Length>0)
        {
            foreach (GameObject hide in hideObj)
            {
                hide.SetActive(false);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        switch(help)
        {
            case Help.mainHelp:
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
       
        if(currentHelp<helpPanel.Length)
        {
            helpPanel[currentHelp].SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&currentHelp<helpPanel.Length)
        {
            helpPanel[currentHelp].SetActive(false);
            currentHelp++;
            helpPanel[currentHelp].SetActive(true);
        }
        if(currentHelp == helpPanel.Length)
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
