﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HP_Train : MonoBehaviour
{
    //成功跳跃的数量
    public int SucceedNum = 0;
    //使用的贴图
    public Vector3 tempPos;
    public GameObject[] Chicken;
    private GameObject Train_Chicken;
    public GameObject m_hurdle;
    //栅栏的数量
    public int HurdleNum;
    public GameObject Panel;
    public float JumpSpeed;

    private float MaxSpeed;
    private bool Falling = false;
    private bool Jumping=false;
    // Start is called before the first frame update
    void Start()
    {
        MaxSpeed = JumpSpeed;
        Train_Chicken = Instantiate(Chicken[(int)(GameSaveNew.Instance.playerChicken.Type)], new Vector3(0,1,0), Quaternion.identity);
        Destroy(Train_Chicken.GetComponent<ChickenRun>());
        for (int i=0;i<HurdleNum;i++)
        {
            Instantiate(m_hurdle,tempPos,new Quaternion(0,0,0,1));
            tempPos.x += Random.Range(8f, 13f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(JumpSpeed<=0&&!Falling)
        {
            Down();
        }
        if(Train_Chicken.transform.position.y <= 1.1f&&Falling)
        {
            Train_Chicken.transform.position = new Vector3(Train_Chicken.transform.position.x, 1, Train_Chicken.transform.position.z);
            Land();
        }
        if(Input.GetMouseButtonDown(0)&&!Jumping&&!Falling)
        {
            Jumping = true;
        }
        if(Jumping&&!Falling)
        {
            Jump();
        }
        if(Falling&&Jumping)
        {
            Train_Chicken.transform.position = new Vector3(Train_Chicken.transform.position.x, Train_Chicken.transform.position.y - JumpSpeed * Time.deltaTime, Train_Chicken.transform.position.z);
            JumpSpeed += 14f * Time.deltaTime;
        }
        if(SucceedNum==10)
        {
            Invoke("Train_End", 1f);
        }
    }
    void Jump()
    {
        Train_Chicken.GetComponent<Animator>().SetBool("Jump", true);
        Train_Chicken.transform.position = new Vector3(Train_Chicken.transform.position.x, Train_Chicken.transform.position.y + JumpSpeed * Time.deltaTime, Train_Chicken.transform.position.z);
        JumpSpeed -= 14f* Time.deltaTime;
    }
    void Down()
    {
        Falling = true;
    }
    void Land()
    {
        JumpSpeed = MaxSpeed;
        Jumping = false;
        Falling = false;
        Train_Chicken.GetComponent<Animator>().SetBool("Jump", false);
    }
    public void Train_End()
    {
        Time.timeScale = 0;
        if (SucceedNum < 2)
        {
            GameSaveNew.Instance.playerChicken.Power += 0;
            ShowPanel(0);
        }
        else if (SucceedNum < 4)
        {
            GameSaveNew.Instance.playerChicken.Power += 2;
            ShowPanel(2);
        }
        else if (SucceedNum < 6)
        {
            GameSaveNew.Instance.playerChicken.Power += 5;
            ShowPanel(5);
        }
        else if (SucceedNum < 8)
        {
            GameSaveNew.Instance.playerChicken.Power += 9;
            ShowPanel(9);
        }
        else if (SucceedNum == 9)
        {
            GameSaveNew.Instance.playerChicken.Power += 14;
            ShowPanel(14);
        }
        else if (SucceedNum == 10)
        {
            GameSaveNew.Instance.playerChicken.Power += 20;
            ShowPanel(20);
        }
        }
    void ShowPanel(int Value)
    {
        Panel.SetActive(true);
        int Inscreace = Mathf.CeilToInt(Value * GameSaveNew.Instance.buffer * Random.Range(0.95f, 1.05f));
        Panel.GetComponentInChildren<TextMeshProUGUI>().text = "本次训练中" + GameSaveNew.Instance.playerChicken.Name + "的战斗力增加了" + Inscreace;
    }
}
