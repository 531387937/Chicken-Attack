using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightChicken 
{
    public enum chickentype
    {
        菜鸡 = 0,
        母鸡 = 1,
        飞机 = 2,
        肉鸡 = 3
    }
    public chickentype Type;
    public string Name;
    //鸡的生命值
    public float HP;
    //鸡的攻击力
    public float Attack;
    //鸡的速度
    public float Speed;
    //鸡的斗志
    public float Spirit;
    //鸡的气势
    public float Strong;
    //鸡的公母
    public bool isCock;
    //鸡的天赋
    public int Talent;
    //鸡的技能组
    public List<int> Skills;
    //鸡的前辈种族
    public List<FightChicken> Grand;
    //遇到的敌机
    public FightChicken[] enemyChickens;

    //设置鸡的种族
    private chickentype setChickenType(FightChicken playerChicken,FightChicken shopChicken)
    {
        chickentype NewChickenType=0;
        int GrandNum = playerChicken.GetGrand().Count;
        for(int i =0;i<GrandNum;i++)
        {
            if (i == 0)
                NewChickenType = (chickentype)playerChicken.GetGrand()[i].Type;
            else
            {
                float a = Random.Range(0f, 1f);
                if (a >=(GrandNum- i) / ((GrandNum + 1)*2))
                {
                    NewChickenType = (chickentype)playerChicken.GetGrand()[i].Type;
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
    public float getHP()
    {
        return HP;
    }
    //获得鸡的攻击力
    public float GetAttack()
    {
        return Attack;
    }
    //获得鸡的速度
    public float GetSpeed()
    {
        return Speed;
    }
    //获得鸡的斗志
    public float GetSpirit()
    {
        return Spirit;
    }
    //获得鸡的气势
    public float GetStrong()
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
    public List<FightChicken> GetGrand()
    {
        return Grand;
    }
    //计算鸡的综合战斗力（待修改）
    public float Getpower()
    {
        return HP / 30 * Attack / 5 * Speed / 30 * Spirit / 15 * Strong / 5;
    }
    //主角初始的鸡
    public FightChicken(string name)
    {
        Name = name;
        HP = 60;
        Attack = 10;
        Spirit = 50;
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

    public FightChicken()
    {

    }

    //用于商店生成的鸡,根据名声生成
    public void InitShopChicken(int fame)
    {
        Type = (chickentype)Random.Range(0, System.Enum.GetNames(Type.GetType()).Length);
        Name = "新鲜出炉的" + Type.ToString();
        HP = 60 * (1 + fame/100);
        Attack = 10 * (1 + fame / 100);
        Spirit = 50 * (1 + fame / 100);
        Speed = 5 * (1 + fame / 100);
        Strong = 10 * (1 + fame / 100);
        Talent = 1 * (1 + fame / 100);
    }

    //鸡死亡简报信息（人数/最强战力）
    public Vector2 ThisChickenBrief()
    {
        if (enemyChickens.Length > 0)//曾经与他人对战
        {
            float EnemyMaxAttack = 0;
            for (int i = 0;i< enemyChickens.Length; i++)
            {
                if(enemyChickens[i].Getpower() > EnemyMaxAttack)
                {
                    EnemyMaxAttack = enemyChickens[i].Getpower();
                }
            }
            return new Vector2(enemyChickens.Length, EnemyMaxAttack);
        }
        return new Vector2(0, 0);
    }
}
