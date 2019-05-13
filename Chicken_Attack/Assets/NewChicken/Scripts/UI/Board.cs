using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Board : MonoBehaviour
{
    public GameObject m_Board;

    public TextMeshProUGUI TMP;
    //变暗的遮罩
    public GameObject Mask;
    private bool ReadyChangeScene=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //用于是否前往斗鸡场景
    public void Go_Attack()
    {
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
        Mask.SetActive(true);
        Time.timeScale = 0;
        ReadyChangeScene = true;
        m_Board.SetActive(true);
        TMP.text = "确定要前往繁殖场吗？";
        SceneChange.SceneName = "ChickenBreedScene";
    }
    public void Go_Train()
    {
        Mask.SetActive(true);
        Time.timeScale = 0;
        ReadyChangeScene = true;
        m_Board.SetActive(true);
        TMP.text = "确定要前往训练场吗？";
        int a = Random.Range(1, 4);
        switch(a)
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
