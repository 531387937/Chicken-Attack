using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ATK_Train : MonoBehaviour
{
    [Range(0,10)]
    private float KeyValue1;
    private float KeyValue2;
    [SerializeField]
    private float currentValue;
    private float add_Value;
    private bool Training=false;
    private int level = 0;
    public float add;
    public Scrollbar Scroll;
    public Image BackImage;
    public GameObject ATK_Panel;
    public Canvas Old;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Training)
        {
            switch(level)
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
        StartCoroutine(AtkTrain());
        level = 0;
        Old.gameObject.SetActive(false);
        currentValue = 0;
        ATK_Panel.SetActive(true);
        KeyValue1 = Random.Range(0f,7.5f);
        KeyValue2 = (KeyValue1 + 2.5f);
        BackImage.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(15+KeyValue1/3*44, 0, 0);
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
                if(level==4)
                {
                    //弹出结算窗口，显示提升攻击力（根据关卡完成数）
                }
                else
                LevelUp();
            }
            else
            {
                //弹出结算窗口
            }
        }
    }
}
