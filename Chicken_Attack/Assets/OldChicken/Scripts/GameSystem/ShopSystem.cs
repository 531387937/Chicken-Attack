using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public Text Pt;
    public Text Gold;
    public Text Prestige;
    public FightChicken[] ShopChicken;
    public GameObject[] ShopChickenUI;
    public Texture[] ChickenTex;
    [HideInInspector]
    public GameObject ChickenUI;

    // Start is called before the first frame update
    void Start()
    {
        ChickenUI = GameObject.FindGameObjectWithTag("ChickenUI");
        ChickenUI.SetActive(false);

        GameSaveNew.Instance.PD.ShopChicken = null;

       
            ShopChicken = new FightChicken[2];        
        //将耗费改为和鸡的战斗力有关
        for(int i = 0;i< ShopChicken.Length; i++)
        {
            ShopChicken[i] = new FightChicken();
            //ShopChicken[i].RandomInitial(Random.Range(0,1));
            ShopChicken[i].InitShopChicken(GameSaveNew.Instance.PD.Prestige,(i+110f)/100f);
            ShopChickenUI[i].GetComponent<ShopChicken>().ThisChicken = ShopChicken[i];
            ShopChickenUI[i].GetComponent<ShopChicken>().CostChicken = Mathf.CeilToInt(ShopChicken[i].Power* 1.33f);
            ShopChickenUI[i].GetComponent<ShopChicken>().Tex.texture = ChickenTex[(int)ShopChicken[i].Type];
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pt.text = GameSaveNew.Instance.PD.Pt.ToString();
        Gold.text = GameSaveNew.Instance.PD.Gold.ToString();
        Prestige.text = GameSaveNew.Instance.PD.Prestige.ToString();
    }
}
