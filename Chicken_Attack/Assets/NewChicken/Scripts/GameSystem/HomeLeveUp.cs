using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeLeveUp : MonoBehaviour
{
    public int[] Cost;
    public Text te;
    // Start is called before the first frame update
    void Start()
    {
        if(GameSaveNew.Instance.PD.HomeLevel<2)
        te.text = Cost[GameSaveNew.Instance.PD.HomeLevel].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
