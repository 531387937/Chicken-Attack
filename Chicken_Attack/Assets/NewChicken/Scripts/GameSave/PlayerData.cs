using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //玩家的金钱数
    public int Gold;
    //玩家的威望
    public int Prestige;
    //鸡毛的数量，随胜利增加
    public int Pt;
    //商店买到的鸡寄存
    public FightChicken ShopChicken;
    //诞生的小鸡
    public List<FightChicken> Chick = new List<FightChicken>();
    //退役的老鸡
    public List<FightChicken> OldChicken = new List<FightChicken>();
    //商店购买使用食物的权利
    public bool[] FoodRights;
    //现在所到达的关卡
    public int NowLevel;
    //房屋等级
    public int HomeLevel;

    public PlayerData()
    {
        Gold = 200;
        Prestige = 0;
        Pt = 10;
        ShopChicken = null;
        HomeLevel = 0;
        FoodRights = new bool[3] { true, false, false };
        NowLevel = 1;
    }
}
