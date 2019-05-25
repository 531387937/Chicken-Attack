using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChickenBreed : MonoBehaviour
{
    public GameObject[] Chicken;
    public RawImage BuyChickenPic;
    public Texture2D[] Pics;
    public GameObject NewChickUI;
    public GameObject eggs;
    public GameObject Pos;
    private GameObject Breed_Chicken;

    //记录上一次的重力感应的Y值
    private float old_y = 0;
    //记录当前的重力感应的Y值
    private float new_y;
    //当前手机晃动的距离
    private float currentDistance = 0;
    //手机晃动的有效距离
    public float distance;
    private bool DoOnce = true;

   
    void Start()
    {
        DoOnce = true;
        eggs.SetActive(false);
        Breed_Chicken = Instantiate(Chicken[(int)GameSaveNew.Instance.playerChicken.Type], Pos.transform.position, new Quaternion(0, 180, 0, 1));
        Breed_Chicken.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            BuyChickenPic.texture = Pics[(int)GameSaveNew.Instance.PD.ShopChicken.Type];
        }
        if (NewChickUI)
        {
            NewChickUI.SetActive(false);
        }
    }

    void Update()
    {
        if (DoOnce)
        {
            new_y = Input.acceleration.y;
            currentDistance = new_y - old_y;
            old_y = new_y;
            if (currentDistance > distance)
            {
                Debug.Log("晃动");
                Three_Eggs();
                Handheld.Vibrate();
                DoOnce = false;
            }
        }
    }

    public void Three_Eggs()
    {
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            eggs.SetActive(true);
        }
    }

    public void Dis(GameObject otherEgg)
    {
        otherEgg.SetActive(false);
    }

    //诞生新的鸡，以后加上命名功能
    public void Decided()
    {
        //eggs.SetActive(false);
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            FightChicken NewChick = new FightChicken("小鸡", GameSaveNew.Instance.playerChicken, GameSaveNew.Instance.PD.ShopChicken);
            GameSaveNew.Instance.PD.Chick = new List<FightChicken>();
            GameSaveNew.Instance.PD.Chick.Add(NewChick);
            //鸡诞生动画
            //NewChickUI.GetComponent<ShopChickenUI>().SetShopChickenUi(NewChick);
            //NewChickUI.SetActive(true);
            Debug.Log("生小鸡！！！！");
            //清空商店买的鸡
            GameSaveNew.Instance.PD.ShopChicken = null;
            Handheld.Vibrate();//震动
        }
    }
}