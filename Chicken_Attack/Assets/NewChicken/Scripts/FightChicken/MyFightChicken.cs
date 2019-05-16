using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MyFightChicken : MonoBehaviour
{
    public FightChicken self;
    private GameObject GameUI;
    public GameObject Name_UI;
    private Vector3 Pos;
    private GameObject a;
    private void Start()
    {
        Name_UI = Resources.Load("Name") as GameObject;
        GameUI = GameObject.Find("Canvas");
        Pos = Camera.main.WorldToScreenPoint(this.transform.position);
       a= Instantiate(Name_UI, Pos,Quaternion.identity,GameUI.transform);
        print(self.Name);
        a.GetComponentInChildren<TextMeshProUGUI>().text = self.Name;
        
        StartCoroutine(SavePos());
    }
    private void Update()
    {
        Pos = new Vector3(Camera.main.WorldToScreenPoint(this.transform.position).x, Camera.main.WorldToScreenPoint(this.transform.position).y + 140f, 0);
        a.transform.position = Pos;
    }
    IEnumerator SavePos()
    {
        self.Pos = this.transform.position;
        yield return new WaitForSeconds(5);
        StartCoroutine(SavePos());
    }
}
