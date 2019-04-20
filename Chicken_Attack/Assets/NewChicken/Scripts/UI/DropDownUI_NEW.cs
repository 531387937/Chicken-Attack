using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropDownUI_NEW : MonoBehaviour, IPointerDownHandler
{
    public ChickenBreed_New CB_New;
    private Chicken[] aaa;
    public bool IsCock;
    public Image back;
    //public string[] showText;
    public Sprite[] sprite;
    Dropdown dropDownItem;
    //List<string> temoNames;
    List<Sprite> sprite_list;
    private bool choose=false;
    public GameObject a;
    public Image other_img;//任意的img，用作被赋值替换
    private int awsl = 0;
     void Start()
    {
        dropDownItem = this.GetComponent<Dropdown>();
        //temoNames = new List<string>();
        sprite_list = new List<Sprite>();
        AddNames();
        UpdateChickenMessage();
       
        //UpdateDropDownItem(temoNames);

    }
    //public override void OnSelect(BaseEventData eventData)
    //{
    //    base.OnSelect(eventData);
    //    choose = true;
    //}
    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateChickenMessage();
        choose = true;
    }
    void UpdateChickenMessage()
    {
        aaa = new Chicken[ChickenList.chickenList.Count];
        sprite_list.Clear();
        awsl = 0;
        dropDownItem.options.Clear();
        Dropdown.OptionData temoData;
        for (int i = 0; i < ChickenList.chickenList.Count; i++)
        {
            if (ChickenList.chickenList[i].isCock==IsCock)
            {
                aaa[awsl] = ChickenList.chickenList[i];
                temoData = new Dropdown.OptionData();
                temoData.text = "生命值:" + ChickenList.chickenList[i].HP + "\n" + "攻击力：" + ChickenList.chickenList[i].Attak + "\n" + "速度:" + ChickenList.chickenList[i].Speed;
                sprite_list.Add(sprite[(int)ChickenList.chickenList[i].Type]);
                //back.GetComponent<Image>().sprite = sprite_list[(int)ChickenList.chickenList[i].Type];
                dropDownItem.options.Add(temoData);
                awsl++;
            }
            else
                continue;
        }
        dropDownItem.captionText.text = "No";
        other_img.sprite = sprite_list[0];
        dropDownItem.captionImage = other_img;
    }
    //void UpdateDropDownItem(List<string> showNames)
    //{
    //    dropDownItem.options.Clear();
    //    Dropdown.OptionData temoData;
    //    for (int i = 0; i < showNames.Count; i++)
    //    {
    //        //给每一个option选项赋值
    //        temoData = new Dropdown.OptionData();
    //        temoData.text = showNames[i];
    //        //temoData.image = sprite_list[i];
    //        //temoData.image = sprite_list[i];
    //        back.GetComponent<Image>().sprite = sprite_list[i];
    //        dropDownItem.options.Add(temoData);
    //    }
    //    //初始选项的显示
    //    dropDownItem.captionText.text = showNames[0];
    //    other_img.sprite = sprite_list[0];
    //    dropDownItem.captionImage = other_img;

    //}
    void Update()
    {
        back.GetComponent<Image>().sprite = sprite_list[dropDownItem.value];
        chickenChange();
        if(aaa[dropDownItem.value].Life<=0)
        {
            UpdateChickenMessage();
        }
        if (choose)
        {
            a = transform.GetChild(3).gameObject;
            if(a==null)
            {
                return;
            }
            a = a.transform.GetChild(0).GetChild(0).gameObject;
            for (int i = 1; i <= awsl; i++)
            {
                a.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = sprite_list[i - 1];
            }
            choose = false;
        }
        this.GetComponent<Image>().sprite = other_img.sprite;
    }
    void AddNames()
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            sprite_list.Add(sprite[i]);
        }
    }
    public void chickenChange()
    {
        if(IsCock)
        {
            CB_New.gongji = aaa[dropDownItem.value];
        }
        if(!IsCock)
        {
            CB_New.muji = aaa[dropDownItem.value];
        }
    }
}
