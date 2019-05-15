using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChickenBreed : MonoBehaviour
{
    public RawImage ChooseChickenPic;
    public RawImage BuyChickenPic;
    public Texture2D[] Pics;
    public GameObject NewChickUI;

    void Start()
    {
        ChooseChickenPic.texture = Pics[(int)GameSaveNew.Instance.playerChicken.Type];
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            BuyChickenPic.texture = Pics[(int)GameSaveNew.Instance.PD.ShopChicken.Type];
        }
        if (NewChickUI)
        {
            NewChickUI.SetActive(false);
        }
    }

    //诞生新的鸡，以后加上命名功能
    public void Decided()
    {
        if(GameSaveNew.Instance.playerChicken != null)
        {
            FightChicken NewChick = new FightChicken("Child", GameSaveNew.Instance.playerChicken, GameSaveNew.Instance.PD.ShopChicken);
            GameSaveNew.Instance.PD.Chick.Add(NewChick);
            //鸡诞生动画
            NewChickUI.GetComponent<ShopChickenUI>().SetShopChickenUi(NewChick);
            NewChickUI.SetActive(true);
            Debug.Log("生小鸡！！！！");
        }
        //清空商店买的鸡
        GameSaveNew.Instance.PD.ShopChicken = null;
    }
}