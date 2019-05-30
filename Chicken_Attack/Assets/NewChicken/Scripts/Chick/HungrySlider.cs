using System.Collections;
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
    public Image HeadTex;

    void Start()
    {
        if (thisChicken.Retire)
        {
            HeadTex.sprite = RetireTex;
            Destroy(HungryStrip.gameObject);
            Destroy(ATK.gameObject);
            myFightChicken.tag = "RetireChicken";
            Destroy(this);
        }
        if (thisChicken.Chick)
        {
            Destroy(ATK.gameObject);
            Fill.color = GrowColor;
            HungryStrip.maxValue = 100;
            StartCoroutine(GrowUp());
        }
        else if (!thisChicken.Chick)
        {
            ATK.text = thisChicken.Power.ToString();
            HungryStrip.maxValue = 100;
            StartCoroutine(Hungry());
        }
    }

    private void Update()
    {
        if (thisChicken.Chick)
        {
            Debug.Log("thisChicken.Grow" + thisChicken.Grow);
            HungryStrip.value = thisChicken.Grow;
        }
        else if (!thisChicken.Chick)
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
    }

    IEnumerator GrowUp()
    {
        yield return new WaitForSeconds(90);
        switch (GameSaveNew.Instance.PD.HomeLevel)
        {
            case 0:
                thisChicken.Grow += 2;
                Debug.Log("成长值+2");
                StartCoroutine(GrowUp());
                break;
            case 1:
                thisChicken.Grow += 4;
                Debug.Log("成长值+4");
                StartCoroutine(GrowUp());
                break;
            case 2:
                thisChicken.Grow += 6;
                Debug.Log("成长值+6");
                StartCoroutine(GrowUp());
                break;
        }
        if (thisChicken.Hungry < 0)
        {
            thisChicken.Hungry = 0;
        }
    }

    IEnumerator Hungry()
    {
        yield return new WaitForSeconds(60);
        switch (GameSaveNew.Instance.PD.HomeLevel)
        {
            case 0:
                thisChicken.Hungry -= 6;
                Debug.Log("饥饿值-6");
                StartCoroutine(Hungry());
                break;
            case 1:
                thisChicken.Hungry -= 4;
                Debug.Log("饥饿值-4");
                StartCoroutine(Hungry());
                break;
            case 2:
                thisChicken.Hungry -= 2;
                Debug.Log("饥饿值-2");
                StartCoroutine(Hungry());
                break;
        }
        if (thisChicken.Hungry < 0)
        {
            thisChicken.Hungry = 0;
        }
    }
}
