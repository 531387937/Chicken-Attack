﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    public GameObject Mask;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                print("点击到图片");
                Time.timeScale = 1;
                Mask.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }
}
