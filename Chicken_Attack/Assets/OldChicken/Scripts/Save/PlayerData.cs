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

    public PlayerData()
    {
        Gold = 100;
        Prestige = 10;
        Pt = 10;
        ShopChicken = null;
    }
}
