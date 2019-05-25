using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShopUI : MonoBehaviour
{
    [HideInInspector]
    //public ShopSystem ShopSystem;

    private void Start()
    {
        //ShopSystem = GameObject.Find("ShopChickenS").GetComponent<ShopSystem>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                //ShopSystem.ChickenUI.active = true;
                //ShopSystem.ChickenUI.GetComponent<ShopChickenUI>().SetShopChickenUi(ThisChicken, CostChicken);
            }
        }
    }
}
