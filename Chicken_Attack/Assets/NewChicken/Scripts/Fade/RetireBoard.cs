using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RetireBoard : MonoBehaviour
{
    public GameObject Mask;
    [HideInInspector]public MyFightChicken MyFightChicken;
    public TextMeshProUGUI text;

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
        this.gameObject.SetActive(false);
        Mask.SetActive(false);
        Time.timeScale = 1;
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
