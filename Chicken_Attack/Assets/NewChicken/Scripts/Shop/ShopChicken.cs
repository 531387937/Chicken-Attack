using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopChicken : MonoBehaviour
{
    public FightChicken ThisChicken;
    public Text Name;
    public Text Cost;
    public RawImage Tex;
    [HideInInspector]
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
            Name.text = "母鸡（" + ThisChicken.Power.ToString() + ")"+"\n"+"种群："+ ThisChicken.Type.ToString();
            Cost.text = CostChicken.ToString() + "G";
        }
        else if (ThisChicken == null)
        {
            this.gameObject.SetActive(false);
        }

        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                ShopSystem.Broad.active = true;
                ShopSystem.Broad.GetComponent<Board_Shop>().SetSomeThing2Buy(ThisChicken, CostChicken);
                ShopSystem.Broad.GetComponentInChildren<TextMeshProUGUI>().text = "是否花费" + Cost.text + "购买" + Name.text + "?";
            }
        }
    }

}
