using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Text;
using UnityEditor;
using System.Net.NetworkInformation;
using System.Reflection;

public class Chicken//新建鸡的时候记得生成实例
{
    //鸡的属性
    public string Name { get; set; }
    public double HP { get; set; }
    public int Level { get; set; }
    public double Exp { get; set; }
    public int Attak { get; set; }
}

public class ChickenList
{
    //鸡群
    public static List<Chicken> chickenList = new List<Chicken>();
}

public class GameSave : MonoBehaviour
{
    private string Mac;//设备MAC
    private bool CanSave = true;//是否可以保存
    //string path = Application.persistentDataPath + @"/GameData.json";
    string path = "Assets/Resources/GameData.json";

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
        chicken.Name = "!!!!!!!!!!!!!!!";
        AddChicken(chicken);
        //---------------------
        
        if (!IOHelper.IsFileExists(path))
        {
            //如没有则创建空记录文件
            IOHelper.SetData(path,ChickenList.chickenList,Mac);
            Debug.Log("创建完成");
        }
        if (IOHelper.IsFileExists(path))
        {
            //读取
            ChickenList.chickenList =  IOHelper.GetData(path, typeof(List<Chicken>), Mac)as List<Chicken>;
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

    IEnumerator SaveData()
    {
        IOHelper.SetData(path, ChickenList.chickenList, Mac);
        yield return new WaitForSeconds(600f);//每十分钟自动同步一次
        CanSave = true;
    }

}
