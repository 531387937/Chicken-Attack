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

    public ShopSystem ShopSystem;
    private int Cost;
    private FightChicken ChooseChicken;

    private void Start()
    {
        ShopSystem = GameObject.Find("ShopChickenS").GetComponent<ShopSystem>();
    }

    public void SetShopChickenUi(FightChicken fightChicken,int cost)
    {
        Attack.text = "攻击力：" + fightChicken.Attack;
        HP.text = "生命值：" + fightChicken.HP;
        Spirit.text = "斗志" + fightChicken.Spirit;
        Speed.text = "速度" + fightChicken.Speed;
        Strong.text = "气势" + fightChicken.Strong;
        Talent.text = "天赋" + fightChicken.Talent;
        Pics.texture = textures[(int)fightChicken.Type];
        Cost = cost;
        ChooseChicken = fightChicken;
    }

    public void Buy()
    {
        if (ShopSystem.PD.ShopChicken == null)
        {
            ShopSystem.PD.Gold -= Cost;
            ShopSystem.PD.ShopChicken = ChooseChicken;
            this.gameObject.SetActive(false);
        }
        else if(ShopSystem.PD.ShopChicken != null)
        {
            //您只能买一只鸡
            this.gameObject.SetActive(false);
        }
        GameSaveNew.Instance.SaveAllData();
    }

    public void NotBuy()
    {
        this.gameObject.SetActive(false);
    }

}
