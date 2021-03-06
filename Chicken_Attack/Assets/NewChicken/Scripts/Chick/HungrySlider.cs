﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HungrySlider : MonoBehaviour
{
    public FightChicken thisChicken;
    public MyFightChicken myFightChicken;
    public GameObject Name;
    public Slider HungryStrip;
    public TextMeshProUGUI ATK;
    public Image Fill;
    public Color NormalColor = new Color(1, 1, 1, 1);
    public Color NoteColor = new Color(1, 1, 1, 1);
    public Color WarnColor = new Color(1, 1, 1, 1);
    public Color GrowColor = new Color(1, 1, 1, 1);
    public Sprite RetireTex;
    public Sprite FightChickneTex;
    public Sprite ChickTex;
    public Image HeadTex;

    void Start()
    {
        if (thisChicken.Retire)
        {
            //HeadTex.sprite = RetireTex;
            Destroy(HeadTex);
            Destroy(HungryStrip.gameObject);
            Destroy(ATK.gameObject);
            myFightChicken.tag = "RetireChicken";
            myFightChicken.GetComponent<Animator>().SetFloat("SpeedScale", Random.Range(0.8f, 1.5f));
            Destroy(this);
        }
        else if (thisChicken.Chick)
        {
            //HeadTex.sprite = ChickTex;
            Destroy(HeadTex);
            Destroy(ATK.gameObject);
            Fill.color = GrowColor;
            HungryStrip.maxValue = 100;
            StartCoroutine(GrowUp());
        }
        else if (!thisChicken.Chick)
        {
            HeadTex.sprite = FightChickneTex;
            ATK.text = thisChicken.Power.ToString();
            HungryStrip.maxValue = 100;
            StartCoroutine(Hungry());
        }
    }

    private void Update()
    {
        if (thisChicken.Chick)
        {
            //Debug.Log("thisChicken.Grow" + thisChicken.Grow);
            HungryStrip.value = thisChicken.Grow;
        }
        else if (!thisChicken.Chick)
        {
            //Debug.Log("thisChicken.Hungry" + thisChicken.Hungry);
            HungryStrip.value = thisChicken.Hungry;
            if (thisChicken.Hungry > 60)
            {
                Fill.color = NormalColor;
            }
            else if (thisChicken.Hungry > 40)
            {
                Fill.color = NoteColor;
            }
            else if (thisChicken.Hungry < 40)
            {
                Fill.color = WarnColor;
            }
        }
    }

    IEnumerator GrowUp()
    {
        //switch (GameSaveNew.Instance.PD.HomeLevel)
        //{
        //    case 0:
        //        iTween.ValueTo(gameObject, iTween.Hash("from", thisChicken.Grow, "to", thisChicken.Grow + 2, "time", 90, "onupdate", "UpdateGrow"));
        //        break;
        //    case 1:
        //        iTween.ValueTo(gameObject, iTween.Hash("from", thisChicken.Grow, "to", thisChicken.Grow + 4, "time", 90, "onupdate", "UpdateGrow"));
        //        break;
        //    case 2:
        //        iTween.ValueTo(gameObject, iTween.Hash("from", thisChicken.Grow, "to", thisChicken.Grow + 6, "time", 90, "onupdate", "UpdateGrow"));
        //        break;
        //}
        yield return new WaitForSeconds(1);
        switch (GameSaveNew.Instance.PD.HomeLevel)
        {
            case 0:
                thisChicken.Grow += 2f / 90f;
                Debug.Log("成长值+"+ 2f / 90f);
                StartCoroutine(GrowUp());
                break;
            case 1:
                thisChicken.Grow += 4f / 90f;
                Debug.Log("成长值+"+ 4f / 90f);
                StartCoroutine(GrowUp());
                break;
            case 2:
                thisChicken.Grow += 6f / 90f;
                Debug.Log("成长值+"+ 6f / 90f);
                StartCoroutine(GrowUp());
                break;
        }
        //StartCoroutine(GrowUp());
        if (thisChicken.Grow > 100)
        {
            thisChicken.Grow = 100;
        }
    }

    void UpdateGrow(object obj)
    {
        thisChicken.Grow = (float)obj;
        //Debug.Log("成长值:"+ (float)obj);
    }

    IEnumerator Hungry()
    {
        //switch (GameSaveNew.Instance.PD.HomeLevel)
        //{
        //    case 0:
        //        iTween.ValueTo(gameObject, iTween.Hash("from", thisChicken.Hungry, "to", thisChicken.Hungry - 6, "time", 60, "onupdate", "UpdateHungry"));
        //        break;
        //    case 1:
        //        iTween.ValueTo(gameObject, iTween.Hash("from", thisChicken.Hungry, "to", thisChicken.Hungry - 4, "time", 60, "onupdate", "UpdateHungry"));
        //        break;
        //    case 2:
        //        iTween.ValueTo(gameObject, iTween.Hash("from", thisChicken.Hungry, "to", thisChicken.Hungry - 2, "time", 60, "onupdate", "UpdateHungry"));
        //        break;
        //}
        yield return new WaitForSeconds(1);
        switch (GameSaveNew.Instance.PD.HomeLevel)
        {
            case 0:
                thisChicken.Hungry -= 6f / 60f;
                Debug.Log("饥饿值-"+ 6f / 60f);
                StartCoroutine(Hungry());
                break;
            case 1:
                thisChicken.Hungry -= 4f / 60f;
                Debug.Log("饥饿值-"+ 4f / 60f);
                StartCoroutine(Hungry());
                break;
            case 2:
                thisChicken.Hungry -= 2f / 60f;
                Debug.Log("饥饿值-"+ 2f / 60f);
                StartCoroutine(Hungry());
                break;
        }
        //StartCoroutine(Hungry());
        if (thisChicken.Hungry < 0)
        {
            thisChicken.Hungry = 0;
        }
    }

    void UpdateHungry(object obj)
    {
        thisChicken.Hungry = (float)obj;
        //Debug.Log("饥饿值:"+ (float)obj);
    }

}
