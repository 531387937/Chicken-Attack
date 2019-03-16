using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Reflection;
//鸡的属性
public class Chicken//新建鸡的时候记得生成实例
{
   public enum chickenType {None,Rookie,KFC }
    public chickenType Type = chickenType.None;
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
    public Chicken()
    {
        Alive = false;
    }
}
//玩家的属性
public class PlayerData
{
    //现在鸡的数量
    public int Chicken_Num ;
    //玩家的金钱数
    public int Gold;
    //玩家的威望
    public int Prestige;
    public PlayerData()
    {
        Chicken_Num = 1;
        Gold = 100;
        Prestige = 0;
    }
}

public class ChickenList
{
    //鸡群
    public static List<Chicken> chickenList = new List<Chicken>();
}

public class GameSave : Singleton<GameSave>
{
    public GameObject BaseChicken;
    public static Chicken[] AllChicken;
    private string Mac;//设备MAC
    //private bool CanSave = true;//是否可以保存
    public static PlayerData PD;
    //string path = Application.persistentDataPath + @"/GameData.json";
    string path = "Assets/Resources/GameData.json";
    string PlayerPath = "Assets/Resources/GamePlayerData.json";
    int Chicken_Before = 0;
    void Awake()
    {
        LoadAllData();
        //获取MAC
        NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
        Mac = nis[0].GetPhysicalAddress().ToString() + "0824";
    }

    void Start()
    {
        //--------测试---------
        //Chicken chicken = new Chicken();
        //chicken.Type = Chicken.chickenType.KFC;
        //chicken.Name = chicken.Type.ToString();
        //AddChicken(chicken);
        //---------------------
        
        //if (!IOHelper.IsFileExists(path))
        //{
        //    //如没有则创建空记录文件
        //    IOHelper.SetData(path,ChickenList.chickenList,Mac);
        //    Debug.Log("创建完成");
        //}
        //if (!IOHelper.IsFileExists(PlayerPath))
        //{
        //    //如没有则创建空记录文件
        //    IOHelper.SetData(PlayerPath, PD, Mac);
        //    Debug.Log("创建完成");
        //}
        //if (IOHelper.IsFileExists(path))
        //{
        //    //读取
        //    ChickenList.chickenList = IOHelper.GetData(path, typeof(List<Chicken>), Mac) as List<Chicken>;
        //    Debug.Log("GD:" + ChickenList.chickenList[0].Name);
        //}  
    }

    private void Update()
    {
        //if (CanSave)//存档
        //{
        //    CanSave = false;
        //}
    }

    public void AddChicken(Chicken chicken,string Name)//新增鸡数据存储方法，新增一只鸡调用
    {
        chicken.Name = Name;
        ChickenList.chickenList.Add(chicken);//在静态鸡列表里增加本鸡
    }
    public void AddNewChicken()//新增鸡数据存储方法，新增一只鸡调用
    {
        Chicken ch = new Chicken();
        ChickenList.chickenList.Add(ch);//在静态鸡列表里增加本鸡
        PD.Chicken_Num=ChickenList.chickenList.Count;
        ChickenUpdate();
    }

    public void RemoveChicken(Chicken chicken)//剔除鸡数据存储方法，剔除一只鸡调用
    {
        ChickenList.chickenList.Remove(chicken);//在静态鸡列表里剔除本鸡
    }
    public void SaveAllData()
    {
        SavePlayerData();

    }
    public void LoadAllData()
    {
        foreach (GameObject delete in GameObject.FindGameObjectsWithTag("Chicken"))
        {
            Destroy(delete);
        }
        PD = IOHelper.GetData(PlayerPath, typeof(PlayerData), Mac) as PlayerData;
        ChickenList.chickenList = IOHelper.GetData(path, typeof(List<Chicken>), Mac) as List<Chicken>;
        AllChicken = new Chicken[ChickenList.chickenList.Count];
        for(int i = 0;i< ChickenList.chickenList.Count;i++)
        {
            AllChicken[i] = ChickenList.chickenList[i];
        }
        
        Chicken_Before = 0;
        ChickenUpdate();
    }

    private void SavePlayerData()
    {
        IOHelper.SetData(PlayerPath, PD, Mac);
        IOHelper.SetData(path, ChickenList.chickenList, Mac);
    }
    public void Remove(int i)
    {
        ChickenList.chickenList.Remove(ChickenList.chickenList[i]);
    }
    public void ChickenUpdate()//更新状态
    {
        for (int i = Chicken_Before; i < ChickenList.chickenList.Count; i++)
        {
            GameObject chicken = Instantiate(BaseChicken);
            chicken.GetComponent<ChiCken_State>().ThisChicken = ChickenList.chickenList[i];
        }
        Chicken_Before = ChickenList.chickenList.Count;
    }
}
