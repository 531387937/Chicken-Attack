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

    // Update is called once per frame
    void Update()
    {
        if (ThisChicken != null)
        {
            this.gameObject.SetActive(true);
            Name.text = ThisChicken.Type.ToString();
            Cost.text = CostChicken.ToString()+"G";
        }
        else if(ThisChicken == null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
