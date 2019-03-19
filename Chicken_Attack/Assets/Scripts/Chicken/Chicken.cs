using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken
{
    public enum chickenType { None, Rookie, KFC,Mood }
    public chickenType Type;
    //鸡的名字
    public string Name { get; set; }
    //鸡的耐力
    public double HP { get; set; }
    //鸡的等级
    public int Level { get; set; }
    //鸡的经验值
    public double Exp { get; set; }
    //鸡的攻击力
    public double Attak { get; set; }
    //鸡的速度，影响先后手
    public double Speed { get; set; }
    public Vector3 pos;
    //是否为公鸡
    public bool isCock;
    public bool Alive;
    //决定鸡的性别
    [Range(0, 4)]
    public int Gender;
    public Sprite Sp;
    public Chicken()
    {
        Alive = false;
    }
    [Range(0,100)]
    private double talent;
    private enum gifts {N,R,SR,SSR,UR}
    private gifts gift;
    //繁育鸡的初始化数值
    public virtual void BirthInitial(Chicken father,Chicken mother)
    {
        talent = Random.Range(0.0f, 100.0f);
        if(talent>=95)
        {
            gift = gifts.UR;
        }
        if (talent <=3 )
        {
            gift = gifts.N;
        }
        if(talent>3&&talent<=24)
        {
            gift = gifts.R;
        }
        if (talent > 24 && talent < 74)
        {
            gift = gifts.SR;
        }
        else
            gift = gifts.SSR;
        float offset;
        switch (gift)
        {
            case gifts.N:
                offset = Random.Range(0.85f, 0.9f);
                break;
            case gifts.R:
                offset = Random.Range(0.9f, 0.95f);
                break;
            case gifts.SR:
                offset = Random.Range(0.95f, 1.05f);
                break;
            case gifts.SSR:
                offset = Random.Range(1.05f, 1.1f);
                break;
            case gifts.UR:
                offset = Random.Range(1.1f, 1.15f);
                break;
        }
    }
    public virtual void RandomInitial()
    {

    }
}

