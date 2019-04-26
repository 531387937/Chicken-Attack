using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightChicken 
{
    public enum chickentype
    {
        菜鸡 = 0,
        木鸡 = 1,
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
    [Range(5,8)]
    public int Talent;
    //鸡的技能组
    public List<int> Skills = new List<int>();
    //鸡的前辈种族
    public List<FightChicken> Grand = new List<FightChicken>();
    //此生遇到的敌机
    public List<FightChicken> enemyChickens = new List<FightChicken>();
    //在场景中位置
    public Vector3 Pos = new Vector3(0,0,0);

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

    /// <summary>
    /// 计算鸡的综合战斗力（待修改）
    /// </summary>
    /// <returns></returns>
    public float Getpower()
    {
        return HP / 30 * Attack / 5 * Speed / 30 * Spirit / 15 * Strong / 5;
    }

    /// <summary>
    /// 主角初始的鸡
    /// </summary>
    /// <param name="name"></param>
    public FightChicken(string name)
    {
        Name = name;
        HP = 60; //生命值
        Attack = 10;//攻击力
        Spirit = 50; //斗志
        Speed = 5; //速度
        Strong = 10;//气势
        Type = 0;
        Talent = 0;//天赋
    }

    /// <summary>
    /// 自定义鸡
    /// </summary>
    /// <param name="name"></param>
    /// <param name="hp"></param>
    /// <param name="attack"></param>
    /// <param name="spirit"></param>
    /// <param name="speed"></param>
    /// <param name="strong"></param>
    /// <param name="type"></param>
    /// <param name="talent"></param>
    public FightChicken(string name,int hp,int attack,int spirit,int speed,int strong,int type,int talent)
    {
        Name = name;
        HP = hp; //生命值
        Attack = attack;//攻击力
        Spirit = spirit; //斗志
        Speed = speed; //速度
        Strong = strong;//气势
        Type =(chickentype)type;
        Talent = talent;//天赋
    }

    public FightChicken()
    {

    }
    
/// <summary>
/// 繁殖得到鸡
/// </summary>
/// <param name="name"></param>
/// <param name="playerChicken"></param>
/// <param name="shopChicken"></param>
    public FightChicken(string name,FightChicken playerChicken,FightChicken shopChicken)
    {
        Name = name;
        FightChicken parent;
        if(playerChicken.Getpower() >= shopChicken.Getpower())
        {
            parent = shopChicken;
        }
        else
        {
            parent = playerChicken;
        }
        HP = Mathf.Floor(parent.getHP() * Random.Range(0.95f, 1.05f));
        Attack = Mathf.Floor(parent.GetAttack() * Random.Range(0.95f, 1.05f));
        Spirit = Mathf.Floor(parent.GetSpirit() * Random.Range(0.95f, 1.05f));
        Speed = Mathf.Floor(parent.GetSpeed() * Random.Range(0.95f, 1.05f));
        Strong = Mathf.Floor(parent.GetStrong() * Random.Range(0.95f, 1.05f));
        Type = setChickenType(playerChicken, shopChicken);
        /*Skills.Add(2);*///待加入技能，从外部读取数据
        Talent = Random.Range(0, 3);
    }

    /// <summary>
    /// 设定性别
    /// </summary>
    public void setCock()
    {
        isCock = (Random.Range(0, 1) == 0 ? false : true);
    }


    /// <summary>
    /// 用于商店生成的鸡,根据名声生成
    /// </summary>
    /// <param name="fame"></param>
    public void InitShopChicken(int fame)
    {
        Type = (chickentype)Random.Range(0, System.Enum.GetNames(Type.GetType()).Length);
        Name = Type.ToString();
        HP = Random.Range(40, 80) * (1 + fame/10);
        Attack = Random.Range(5, 15) * (1 + fame / 10);
        Spirit = Random.Range(45, 65) * (1 + fame / 10);
        Speed = Random.Range(3, 8) * (1 + fame / 10);
        Strong = Random.Range(8, 13) * (1 + fame / 10);
        Talent = Random.Range(0, 4) * (1 + fame / 10);
    }

    /// <summary>
    /// 鸡死亡简报信息（人数/最强战力）
    /// </summary>
    /// <returns></returns>
    public Vector2 ThisChickenBrief()
    {
        if (enemyChickens.Count > 0)//曾经与他人对战
        {
            float EnemyMaxAttack = 0;
            for (int i = 0;i< enemyChickens.Count; i++)
            {
                if(enemyChickens[i].Getpower() > EnemyMaxAttack)
                {
                    EnemyMaxAttack = enemyChickens[i].Getpower();
                }
            }
            return new Vector2(enemyChickens.Count, EnemyMaxAttack);
        }
        return new Vector2(0, 0);
    }
}
