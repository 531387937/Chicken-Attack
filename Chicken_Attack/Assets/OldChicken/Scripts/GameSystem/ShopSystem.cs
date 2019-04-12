﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    private PlayerData PD;
    public Text Pt;
    public Text Gold;
    public Text Prestige;
    public Chicken[] ShopChicken;
    public GameObject[] ShopChickenUI;
    public Texture[] ChickenTex; 

    // Start is called before the first frame update
    void Start()
    {
        PD = GameSave.Instance.PD;
        if (PD.Prestige <= 6)
        {
            ShopChicken = new Chicken[PD.Prestige];
        }
        else if(PD.Prestige > 6)
        {
            ShopChicken = new Chicken[6];
        }

        for(int i = 0;i< ShopChicken.Length; i++)
        {
            ShopChicken[i] = new Chicken();
            ShopChicken[i].RandomInitial(Random.Range(0,1));
            ShopChickenUI[i].GetComponent<ShopChicken>().ThisChicken = ShopChicken[i];
            ShopChickenUI[i].GetComponent<ShopChicken>().CostChicken = Random.Range(1,15);
            ShopChickenUI[i].GetComponent<ShopChicken>().Tex.texture = ChickenTex[(int)ShopChicken[i].Type];
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pt.text = PD.Pt.ToString();
        Gold.text = PD.Gold.ToString();
        Prestige.text = PD.Prestige.ToString();

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if(Physics.Raycast(ray,out hit))
        //{
        //    if (hit.collider.tag == "ShopChicken")
        //    {

        //    }
        //}
    }
}