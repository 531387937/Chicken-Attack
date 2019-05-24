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
    public Image Fill;
    public Color NormalColor = new Color(1, 1, 1, 1);
    public Color NoteColor = new Color(1, 1, 1, 1);
    public Color WarnColor = new Color(1, 1, 1, 1);

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

    IEnumerator Hungry()
    {
        yield return new WaitForSeconds(60);
        thisChicken.Hungry -= 2;
        Debug.Log("饥饿值-2");
        StartCoroutine(Hungry());
    }
}
