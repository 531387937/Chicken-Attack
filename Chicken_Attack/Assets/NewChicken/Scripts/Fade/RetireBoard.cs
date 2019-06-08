using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RetireBoard : MonoBehaviour
{
    public GameObject Mask;
    [HideInInspector]public MyFightChicken MyFightChicken;
    public TextMeshProUGUI text;
    public GameObject retireBack;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                print("点击到图片");
                Time.timeScale = 1;
                Mask.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void UpDataText()
    {
        text.text = "选择让“" + MyFightChicken.self.Name + "”去做什么呢？\n外出探险会遇到意料之外的事\n卖掉会获得金钱和鸡毛但是会影响声誉";
    }

    public void Sale()
    {
        //卖出操作
        //卖出收益面板
        retireBack.SetActive(true);
        GameSaveNew.Instance.PD.OldChicken.Remove(MyFightChicken.self);
        Destroy(MyFightChicken.gameObject);
        int pt = Random.Range(0, 2);
        retireBack.GetComponent<RetireBack>().pt.text = pt.ToString();
        GameSaveNew.Instance.PD.Pt += pt;
        int gold = Random.Range(100, 300);
        GameSaveNew.Instance.PD.Gold += gold;
        retireBack.GetComponent<RetireBack>().gold.text = gold.ToString();
        GameSaveNew.Instance.PD.Prestige -= 1;
        retireBack.GetComponent<RetireBack>().honer.text = "-1";
        retireBack.GetComponent<RetireBack>().text.text = "售卖本鸡您的声誉减一";
        GameSaveNew.Instance.SaveAllData();
        this.gameObject.SetActive(false);
        //Mask.SetActive(false);
        //Time.timeScale = 1;
    }

    public void OutSide()
    {
        MyFightChicken.self.OutSide = true;
        Destroy(MyFightChicken.gameObject);
        this.gameObject.SetActive(false);
        GameSaveNew.Instance.SaveAllData();
        Mask.SetActive(false);
        Time.timeScale = 1;
    }

}
