using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChicken : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Mychickens;
    private int ActiveNum;
    Chicken[] GetChicken;
    // Start is called before the first frame update
    void Start()
    {
       
        GetChicken = new Chicken[ActiveNum];
        Mychickens = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i<Mychickens.Length; i++)
            Mychickens[i] = gameObject.transform.GetChild(i).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        ActiveNum = GameSave.PD.Chicken_Num;
        for(int a=0;a<ActiveNum;a++)
        {
            Mychickens[a].SetActive(true);
        }
    }
}
