using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject m_Board;
    public TextMeshProUGUI TMP;
    public Button YES;
    public Button NO;
    //变暗的遮罩
    public GameObject Mask;
    private bool ReadyChangeScene = false;
    public NamedSystem namedSystem;

    private void Start()
    {
        namedSystem.gameObject.SetActive(false);
    }

    //用于是否前往斗鸡场景
    public void Go_Attack()
    {
        YES.gameObject.SetActive(true);
        NO.gameObject.SetActive(true);
        Mask.SetActive(true);
        Time.timeScale = 0;
        ReadyChangeScene = true;
        m_Board.SetActive(true);
        TMP.text = "确定要前往斗鸡吗？";
        SceneChange.SceneName = "BattleChoose";
    }
    //用于是否前往商店场景
    public void Go_Shop()
    {
        YES.gameObject.SetActive(true);
        NO.gameObject.SetActive(true);
        Mask.SetActive(true);
        Time.timeScale = 0;
        ReadyChangeScene = true;
        m_Board.SetActive(true);
        TMP.text = "确定要前往商店吗？";
        SceneChange.SceneName = "XYShop";
    }

    //用于是否前往繁殖场景
    public void Go_Breed()
    {
        YES.gameObject.SetActive(true);
        NO.gameObject.SetActive(true);
        if (GameSaveNew.Instance.PD.ShopChicken != null)
        {
            Mask.SetActive(true);
            Time.timeScale = 0;
            ReadyChangeScene = true;
            m_Board.SetActive(true);
            TMP.text = "确定要前往繁殖场吗？";
            SceneChange.SceneName = "ChickenBreedScene";
        }
        else 
        {
            Mask.SetActive(true);
            Time.timeScale = 0;
            m_Board.SetActive(true);
            TMP.text = "您还没有在商店购买野鸡";
            YES.gameObject.SetActive(false);
        }
    }

    //前往训练场
    public void Go_Train()
    {
        YES.gameObject.SetActive(true);
        NO.gameObject.SetActive(true);
        if (GameSaveNew.Instance.playerChicken.Hungry > 5 && GameSaveNew.Instance.PD.Pt > 1)
        {
            Mask.SetActive(true);
            Time.timeScale = 0;
            ReadyChangeScene = true;
            m_Board.SetActive(true);
            TMP.text = "确定要前往训练场吗？";
            //switch (GameSaveNew.Instance.PD.HomeLevel)
            //{
            //    case 0:
            //        GameSaveNew.Instance.playerChicken.Hungry -= 15;
            //        break;
            //    case 1:
            //        GameSaveNew.Instance.playerChicken.Hungry -= 10;
            //        break;
            //    case 2:
            //        GameSaveNew.Instance.playerChicken.Hungry -= 5;
            //        break;
            //}
            int a = Random.Range(1, 4);
            switch (a)
            {
                case 1:
                    SceneChange.SceneName = "ATK_Training";
                    break;
                case 2:
                    SceneChange.SceneName = "Strong_Train";
                    break;
                case 3:
                    SceneChange.SceneName = "HP_Train";
                    break;
            }
        }
        else if(GameSaveNew.Instance.PD.Pt > 1)
        {
            Mask.SetActive(true);
            Time.timeScale = 0;
            m_Board.SetActive(true);
            TMP.text = "您的鸡饥饿值太低了,无力训练";
            YES.gameObject.SetActive(false);
        }
        else if (GameSaveNew.Instance.playerChicken.Hungry > 10)
        {
            Mask.SetActive(true);
            Time.timeScale = 0;
            m_Board.SetActive(true);
            TMP.text = "您没有足够的鸡毛参加训练";
            YES.gameObject.SetActive(false);
        }
    }

    public void DoSomething(string Text)
    {
        YES.gameObject.SetActive(true);
        NO.gameObject.SetActive(true);
        Mask.SetActive(true);
        Time.timeScale = 0;
        m_Board.SetActive(true);
        TMP.text = Text;
    }

    public void Yes()
    {
        Time.timeScale = 1;
        if(ReadyChangeScene)
        {
            SceneManager.LoadScene("LoadScene");
        }
    }

    public void No()
    {
        Time.timeScale = 1;
        Mask.SetActive(false);
        ReadyChangeScene = false;
        m_Board.SetActive(false);
    }
}
