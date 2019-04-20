using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class Player_Chicken : MonoBehaviour
{
    public GameObject[] ga; 
    // Start is called before the first frame update
    void Start()
    {
        //按照鸡的种类生成鸡
        for (int i = 0; i < GameSaveNew.Instance.PD.ChickenNum; i++)
        {
           GameObject a= Instantiate(ga[(int)GameSaveNew.Instance.playerChicken[i].Type], new Vector3(i * 2, 0, 0), new Quaternion(0, 0, 0, 1));
            a.AddComponent<MyFightChicken>();
            a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken[i];
        }
    }   
}