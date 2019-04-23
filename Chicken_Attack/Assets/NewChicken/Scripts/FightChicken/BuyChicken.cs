using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyChicken : MonoBehaviour
{
    //得到买的鸡
    private FightChicken buyChicken;
    //用于显示买来的鸡
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        buyChicken = GameSaveNew.Instance.PD.ShopChicken;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //展示买来的鸡的属性
    void ShowShopChicken()
    {

    }
}
