using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropDownUI_NEW : MonoBehaviour
{ Dropdown dropDownItem;
    //public override void OnSelect(BaseEventData eventData)
    //{
    //    base.OnSelect(eventData);
    //    choose = true;
    //}
    private FightChicken chooseChicken=null;
    void Start()
    {
        dropDownItem = this.GetComponent<Dropdown>();
        dropDownItem.options.Clear();
        Dropdown.OptionData temoData;
        for(int i = 0;i< GameSaveNew.Instance.playerChicken.Count;i++)
        {
            temoData = new Dropdown.OptionData();
            temoData.text = GameSaveNew.Instance.playerChicken[i].Name;
            dropDownItem.options.Add(temoData);
        }
    }
    public void Chosen_Chicken(int value)
    {
        switch (value)
        {
            case 0:
                chooseChicken = GameSaveNew.Instance.playerChicken[0];
                break;
            case 1:
                chooseChicken = GameSaveNew.Instance.playerChicken[1];
                break;
            case 2:
                chooseChicken = GameSaveNew.Instance.playerChicken[2];
                break;
        }
    }
    //诞生新的鸡，以后加上命名
    public void Decided()
    {
        if(chooseChicken!=null)
        {
            GameSaveNew.Instance.PD.Chick = new FightChicken("Child", chooseChicken, GameSaveNew.Instance.PD.ShopChicken);
        }
        //清空商店买的鸡
        GameSaveNew.Instance.PD.ShopChicken = null;
    }
}