using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChickenUI : MonoBehaviour
{
    public Text Attack;
    public Text HP;
    public Text Spirit;
    public Text Speed;
    public Text Strong;
    public Text Talent;
    public RawImage Pics;
    public Texture[] textures;

    private int Cost;
    private FightChicken ChooseChicken;

    /// <summary>
    /// 商店用
    /// </summary>
    /// <param name="fightChicken"></param>
    /// <param name="cost"></param>
    //public void SetShopChickenUi(FightChicken fightChicken,int cost)
    //{
    //    //Attack.text = "攻击力：" + fightChicken.Attack;
    //    //HP.text = "生命值：" + fightChicken.HP;
    //    //Spirit.text = "斗志" + fightChicken.Spirit;
    //    //Speed.text = "速度" + fightChicken.Speed;
    //    //Strong.text = "气势" + fightChicken.Strong;
    //    Talent.text = "天赋" + fightChicken.Talent;
    //    Pics.texture = textures[(int)fightChicken.Type];
    //    Cost = cost;
    //    ChooseChicken = fightChicken;
    //}

    /// <summary>
    /// 繁殖用
    /// </summary>
    /// <param name="fightChicken"></param>
    public void SetShopChickenUi(FightChicken fightChicken)
    {
        //Attack.text = "攻击力：" + fightChicken.Attack;
        //HP.text = "生命值：" + fightChicken.HP;
        //Spirit.text = "斗志" + fightChicken.Spirit;
        //Speed.text = "速度" + fightChicken.Speed;
        //Strong.text = "气势" + fightChicken.Strong;
        Talent.text = "天赋" + fightChicken.Talent;
        Pics.texture = textures[(int)fightChicken.Type];
    }

    public void Buy()
    {
        if (GameSaveNew.Instance.PD.ShopChicken == null && GameSaveNew.Instance.PD.Gold >= Cost)
        {
            GameSaveNew.Instance.PD.Gold -= Cost;
            GameSaveNew.Instance.PD.ShopChicken = ChooseChicken;
            this.gameObject.SetActive(false);
        }
        else
        {
            //您的金钱不足
            this.gameObject.SetActive(false);
        }
        GameSaveNew.Instance.SaveAllData();
    }

    public void NotBuy()
    {
        this.gameObject.SetActive(false);
    }


    //繁殖系统部分

    public void Take()
    {
        GameSaveNew.Instance.SaveAllData();
    }

    public void NotTake()
    {
        GameSaveNew.Instance.PD.Chick = null;
        GameSaveNew.Instance.SaveAllData();
    }

}
