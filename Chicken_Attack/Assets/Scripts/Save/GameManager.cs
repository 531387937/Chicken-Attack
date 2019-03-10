using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using LitJson;
using Newtonsoft.Json;
using System;

public class GameManager : MonoBehaviour
{
    
    public GameObject saveObj;
    public Dictionary<string, ChickenSave> chickenSave = new Dictionary<string, ChickenSave>();
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private static string SerializeObject(object pObject)
    {
        //序列化后的字符串
        string serializedString = string.Empty;
        //使用Json.Net进行序列化
        serializedString = JsonConvert.SerializeObject(pObject);
        return serializedString;
    }
    private static object DeserializeObject(string pString, Type pType)
    {
        //反序列化后的对象
        object deserializedObject = null;
        //使用Json.Net进行反序列化
        deserializedObject = JsonConvert.DeserializeObject(pString, pType);
        return deserializedObject;
    }
    //public void SaveByJson()
    //{
    //    ChickenSave CS = new ChickenSave();
    //    //CS.ga = saveObj;
    //    //CS.strengh = 10;
    //    //CS.Speed = 12;
    //    //CS.Name = "faster";
    //    //CS.Endurance = 1;
    //    //chickenSave.Add(CS.Name, CS);
    //    //Save save = new Save();
    //    //save.money = money;
    //    //save.score = score;
    //    string jsonStr = JsonMapper.ToJson(chickenSave);
    //    //string jsonStr = JsonMapper.ToJson(save);
    //    using (StreamWriter sw = new StreamWriter(@"C:\Users\棋子\Desktop\SaveTest.txt"))
    //    { sw.Write(jsonStr); }
    //}
    //public void LoadByJson()
    //{
    //    //chickenSave.Clear();
    //    //if (File.Exists(@"C:\Users\棋子\Desktop\SaveTest.txt"))
    //    //{
    //    //    FileStream fs = new FileStream(@"C:\Users\棋子\Desktop\SaveTest.txt", FileMode.Open);
    //    //    StreamReader sr = new StreamReader(fs);
            
    //    //        JsonData values = JsonMapper.ToObject(sr.ReadToEnd());
    //    //        foreach(var key in values.Keys)
    //    //    {

    //    //    }
                
            
    //    //}
    //}
}
