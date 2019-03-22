using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chicken
{
    public enum chickenType { Weak, Rookie, KFC,Mood }
    public chickenType Type;
    //鸡的名字
    public string Name { get; set; }
    //鸡的耐力
    public float HP { get; set; }
    //鸡的生命
    public int Life=8;
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
    public bool Alive;
    //决定鸡的性别
    [Range(0, 4)]
    public int Gender;
    public Chicken()
    {
        Alive = false;
    }
    [Range(0,100)]
    private float talent;
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
        float offset=1;
        switch (gift)
        {
            case gifts.N:
                offset = Random.Range(0.85f, 0.9f);
                Type = chickenType.Mood;
                break;
            case gifts.R:
                offset = Random.Range(0.9f, 0.95f);
                Type = chickenType.Rookie;
                break;
            case gifts.SR:
                offset = Random.Range(0.95f, 1.05f);
                Type = chickenType.Weak;
                break;
            case gifts.SSR:
                offset = Random.Range(1.05f, 1.1f);
                Type = chickenType.KFC;
                break;
            case gifts.UR:
                offset = Random.Range(1.1f, 1.15f);
                Type = chickenType.KFC;
                break;
        }
        HP = HalfOffset(father.HP, mother.HP, offset);
        Attak = HalfOffset(father.Attak, mother.Attak, offset);
        Speed = HalfOffset(father.Speed, mother.Speed, offset);
        if (Random.Range(0, 4) < 1)
        {
            isCock = false;
        }
        else
            isCock = true;
        pos = new Vector3(Random.Range(-7.0f, 8.0f), Random.Range(-4.0f, 5.0f), 0);
    }
    public virtual void RandomInitial(int ex)
    {
        talent = Random.Range(0.0f, 100.0f);
        if (talent >= 95)
        {
            gift = gifts.UR;
        }
        if (talent <= 3)
        {
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
                offset = Random.Range(0.9f, 0.98f);
                Type = chickenType.Mood;
                break;
            case gifts.R:
                offset = Random.Range(0.98f, 1.05f);
                Type = chickenType.Rookie;
                break;
            case gifts.SR:
                offset = Random.Range(1.05f, 1.10f);
                Type = chickenType.Weak;
                break;
            case gifts.SSR:
                offset = Random.Range(1.10f, 1.15f);
                Type = chickenType.KFC;
                break;
            case gifts.UR:
                offset = Random.Range(1.15f, 1.2f);
                Type = chickenType.KFC;
                break;
        }
       switch(ex)
        {
            case 0:
                HP =Mathf.FloorToInt(Random.Range(10.0f,15.0f)*offset);
                Attak = Mathf.FloorToInt(Random.Range(10.0f, 15.0f) * offset);
                Speed = Mathf.FloorToInt(Random.Range(10.0f, 15.0f) * offset);
                break;
            case 1:
                HP =Mathf.FloorToInt(Random.Range(15f, 18f)* offset);
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

