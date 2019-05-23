using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg2Chicken : MonoBehaviour
{
    public GameObject a;

    public void ToChi()
    {
        a.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
