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
                GameObject a = Instantiate(ga[(int)GameSaveNew.Instance.playerChicken.Type], GameSaveNew.Instance.playerChicken.Pos, new Quaternion(0, 0, 0, 1));
                a.AddComponent<MyFightChicken>();
                a.GetComponent<MyFightChicken>().self = GameSaveNew.Instance.playerChicken;
            
        

        //遍历小鸡
        if (GameSaveNew.Instance.PD.Chick != null)
        {
            for (int i = 0; i < GameSaveNew.Instance.PD.Chick.Count; i++)
            {
                GameObject b = Instantiate(ga[4], GameSaveNew.Instance.PD.Chick[i].Pos, new Quaternion(0, 0, 0, 1));
                b.AddComponent<Chick>();
                b.GetComponent<Chick>().self = GameSaveNew.Instance.PD.Chick[i];
            }
        }
        
        StartCoroutine(SaveGame());
    }

    IEnumerator SaveGame()
    {
        yield return new WaitForSeconds(5);
        GameSaveNew.Instance.SaveAllData();
        Debug.Log("保存");
        StartCoroutine(SaveGame());
    }

}