using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[System.Serializable]
public class ChickenAttack:ScriptableObject
    {
        public string Name;
        public double strengh;
        public double Endurance;
        public double Speed;
    }
public class ChickenSave : MonoBehaviour
{
    public string Chicken_Name;
    public double Chicken_strengh;
    public double Chicken_Endurance;
    public double Chicken_Speed;
    public ChickenAttack ccc;
    private void Awake()
    {
        ccc.Name = "No";
    }
    private void Start()
    {
        
        Chicken_Name = ccc.Name;
        Chicken_Endurance = ccc.Endurance;
        Chicken_Speed = ccc.Speed;
        Chicken_strengh = ccc.strengh;
        SaveGame();
    }
    public void SaveGame()
    {
        string dirpath = Application.persistentDataPath + @"/SaveTest";
        IOHelper.CreateDirectory(dirpath);
        string filename = dirpath + @"/GameData.sav";
        print(filename);
        ChickenAttack t = new ChickenAttack();
        //IOHelper.SetData(filename, ccc);
    }
}
