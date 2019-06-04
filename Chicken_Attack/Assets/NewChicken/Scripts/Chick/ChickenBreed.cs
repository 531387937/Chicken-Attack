using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChickenBreed : MonoBehaviour
{
    public GameObject[] Chicken;
    //public RawImage BuyChickenPic;
    //public Texture2D[] Pics;
    public GameObject NewChickUI;
    public GameObject eggs;
    public GameObject Pos;
    public GameObject BuyChickenPos;
    private GameObject Breed_Chicken;
    float timer = 0;
    private bool btn = false;
    private bool btn1 = false;
    public Image dark;

    public GameObject Light;
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
           GameObject g = Instantiate(Chicken[(int)GameSaveNew.Instance.PD.ShopChicken.Type], BuyChickenPos.transform.position, new Quaternion(0, 0, 0, 1));
            g.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            //BuyChickenPic.texture = Pics[(int)GameSaveNew.Instance.PD.ShopChicken.Type];
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
                //Handheld.Vibrate();
                DoOnce = false;
            }
        }
        if (btn)
        {
            timer += Time.deltaTime;
            dark.color = new Color(0, 0, 0, timer);
        }
        if(!btn&&btn1)
        {
        timer -= Time.deltaTime;
        dark.color = new Color(0, 0, 0, timer);
        }
        print(timer);
    }

    public void Three_Eggs()
    {
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            StartCoroutine(Dark());
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
            //GameSaveNew.Instance.PD.Chick = new List<FightChicken>();
            GameSaveNew.Instance.PD.Chick.Add(NewChick);
            //鸡诞生动画
            //NewChickUI.GetComponent<ShopChickenUI>().SetShopChickenUi(NewChick);
            //NewChickUI.SetActive(true);
            Debug.Log("生小鸡！！！！");
            //清空商店买的鸡
            GameSaveNew.Instance.PD.ShopChicken = null;
            Handheld.Vibrate();//震动
            Invoke("ChangeScene2Main", 3f);
        }
    }

    void ChangeScene2Main()
    {
        SceneManager.LoadScene("QiZiNewChicken");
    }
    IEnumerator Dark()
    {
        //yield return new WaitForSeconds(0.5f);
        btn = true;
        timer = 0;
        yield return new WaitForSeconds(2);
        btn = false;
        Light.SetActive(false);
        btn1 = true;
        timer = 1;
        eggs.SetActive(true);
    }
}