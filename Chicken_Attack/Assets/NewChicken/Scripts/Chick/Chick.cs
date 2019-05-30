﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chick : MonoBehaviour
{
    public FightChicken self;
    private GameObject GameUI;
    public GameObject Name_UI;
    private Vector3 Pos;
    [HideInInspector]
    public GameObject Current_Name_UI;

    private void Start()
    {
        GameUI = GameObject.Find("Canvas");
        Name_UI = Resources.Load("Name") as GameObject;
        Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        Current_Name_UI = Instantiate(Name_UI, Pos, Quaternion.identity, GameUI.transform);
        Current_Name_UI.GetComponentInChildren<TextMeshProUGUI>().text = self.Name;
        Current_Name_UI.GetComponent<HungrySlider>().thisChicken = self;
        print(self.Name);

        StartCoroutine(SavePos());
        Invoke("ChangeTag", 1f);//临时避免对小鸡喂食
    }

    IEnumerator SavePos()
    {
        self.Pos = this.transform.position;
        yield return new WaitForSeconds(5);
        StartCoroutine(SavePos());
    }

    void ChangeTag()
    {
        this.tag = "Chick";
    }

    // Update is called once per frame
    void Update()
    {
        Pos = new Vector3(Camera.main.WorldToScreenPoint(this.transform.position).x, Camera.main.WorldToScreenPoint(this.transform.position).y + 50f, 0);
        Current_Name_UI.transform.position = Pos;
        if (self.Grow >= 100)
        {
            Time.timeScale = 0;
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
        }

        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                //喂养小鸡
                //摸摸小鸡动画
                Debug.Log("点击小鸡！！！");
                //Time.timeScale = 0;
                //GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
                //GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 0;
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
        }
    }
}
