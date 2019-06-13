using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    //使用UI的Panel进行添加
    public GameObject[] helpPanel;

    private int currentHelp=0;

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
        }
    }
}
