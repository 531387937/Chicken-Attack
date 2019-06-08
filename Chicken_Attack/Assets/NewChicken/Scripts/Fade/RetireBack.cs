using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RetireBack : MonoBehaviour
{
    public GameObject Mask;
    public TextMeshProUGUI text;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI pt;
    public TextMeshProUGUI honer;
    public Player_Chicken player_Chicken;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                player_Chicken.BoardCheck = true;
                print("点击到图片");
                Time.timeScale = 1;
                Mask.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

}
