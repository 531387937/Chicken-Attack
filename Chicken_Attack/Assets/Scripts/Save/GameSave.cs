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
}
//玩家的属性
public class PlayerData
{
    //现在鸡的数量
    public int Chicken_Num;
    //玩家的金钱数
    public int Gold;
    //玩家的威望
    public int Prestige;
}

public class ChickenList
{
    //鸡群
    public static List<Chicken> chickenList = new List<Chicken>();
}

public class GameSave : MonoBehaviour
{
    public static int Chicken_Num;
public static int Gold;
public static int Prestige;
    private string Mac;//设备MAC
    private bool CanSave = true;//是否可以保存
    //string path = Application.persistentDataPath + @"/GameData.json";
    string path = "Assets/Resources/GameData.json";
    string PlayerPath = "Assets/Resources/GamePlayerData.json";
    void Awake()
    {
        //获取MAC
        NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
        Mac = nis[0].GetPhysicalAddress().ToString() + "0824";
    }

    void Start()
    {
        //--------测试---------
        Chicken chicken = new Chicken();
        chicken.Type = Chicken.chickenType.KFC;
        chicken.Name = chicken.Type.ToString();
        AddChicken(chicken);
        PlayerData playerData = new PlayerData();
        //---------------------
        
        if (!IOHelper.IsFileExists(path))
        {
            //如没有则创建空记录文件
            IOHelper.SetData(path,ChickenList.chickenList,Mac);
            Debug.Log("创建完成");
        }
        if (!IOHelper.IsFileExists(PlayerPath))
        {
            //如没有则创建空记录文件
            IOHelper.SetData(PlayerPath, playerData, Mac);
            Debug.Log("创建完成");
        }
        if (IOHelper.IsFileExists(path))
        {
            //读取
            ChickenList.chickenList = IOHelper.GetData(path, typeof(List<Chicken>), Mac) as List<Chicken>;
            Debug.Log("GD:" + ChickenList.chickenList[0].Name);
        }  
    }

    private void Update()
    {
        if (CanSave)//存档
        {
            StartCoroutine("SaveData");
            CanSave = false;
        }
    }

    public void AddChicken(Chicken chicken)//新增鸡数据存储方法，新增一只鸡调用
    {
        ChickenList.chickenList.Add(chicken);//在静态鸡列表里增加本鸡
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

    }
    IEnumerator SaveData()
    {
        IOHelper.SetData(path, ChickenList.chickenList, Mac);
        yield return new WaitForSeconds(6f);//每十分钟自动同步一次
        CanSave = true;
    }
    private void SavePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.Chicken_Num = Chicken_Num;
        playerData.Gold = Gold;
        playerData.Prestige = Prestige;
    }
}
