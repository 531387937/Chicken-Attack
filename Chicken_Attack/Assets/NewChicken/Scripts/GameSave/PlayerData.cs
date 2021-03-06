﻿using System.Collections;
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


    //主界面需要的新手引导
    public int mainHelp;
    //战斗界面需要的新手引导
    public int battleHelp;
    //ATK训练场需要的新手引导
    public int atkHelp;
    //商店训练场需要的新手引导
    public int shopHelp;
    //繁殖界面需要的新手引导
    public int breedHelp;
    //HP训练场需要的新手引导
    public int hpHelp;
    //Strong训练场需要的新手引导
    public int strongHelp;
    //小鸡长成时的新手引导
    public int growHelp;

    public PlayerData()
    {
        Gold = 200;
        Prestige = 0;
        Pt = 3;
        ShopChicken = null;
        HomeLevel = 0;
        FoodRights = new bool[3] { true, false, false };
        NowLevel = 1;
    }
}
