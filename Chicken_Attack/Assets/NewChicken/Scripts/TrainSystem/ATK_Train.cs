using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ATK_Train : MonoBehaviour
{
    private bool Training = false;
    private int level = 0;
    public float[] add;
    //显示训练效果
    public GameObject Panel;
    public GameObject Pos;
    public GameObject[] Chicken;
    private GameObject Train_Chicken;
    private Animator An;

    public TargetCtr TC;
    // Start is called before the first frame update
    private void Start()
    {
        InitAtk();
        Train_Chicken = Instantiate(Chicken[(int)(GameSaveNew.Instance.playerChicken.Type)], Pos.transform.position, Pos.transform.rotation);
        An = Train_Chicken.GetComponent<Animator>();
        An.SetBool("Scroll",true);
        TC.Speed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //if (Training)
        //{
        //    switch (level)
        //    {
        //        case 0:
        //            add = 10;
        //            break;
        //        case 1:
        //            add = 12.5f;
        //            break;
        //        case 2:
        //            add = 15f;
        //            break;
        //        case 3:
        //            add = 17.5f;
        //            break;
        //        case 4:
        //            add = 20f;
        //            break;
        //    }
        print(Training);
        //}
    }
    public void InitAtk()
    {
        if (GameSaveNew.Instance.PD.Pt >= 0)
        {
            GameSaveNew.Instance.PD.Pt--;
            StartCoroutine(AtkTrain());
            level = 0;
        }
    }
    private void LevelUp()
    {
        level++;
        StartCoroutine(AtkTrain());
    }

    IEnumerator AtkTrain()
    {
        yield return new WaitForSeconds(1f);
        TC.Speed = add[level];
        Training = true;
    }

    public void Rua()
    {
        print("???");
        if (Training)
        {
            TC.Speed = 0;
            Training = false;
            if (TC.targetIn)
            {
                if (level == 4)
                {
                    level++;
                    An.SetTrigger("Attack1");
                    //弹出结算窗口，显示提升攻击力（根据关卡完成数）
                    //Invoke("ATK_END", 1.5f);//会调用两次故停用xy6.5
                    ATK_END();
                }
                else
                    An.SetTrigger("Attack");
                Invoke("LevelUp", 1f);//减少停顿时间xy6.5
                TC.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("Sroll");
            }
            else
            {
                //弹出结算窗口
                ATK_END();
            }
        }
    }
    void ATK_END()
    {
        if(GameSaveNew.Instance.playerChicken.Power < GameSaveNew.Instance.playerChicken.PowerLimit)
        {
            switch (level)
            {
                case 0:
                    //GameSaveNew.Instance.playerChicken.Power += 0;
                    ShowPanel(0);
                    break;
                case 1:
                    //GameSaveNew.Instance.playerChicken.Power += 2;
                    ShowPanel(2);
                    break;
                case 2:
                    //GameSaveNew.Instance.playerChicken.Power += 5;
                    ShowPanel(5);
                    break;
                case 3:
                    //GameSaveNew.Instance.playerChicken.Power += 9;
                    ShowPanel(9);
                    break;
                case 4:
                    //GameSaveNew.Instance.playerChicken.Power += 14;
                    ShowPanel(14);
                    break;
                case 5:
                    //GameSaveNew.Instance.playerChicken.Power += 20;
                    ShowPanel(20);
                    break;
            }
        }
        else
        {
            ShowPanel(0, GameSaveNew.Instance.playerChicken.Name+"的战斗力已达上限\n(不如我们来支一个烤架...)");
        }
        
        //GameSaveNew.Instance.SaveAllData();
    }
    void ShowPanel(int Value)
    {
        Panel.SetActive(true);
        int Inscreace = Mathf.CeilToInt(Value * GameSaveNew.Instance.buffer * Random.Range(0.95f, 1.05f));//造成显示和实际增加不同的原因，实际增加以此为准xy6.5
        Panel.GetComponentInChildren<TextMeshProUGUI>().text ="本次训练中"+ GameSaveNew.Instance.playerChicken.Name + "的战斗力增加了" + Inscreace;
        GameSaveNew.Instance.playerChicken.Power += Inscreace;
        GameSaveNew.Instance.playerChicken.Hungry -= 5;
        GameSaveNew.Instance.SaveAllData();
    }
    void ShowPanel(int Value,string s)//重载战斗力上限显示面板
    {
        Panel.SetActive(true);
        int Inscreace = Mathf.CeilToInt(Value * GameSaveNew.Instance.buffer * Random.Range(0.95f, 1.05f));
        Panel.GetComponentInChildren<TextMeshProUGUI>().text = "本次训练中" + GameSaveNew.Instance.playerChicken.Name + "的战斗力增加了" + Inscreace+s;
        GameSaveNew.Instance.playerChicken.Power += Inscreace;
        GameSaveNew.Instance.playerChicken.Hungry -= 5;
        GameSaveNew.Instance.SaveAllData();
    }
}