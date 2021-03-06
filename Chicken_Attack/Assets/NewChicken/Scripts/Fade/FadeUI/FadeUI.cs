﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    Animator Animator;
    Vector2 began = Vector2.zero;
    Vector2 End = Vector2.zero;
    //public int FingerDistance = 5;
    public float AreaScale = 2.5f;
    public float Scale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            if (Input.touches[0].position.x > Screen.width / AreaScale)
                began = Input.touches[0].position;
        }
        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            if (Input.touches[0].position.x > Screen.width / AreaScale)
                End = Input.touches[0].position;
        }
        if(began != Vector2.zero && End != Vector2.zero)
        {
            if (began.x > End.x + Screen.width * Scale)//左滑
            {
                Debug.Log("左滑");
                Animator.SetBool("IsShow", true);
            }
            else if (began.x + Screen.width * Scale < End.x)//右滑
            {
                Debug.Log("右滑");
                Animator.SetBool("IsShow", false);
            }
            began = Vector2.zero;
            End = Vector2.zero;
        }
    }


    ////////////////////////////////////////////////////////////////////
    //                          _ooOoo_                               //
    //                         o8888888o                              //
    //                         88" . "88                              //
    //                         (| ^_^ |)                              //
    //                         O\  =  /O                              //
    //                      ____/`---'\____                           //
    //                    .'  \\|     |//  `.                         //
    //                   /  \\|||  :  |||//  \                        //
    //                  /  _||||| -:- |||||-  \                       //
    //                  |   | \\\  -  /// |   |                       //
    //                  | \_|  ''\---/''  |   |                       //
    //                  \  .-\__  `-`  ___/-. /                       //
    //                ___`. .'  /--.--\  `. . ___                     //
    //              ."" '<  `.___\_<|>_/___.'  >'"".                  //
    //            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
    //            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
    //      ========`-.____`-.___\_____/___.-`____.-'========         //
    //                           `=---='                              //
    //      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
    //         佛祖保佑       永无BUG     永不修改                  //
    ////////////////////////////////////////////////////////////////////

}
