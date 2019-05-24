﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HungrySlider : MonoBehaviour
{
    public FightChicken thisChicken;
    public Slider HungryStrip;
    public TextMeshProUGUI ATK;

    void Start()
    {
        ATK.text = thisChicken.Power.ToString();
        HungryStrip.maxValue = 100;
        StartCoroutine(Hungry());
    }

    private void Update()
    {
        Debug.Log("thisChicken.Hungry" + thisChicken.Hungry);
        HungryStrip.value = thisChicken.Hungry;
    }

    IEnumerator Hungry()
    {
        yield return new WaitForSeconds(60);
        thisChicken.Hungry -= 2;
        Debug.Log("饥饿值-2");
        StartCoroutine(Hungry());
    }
}