using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoodShopUI : MonoBehaviour
{
    private ShopSystem ShopSystem;
    public int Cost;
    public Food food;
    public Material UnActiveGrey;

    private void Start()
    {
        ShopSystem = GameObject.Find("ShopChickenS").GetComponent<ShopSystem>();
        if (GameSaveNew.Instance.PD.FoodRights[(int)food])
        {
            //this.gameObject.SetActive(false);
            gameObject.GetComponentInChildren<RawImage>().material = UnActiveGrey;
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                ShopSystem.Broad.active = true;
                ShopSystem.Broad.GetComponent<Board_Shop>().SetSomeThing2Buy(food, Cost);
                ShopSystem.Broad.GetComponentInChildren<TextMeshProUGUI>().text = "是否花费" + Cost + "G购买" + food.ToString() + "?";
            }
        }
    }

    public void Fresh()
    {
        if (GameSaveNew.Instance.PD.FoodRights[(int)food])
        {
            //this.gameObject.SetActive(false);
            gameObject.GetComponentInChildren<RawImage>().material = UnActiveGrey;
            this.enabled = false;
        }
    }
}
