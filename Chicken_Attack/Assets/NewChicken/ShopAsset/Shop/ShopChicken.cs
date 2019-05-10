using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChicken : MonoBehaviour
{
    public FightChicken ThisChicken;
    public Text Name;
    public Text Cost;
    public RawImage Tex;
    public int CostChicken;
    [HideInInspector]
    public ShopSystem ShopSystem;

    private void Start()
    {
        ShopSystem = GameObject.Find("ShopChickenS").GetComponent<ShopSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisChicken != null)
        {
            this.gameObject.SetActive(true);
            Name.text = ThisChicken.Power.ToString();
            Cost.text = CostChicken.ToString()+"G";
        }
        else if(ThisChicken == null)
        {
            this.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                ShopSystem.ChickenUI.active = true;
                ShopSystem.ChickenUI.GetComponent<ShopChickenUI>().SetShopChickenUi(ThisChicken,CostChicken);
            }
        }
    }

}
