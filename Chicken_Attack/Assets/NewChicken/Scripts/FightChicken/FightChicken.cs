﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightChicken 
{
   public enum chickentype {caiji,muji,feiji,rouji }
    public chickentype Type;
    public string Name;
    //鸡的生命值
    public double HP;
    //鸡的攻击力
    public double Attack;
    //鸡的速度
    public double Speed;
    //鸡的斗志
    public double Spirit;
    //鸡的气势
    public double Strong;
    //鸡的公母
    public bool isCock;
    //鸡的天赋
    public int Talent;
    //鸡的技能组
    public List<int> Skills;
    //鸡的前辈种族
    public List<int> Grand;
    //设置鸡的种族
    private chickentype setChickenType(FightChicken playerChicken,FightChicken shopChicken)
    {
        chickentype NewChickenType=0;
        int GrandNum = playerChicken.GetGrand().Count;
        for(int i =0;i<GrandNum;i++)
        {
            if (i == 0)
                NewChickenType = (chickentype)playerChicken.GetGrand()[i];
            else
            {
                double a = Random.Range(0f, 1f);
                if (a >=(GrandNum- i) / ((GrandNum + 1)*2))
                {
                    NewChickenType = (chickentype)playerChicken.GetGrand()[i];
                }
            }
        }
        return NewChickenType;
    }
    //获得鸡的名字
    public string getName()
    {
        return Name;
    }
    //获得鸡的生命值
    public double getHP()
    {
        return HP;
    }
    //获得鸡的攻击力
    public double GetAttack()
    {
        return Attack;
    }
    //获得鸡的速度
    public double GetSpeed()
    {
        return Speed;
    }
    //获得鸡的斗志
    public double GetSpirit()
    {
        return Spirit;
    }
    //获得鸡的气势
    public double GetStrong()
    {
        return Strong;
    }
    //检查鸡的公母
    public bool CheckCock()
    {
        return isCock;
    }
    //获得鸡的天赋
    public int GetTalent()
    {
        return Talent;
    }
    public List<int> GetGrand()
    {
        return Grand;
    }
    //计算鸡的综合战斗力（待修改）
    public double Getpower()
    {
        return HP / 30 * Attack / 5 * Speed / 30 * Spirit / 15 * Strong / 5;
    }
    //主角初始的鸡
    public FightChicken(string name)
    {
        Name = name;
        HP = 60;
        Attack = 10;
        Spirit = 20;
        Speed = 5;
        Strong = 10;
        Type = 0;
        Talent = 0;
    }
    //繁殖得到的鸡
    public FightChicken(string name,FightChicken playerChicken,FightChicken shopChicken)
    {
        Name = name;
        FightChicken parent;
        if(playerChicken.Getpower()>=shopChicken.Getpower())
        {
            parent = shopChicken;
        }
        else
        {
            parent = playerChicken;
        }
        HP = parent.getHP() * Random.Range(0.95f, 1.05f);
        Attack = parent.GetAttack()* Random.Range(0.95f, 1.05f);
        Spirit = parent.GetSpirit()*Random.Range(0.95f, 1.05f);
        Speed = parent.GetSpeed()* Random.Range(0.95f, 1.05f);
        Strong = parent.GetStrong()* Random.Range(0.95f, 1.05f);
        Type = setChickenType(playerChicken, shopChicken);
        /*Skills.Add(2);*///待加入技能，从外部读取数据
        Talent = Random.Range(0, 3);
    }
    public void setCock()
    {
        isCock = (Random.Range(0, 1) == 0 ? false : true);
    }
    //用于商店生成的鸡,根据名声生成
    public FightChicken(int fame)
    {

    }
    public FightChicken()
    {

    }
}