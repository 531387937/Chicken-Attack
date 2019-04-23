using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleSpeed : MonoBehaviour
{
    private int battleSpeed = 1;
    public TextMeshProUGUI SpeedValue;
    private void Start()
    {
        battleSpeed = 1;
        SpeedValue.text = "x1";
    }
    public void TimeChange()
    {
        switch (battleSpeed)
        {
            case 1:
                battleSpeed++;
                Time.timeScale = 2;
                SpeedValue.text = "x2";
                break;
            case 2:
                battleSpeed++;
                Time.timeScale = 3;
                SpeedValue.text = "x3";
                break;
            case 3:
                battleSpeed=1;
                Time.timeScale = 1;
                SpeedValue.text = "x1";
                break;
        }
    }
}
