﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chicken
{
    //鸡的种族
    public enum chickenType { Weak, Rookie, KFC,Mood }
    public chickenType Type;
    //鸡的名字
    public string Name { get; set; }
    //鸡的耐力
    public float HP { get; set; }
    //鸡的寿命
    public int Life;
    //鸡的等级
    //public int Level { get; set; }
    //鸡的经验值
    //public float Exp { get; set; }
    //鸡的攻击力
    public float Attak { get; set; }
    //鸡的速度，影响先后手
    public float Speed { get; set; }
    public Vector3 pos;
    //是否为公鸡
    public bool isCock;
    //鸡是否在场上
    public bool Alive;
    //决定鸡的性别
    [Range(0, 4)]
    public int Gender;
    public Chicken()
    {
        Alive = false;
    }
    //鸡的天赋
    [Range(0,100)]
    private float talent;
    //鸡的稀有度（根据天赋）
    private enum gifts {N,R,SR,SSR,UR}
    private gifts gift;

    //繁育鸡的初始化数值（用于鸡的繁殖）
    public virtual void BirthInitial(Chicken father,Chicken mother)
    {
        Life = Random.Range(8, 10);
        talent = Random.Range(0.0f, 100.0f);
        if (talent >= 95)
        {
            gift = gifts.UR;
        }
        if (talent <= 3)
        {
            Life = Random.Range(12, 15);
            gift = gifts.N;
        }
        if (talent>3&&talent<=24)
        {
            gift = gifts.R;
        }
        if (talent > 24 && talent < 74)
        {
            gift = gifts.SR;
        }
        else
            gift = gifts.SSR;
        float offset=1;
        switch (gift)
        {
            case gifts.N:
                offset = Random.Range(0.9f, 1f);
                Type = chickenType.Mood;
                break;
            case gifts.R:
                offset = Random.Range(1f, 1.08f);
                Type = chickenType.Rookie;
                break;
            case gifts.SR:
                offset = Random.Range(1.08f, 1.15f);
                Type = chickenType.Weak;
                break;
            case gifts.SSR:
                offset = Random.Range(1.15f, 1.2f);
                Type = chickenType.KFC;
                break;
            case gifts.UR:
                offset = Random.Range(1.2f, 1.25f);
                Type = chickenType.KFC;
                break;
        }
        HP = HalfOffset(father.HP, mother.HP, offset);
        Attak = HalfOffset(father.Attak, mother.Attak, offset);
        Speed = HalfOffset(father.Speed, mother.Speed, offset);
        if (Random.Range(0, 3) < 1)
        {
            isCock = false;
        }
        else
            isCock = true;
        pos = new Vector3(Random.Range(-6.0f, 7.0f), Random.Range(-4.0f, 5.0f), 0);
    }

    //鸡的随机初始化（用于商店等）
    public virtual void RandomInitial(int ex)
    {        
        Life = Random.Range(8,10);
        talent = Random.Range(0.0f, 100.0f);
        if (talent >= 95)
        {
            gift = gifts.UR;
        }
        if (talent <= 3)
        {
            Life = Random.Range(12, 15);
            gift = gifts.N;
        }
        if (talent > 3 && talent <= 24)
        {
            gift = gifts.R;
        }
        if (talent > 24 && talent < 74)
        {
            gift = gifts.SR;
        }
        if (talent >= 74 && talent < 95)
        { gift = gifts.SSR; }
        float offset = 1;
        switch (gift)
        {
            case gifts.N:
                offset = Random.Range(0.9f, 1f);
                Type = chickenType.Mood;
                break;
            case gifts.R:
                offset = Random.Range(1f, 1.08f);
                Type = chickenType.Rookie;
                break;
            case gifts.SR:
                offset = Random.Range(1.08f, 1.15f);
                Type = chickenType.Weak;
                break;
            case gifts.SSR:
                offset = Random.Range(1.15f, 1.2f);
                Type = chickenType.KFC;
                break;
            case gifts.UR:
                offset = Random.Range(1.2f, 1.25f);
                Type = chickenType.KFC;
                break;
        }
       switch(ex)
       {
            case 0:
                HP =Mathf.FloorToInt(Random.Range(60.0f,75.0f)*offset);
                Attak = Mathf.FloorToInt(Random.Range(10.0f, 15.0f) * offset);
                Speed = Mathf.FloorToInt(Random.Range(10.0f, 15.0f) * offset);
                break;
            case 1:
                HP =Mathf.FloorToInt(Random.Range(65.0f, 80.0f) * offset);
                Attak = Mathf.FloorToInt(Random.Range(15f, 18f) * offset);
                Speed = Mathf.FloorToInt(Random.Range(15f, 18f) * offset);
                break;
       }
        if (Random.Range(0, 4) < 1)
        {
            isCock = false;
        }
        else
            isCock = true;
        pos = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.0f, 3.0f), 0);
    }

    private int HalfOffset(float fa,float ma,float Offset)
    {
        int value = Mathf.FloorToInt((fa + ma) * 0.5f * Offset);
        return value;
    }
}

