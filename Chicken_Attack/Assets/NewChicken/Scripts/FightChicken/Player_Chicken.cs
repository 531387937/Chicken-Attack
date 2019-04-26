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
        Debug.Log(GameSaveNew.Instance.PD.Chick[0].Pos);
        //按照鸡的种类生成鸡
        for (int i = 0; i < GameSaveNew.Instance.PD.ChickenNum; i++)
        {
           GameObject a= Instantiate(ga[(int)GameSaveNew.Instance.playerChicken[i].Type], GameSaveNew.Instance.playerChicken[i].Pos, new Quaternion(0, 0, 0, 1));
            a.AddComponent<MyFightChicken>();
            a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken[i];
        }
        //遍历小鸡
        for (int i = 0; i < GameSaveNew.Instance.PD.Chick.Count; i++)
        {
            GameObject a = Instantiate(ga[4], GameSaveNew.Instance.PD.Chick[i].Pos, new Quaternion(0, 0, 0, 1));
        }

        StartCoroutine(SaveGame());
    }

    IEnumerator SaveGame()
    {
        GameSaveNew.Instance.SaveAllData();
        Debug.Log("保存");
        yield return new WaitForSeconds(60);
        StartCoroutine(SaveGame());
    }

}