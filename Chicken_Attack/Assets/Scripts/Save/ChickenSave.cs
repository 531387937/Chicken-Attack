using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
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
    public ChickenAttack ccc;
    public string Chicken_Name;
    public double Chicken_strengh;
    public double Chicken_Endurance;
    public double Chicken_Speed;
   
    private void Start()
    {
        Chicken_Name = ccc.Name;
        SaveGame();
    }
    public void SaveGame()
    {
        string dirpath = Application.persistentDataPath + @"/SaveTest";
        print(dirpath);
        IOHelper.CreateDirectory(dirpath);
        string filename = dirpath + @"/GameData.sav";
        print(filename);
        ChickenAttack t = new ChickenAttack();
        IOHelper.SetData(filename, ccc);
    }
}
