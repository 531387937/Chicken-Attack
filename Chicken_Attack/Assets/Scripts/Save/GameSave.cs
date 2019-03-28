using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Reflection;

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
    public PlayerData PD = new PlayerData();
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
        PD.Chicken_Num= ChickenList.chickenList.Count;
        ChickenUpdate();
    }

    public void AddNewChicken()//新增鸡数据存储方法，新增一只鸡调用
    {
        Chicken ch = new Chicken();
        ChickenList.chickenList.Add(ch);//在静态鸡列表里增加本鸡
        PD.Chicken_Num = ChickenList.chickenList.Count;
        ChickenUpdate();
    }

    public void ReturnChicken(Chicken ch)//鸡战斗结束一只鸡调用 xy19.3.28
    {
        ChickenList.chickenList.Add(ch);//在静态鸡列表里增加本鸡
        PD.Chicken_Num = ChickenList.chickenList.Count;
        ChickenUpdate();
    }

    public void RemoveChicken(Chicken chicken)//剔除鸡数据存储方法，剔除一只鸡调用
    {
        ChickenList.chickenList.Remove(chicken);//在静态鸡列表里剔除本鸡
    }
    //存档
    public void SaveAllData()
    {
        IOHelper.SetData(PlayerPath, PD, Mac);
        IOHelper.SetData(path, ChickenList.chickenList, Mac);
    }
    //读档
    public void LoadAllData()
    {
        if(IOHelper.IsFileExists(PlayerPath))
        {
            PD = IOHelper.GetData(PlayerPath, typeof(PlayerData), Mac) as PlayerData;
        }
        if (IOHelper.IsFileExists(path))
        {
            foreach (GameObject delete in GameObject.FindGameObjectsWithTag("Chicken"))
            {
                Destroy(delete);
            }
            ChickenList.chickenList = IOHelper.GetData(path, typeof(List<Chicken>), Mac) as List<Chicken>;
            AllChicken = new Chicken[ChickenList.chickenList.Count];
            for (int i = 0; i < ChickenList.chickenList.Count; i++)
            {
                AllChicken[i] = ChickenList.chickenList[i];
            }
            Chicken_Before = 0;
            ChickenUpdate();
        }
    }
    public void Remove(int i)
    {
        ChickenList.chickenList.Remove(ChickenList.chickenList[i]);
    }
    //更新鸡的状态
    private void ChickenUpdate()
    {
        for (int i = Chicken_Before; i < ChickenList.chickenList.Count; i++)
        {
            GameObject chicken = Instantiate(BaseChicken);
            chicken.GetComponent<ChiCken_State>().ThisChicken = ChickenList.chickenList[i];
        }
        Chicken_Before = ChickenList.chickenList.Count;
    }
}
