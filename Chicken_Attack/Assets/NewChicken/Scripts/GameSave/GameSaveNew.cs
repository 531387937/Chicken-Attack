using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Reflection;

public class GameSaveNew : Singleton<GameSaveNew>
{
    private string Mac;//设备MAC
    public FightChicken playerChicken;
    public PlayerData PD = new PlayerData();
    string path = "Assets/Resources/GameData.json";
    string PlayerPath = "Assets/Resources/GamePlayerData.json";

    //之后用这个来表现对训练的影响浮动值
    public float buffer=1.0f;

    void Awake()
    {
        //导出时切换
        path = GetDataPath() + "/GameData.json";
        PlayerPath = GetDataPath() + "/GamePlayerData.json";
        LoadAllData();
        //获取MAC
        NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
        Mac = nis[0].GetPhysicalAddress().ToString() + "0824";
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!IOHelper.IsFileExists(path))
        {
            //如没有则创建空记录文件
            playerChicken = new FightChicken("初始鸡");
            IOHelper.SetData(path, playerChicken, Mac);
            Debug.Log("创建完成");
        }
        if (!IOHelper.IsFileExists(PlayerPath))
        {
            //如没有则创建空记录文件
            IOHelper.SetData(PlayerPath, PD, Mac);
            Debug.Log("创建完成");
        }
    }
    
    public void SavePlayerData()
    {
        IOHelper.SetData(PlayerPath, PD, Mac);
    }

    public void SaveChickenData()
    {
        IOHelper.SetData(path, playerChicken, Mac);
    }

    //存档
    public void SaveAllData()
    {
        IOHelper.SetData(PlayerPath, PD, Mac);
        IOHelper.SetData(path, playerChicken, Mac);
    }

    //读档
    public void LoadAllData()
    {
        if (IOHelper.IsFileExists(PlayerPath))
        {
            PD = IOHelper.GetData(PlayerPath, typeof(PlayerData), Mac) as PlayerData;
        }
        if (IOHelper.IsFileExists(path))
        {          
            playerChicken = IOHelper.GetData(path, typeof(FightChicken), Mac) as FightChicken;           
        }
    }

    private static string GetDataPath()//获取路径
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)//如果是iphone
        {
            string path = Application.persistentDataPath;
                                                         
            return path;
        }
        else if (Application.platform == RuntimePlatform.Android)//如果是android
        {
            return Application.persistentDataPath;
        }
        else
            return Application.dataPath;
    }
}