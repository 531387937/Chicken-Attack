using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropDownUI_NEW : MonoBehaviour
{
    Dropdown dropDownItem;
    public RawImage ChooseChickenPic;
    public RawImage BuyChickenPic;
    public Texture2D[] Pics;
    public GameObject NewChickUI;

    //public override void OnSelect(BaseEventData eventData)
    //{
    //    base.OnSelect(eventData);
    //    choose = true;
    //}
    private FightChicken chooseChicken = null;

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
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            BuyChickenPic.texture = Pics[(int)GameSaveNew.Instance.PD.ShopChicken.Type];
        }
        NewChickUI.SetActive(false);
    }
    public void Chosen_Chicken(int value)
    {
        switch (value)
        {
            case 0:
                chooseChicken = GameSaveNew.Instance.playerChicken[0];
                ChooseChickenPic.texture = Pics[(int)chooseChicken.Type];
                break;
            case 1:
                chooseChicken = GameSaveNew.Instance.playerChicken[1];
                ChooseChickenPic.texture = Pics[(int)chooseChicken.Type];
                break;
            case 2:
                chooseChicken = GameSaveNew.Instance.playerChicken[2];
                ChooseChickenPic.texture = Pics[(int)chooseChicken.Type];
                break;
        }
    }
    public void Battle_Chicken(int value)
    {
        switch (value)
        {
            case 0:
                GameSaveNew.Instance.ChooseChicken = GameSaveNew.Instance.playerChicken[0];
                break;
            case 1:
                GameSaveNew.Instance.ChooseChicken = GameSaveNew.Instance.playerChicken[1];
                break;
            case 2:
                GameSaveNew.Instance.ChooseChicken = GameSaveNew.Instance.playerChicken[2];
                break;
        }
    }
    //诞生新的鸡，以后加上命名功能
    public void Decided()
    {
        if(chooseChicken != null)
        {
            GameSaveNew.Instance.PD.Chick = new FightChicken("Child", chooseChicken, GameSaveNew.Instance.PD.ShopChicken);
            //鸡诞生动画
            NewChickUI.GetComponent<ShopChickenUI>().SetShopChickenUi(GameSaveNew.Instance.PD.Chick);
            NewChickUI.SetActive(true);
            Debug.Log("生小鸡！！！！");
        }
        //清空商店买的鸡
        GameSaveNew.Instance.PD.ShopChicken = null;
    }
}