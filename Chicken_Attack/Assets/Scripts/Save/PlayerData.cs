using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    //现在鸡设的等级
    public int a = 0;
    
    //鸡随鸡舍等级的最大数量
    public int[] MaxChicken = { 1, 3, 5 };
    //现在鸡的最大数量
    public int CurrentMaxChicken;
    //现在鸡的数量
    public int Chicken_Num;
    //玩家的金钱数
    public int Gold;
    //玩家的威望
    public int Prestige;
    //鸡毛的数量，随胜利增加
    public int Pt;
    public PlayerData()
    {
        Chicken_Num = 1;
        Gold = 100;
        Prestige = 0;
        Pt = 10;
        CurrentMaxChicken = MaxChicken[a];
    }
}
