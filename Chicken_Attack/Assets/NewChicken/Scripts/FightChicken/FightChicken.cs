using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightChicken 
{
    public enum chickentype
    {
        菜鸡 = 0,
        野鸡 = 1,
        白羽鸡 = 2,
        //肉鸡 = 3
    }
    public chickentype Type;
    public string Name;
    ////鸡的生命值
    //public float HP;
    ////鸡的攻击力
    //public float Attack;
    ////鸡的速度
    //public float Speed;
    ////鸡的斗志
    //public float Spirit;
    ////鸡的气势
    //public float Strong;
    ////鸡的天赋
    public float Power = 100;
    [Range(5,8)]
    public int Talent = 5;
    //鸡的技能组
    //public List<int> Skills = new List<int>();
    //鸡的前辈种族
    public List<FightChicken> Grand = new List<FightChicken>();
    //此生遇到的敌鸡战斗力
    public List<float> enemyChickens = new List<float>();
    //在场景中位置
    public Vector3 Pos = new Vector3(0,Random.Range(0.5f,-3.5f),0);//随机Y值
    //玩家的第几只鸡
    //public int Ch_Num;
    //饥饿值
    public float Hungry = 100;
    //成长值
    public float Grow = 0;
    //是否是小鸡
    public bool Chick = false;
    //退役
    public bool Retire = false;
    //显示退役证书
    //public bool ShowRetirePlane = false;

    /// <summary>
    /// 设置鸡的种族
    /// </summary>
    /// <param name="playerChicken"></param>
    /// <param name="shopChicken"></param>
    /// <returns></returns>
    private chickentype setChickenType(FightChicken playerChicken,FightChicken shopChicken)
    {
        chickentype NewChickenType=0;
        int GrandNum = playerChicken.GetGrand().Count;
        if (playerChicken.GetGrand().Count > 0)
        {
            for (int i = 0; i < GrandNum; i++)
            {
                if (i == 0)
                    NewChickenType = (chickentype)playerChicken.GetGrand()[i].Type;
                else
                {
                    float a = Random.Range(0f, 1f);
                    if (a >= (GrandNum - i) / ((GrandNum + 1) * 2))
                    {
                        NewChickenType = (chickentype)playerChicken.GetGrand()[i].Type;
                    }
                }
            }
        }
        else
        {
            float a = Random.Range(0f, 1f);
            if(a>=0.5f)
            {
                NewChickenType = playerChicken.Type;
            }
            else
            {
                NewChickenType = shopChicken.Type;
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
    //public float getHP()
    //{
    //    return HP;
    //}
    ////获得鸡的攻击力
    //public float GetAttack()
    //{
    //    return Attack;
    //}
    ////获得鸡的速度
    //public float GetSpeed()
    //{
    //    return Speed;
    //}
    ////获得鸡的斗志
    //public float GetSpirit()
    //{
    //    return Spirit;
    //}
    ////获得鸡的气势
    //public float GetStrong()
    //{
    //    return Strong;
    //}
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
    /// 计算鸡的综合战斗力评价（待修改）
    /// </summary>
    /// <returns></returns>
    //public float Getpower()
    //{
    //    return Mathf.Ceil(HP / 30 * Attack / 12 * Speed /1.2f * Spirit / 30 * Strong / 12);
    //}

    /// <summary>
    /// 主角初始的鸡
    /// </summary>
    /// <param name="name"></param>
    public FightChicken(string name)
    {
        Name = name;
        Power = 100;
        Type = 0;
        Hungry = 100;
        Talent = 5;//天赋
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
    public FightChicken(string name,float power,int type,int talent)
    {
        Name = name;
        Power = power;
        Type =(chickentype)type;
        Hungry = 100;
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
        Hungry = 100;
        Name = "小鸡";
        Power = Mathf.Ceil(playerChicken.Power * 0.75f + shopChicken.Power * 0.25f * Random.Range(0.95f, 1.05f));
        Type = setChickenType(playerChicken, shopChicken);
        /*Skills.Add(2);*///待加入技能，从外部读取数据
        Talent = Random.Range(5, 8);
        Chick = true;
        Grand.Add(playerChicken);
    }

    /// <summary>
    /// 设定性别
    /// </summary>
    //public void setCock()
    //{
    //    isCock = (Random.Range(0, 1) == 0 ? false : true);
    //}


    /// <summary>
    /// 用于商店生成的鸡,根据名声生成
    /// </summary>
    /// <param name="fame"></param>
    public void InitShopChicken(int fame,float time)
    {
        string aa = Resources.Load("EnemyData").ToString();
        List<LevelSet> NowLevel= IOHelper.GetData(aa, typeof(List<LevelSet>), 1) as List<LevelSet>;
        //------该行有问题---xy19.5.25
        float New_Power = NowLevel[fame].EnemyChicken.Power;
        Type = (chickentype)Random.Range(0, System.Enum.GetNames(Type.GetType()).Length);
        Name = Type.ToString();
        Power =Mathf.Ceil(New_Power * time * Random.Range(0.95f,1.05f));
        Debug.Log(Power);
        Talent = Random.Range(0, 4) * (1 + fame / 10);
    }

    /// <summary>
    /// 鸡退休简报信息（人数/最强战力）
    /// </summary>
    /// <returns></returns>
    public Vector2 ThisChickenBrief()
    {
        if (enemyChickens.Count > 0)//曾经与他人对战
        {
            float EnemyMaxAttack = 0;
            for (int i = 0;i< enemyChickens.Count; i++)
            {
                if(enemyChickens[i]> EnemyMaxAttack)
                {
                    EnemyMaxAttack = enemyChickens[i];
                }
            }
            return new Vector2(enemyChickens.Count, EnemyMaxAttack);
        }
        return new Vector2(0, 0);
    }
}
