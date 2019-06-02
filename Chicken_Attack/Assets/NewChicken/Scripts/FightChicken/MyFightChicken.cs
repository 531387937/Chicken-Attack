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
    [HideInInspector]
    public GameObject Current_Name_UI;
    private float Widthscale = 95f / 1080f;

    private void Start()
    {
        GameUI = GameObject.Find("Canvas");
        Name_UI = Resources.Load("Name") as GameObject;
        Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        Current_Name_UI = Instantiate(Name_UI, Pos,Quaternion.identity,GameUI.transform);
        Current_Name_UI.GetComponentInChildren<TextMeshProUGUI>().text = self.Name;
        Current_Name_UI.GetComponent<HungrySlider>().thisChicken = self;
        Current_Name_UI.GetComponent<HungrySlider>().myFightChicken = this;
        print(self.Name);

        StartCoroutine(SavePos());
    }

    private void Update()
    {
        Pos = new Vector3(Camera.main.WorldToScreenPoint(this.transform.position).x, Camera.main.WorldToScreenPoint(this.transform.position).y + (Screen.width * Widthscale), 0);
        Current_Name_UI.transform.position = Pos;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider == this.gameObject.GetComponent<Collider2D>())
                {
                    if (self.Retire)
                    {
                        Debug.Log(this.gameObject.name + "点击退役鸡！！！");
                    }
                    else if (!self.Retire)
                    {
                        Debug.Log(this.gameObject.name + "点击战斗鸡！！！");
                    }
                }
            }
        }
    }
    
    IEnumerator SavePos()
    {
        self.Pos = this.transform.position;
        yield return new WaitForSeconds(5);
        StartCoroutine(SavePos());
    }

    private void OnDestroy()
    {
        Destroy(Current_Name_UI);
    }
}
