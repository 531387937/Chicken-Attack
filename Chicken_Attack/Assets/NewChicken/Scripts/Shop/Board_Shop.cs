using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Shop : MonoBehaviour
{
    private FightChicken ChooseChicken;
    private int Cost;
    private Food CurrentFood;
    private bool BuyFood = false;
    private bool BuyHome = false;

    private void Update()
    {
        Debug.Log("Cost:" + Cost);
    }

    /// <summary>
    /// 买鸡
    /// </summary>
    /// <param name="fightChicken"></param>
    /// <param name="cost"></param>
    public void SetSomeThing2Buy(FightChicken fightChicken, int cost)
    {
        ChooseChicken = fightChicken;
        Cost = cost;
    }

    /// <summary>
    /// 买食物
    /// </summary>
    /// <param name="num"></param>
    /// <param name="cost"></param>
    public void SetSomeThing2Buy(Food food, int cost)
    {
        BuyFood = true;
        Cost = cost;
        CurrentFood = food;
    }

    public void Buy()
    {
        if (ChooseChicken != null)
        {
            if (GameSaveNew.Instance.PD.ShopChicken == null && GameSaveNew.Instance.PD.Gold >= Cost)
            {
                GameSaveNew.Instance.PD.Gold -= Cost;
                GameSaveNew.Instance.PD.ShopChicken = ChooseChicken;
                this.gameObject.SetActive(false);
                InitAll();
            }
            else
            {
                //您的金钱不足
                this.gameObject.SetActive(false);
                InitAll();
            }
        }
        else
        {
            if (GameSaveNew.Instance.PD.Gold >= Cost)
            {
                if (BuyFood)
                {
                    if (!GameSaveNew.Instance.PD.FoodRights[(int)CurrentFood])
                    {
                        GameSaveNew.Instance.PD.Gold -= Cost;
                        GameSaveNew.Instance.PD.FoodRights[(int)CurrentFood] = true;
                        Debug.Log("购买食物！！！");
                        foreach( GameObject g in GameObject.FindGameObjectsWithTag("ShopFood"))
                        {
                            g.GetComponent<FoodShopUI>().Fresh();
                        }
                    }
                }
                else if (BuyHome)
                {
                    GameSaveNew.Instance.PD.Gold -= Cost;
                    //升级房屋操作
                }
                this.gameObject.SetActive(false);
                InitAll();
            }
            else
            {
                //您的金钱不足
                this.gameObject.SetActive(false);
                InitAll();
            }
        }
        GameSaveNew.Instance.SaveAllData();
    }

    public void NotBuy()
    {
        InitAll();
        this.gameObject.SetActive(false);
    }

    private void InitAll()
    {
        ChooseChicken = null;
        BuyFood = false;
        BuyHome = false;
    }
}
