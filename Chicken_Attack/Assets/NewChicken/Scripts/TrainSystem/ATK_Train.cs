using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ATK_Train : MonoBehaviour
{
    [Range(0, 10)]
    private float KeyValue1;
    private float KeyValue2;
    private float currentValue;
    private float add_Value;
    private bool Training = false;
    private int level = 0;
    public float add;
    public Scrollbar Scroll;
    public Image BackImage;
    public GameObject ATK_Panel;
    public GameObject Pos;
    public GameObject[] Chicken;
    private GameObject Train_Chicken;
    private Animator An;
    // Start is called before the first frame update
    private void Start()
    {
        InitAtk();
        Train_Chicken = Instantiate(Chicken[(int)(GameSaveNew.Instance.playerChicken.Type)], Pos.transform.position, Pos.transform.rotation);
        An = Train_Chicken.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Training)
        {
            switch (level)
            {
                case 0:
                    add = 10;
                    break;
                case 1:
                    add = 12.5f;
                    break;
                case 2:
                    add = 15f;
                    break;
                case 3:
                    add = 17.5f;
                    break;
                case 4:
                    add = 20f;
                    break;
            }
            if (currentValue >= 9.9f)
            {
                add_Value = -add;
            }
            if (currentValue <= 0.1f)
            {
                add_Value = add;
            }

            currentValue += add_Value * Time.deltaTime;
            Scroll.value = currentValue / 10;
        }
    }
    public void InitAtk()
    {
        if (GameSaveNew.Instance.PD.Pt >= 0)
        {
            GameSaveNew.Instance.PD.Pt--;
            StartCoroutine(AtkTrain());
            level = 0;
            currentValue = 0;
            ATK_Panel.SetActive(true);
            KeyValue1 = Random.Range(0f, 7.5f);
            KeyValue2 = (KeyValue1 + 2.5f);
            BackImage.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(15 + KeyValue1 / 3 * 44, 0, 0);
        }
    }
    private void LevelUp()
    {
        level++;
        KeyValue1 = Random.Range(0f, 7.5f);
        KeyValue2 = (KeyValue1 + 2.5f);
        BackImage.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(15 + KeyValue1 / 3 * 44, 0, 0);
        StartCoroutine(AtkTrain());
    }
    IEnumerator AtkTrain()
    {
        yield return new WaitForSeconds(2f);
        Training = true;
    }
    public void Rua()
    {
        if (Training)
        {
            Training = false;
            if (currentValue > KeyValue1 && currentValue < KeyValue2)
            {
                if (level == 4)
                {
                    An.SetTrigger("Attack1");
                    //弹出结算窗口，显示提升攻击力（根据关卡完成数）
                    Invoke("ATK_END", 1.5f);
                    ATK_END();
                }
                else
                    An.SetTrigger("Attack");
                    Invoke("LevelUp",1.5f);
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
        switch (level)
        {
            case 0:
                GameSaveNew.Instance.playerChicken.Power += 1;
                break;
            case 1:
                GameSaveNew.Instance.playerChicken.Power += 5;
                break;
            case 2:
                GameSaveNew.Instance.playerChicken.Power += 9;
                break;
            case 3:
                GameSaveNew.Instance.playerChicken.Power += 14;
                break;
            case 4:
                GameSaveNew.Instance.playerChicken.Power += 20;
                break;
        }
        GameSaveNew.Instance.SaveAllData();
    }
}