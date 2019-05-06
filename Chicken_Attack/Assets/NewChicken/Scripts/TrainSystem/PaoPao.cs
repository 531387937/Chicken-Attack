using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PaoPao : MonoBehaviour
{
    public static Action<Transform> OnEnter;
    private GameObject Train_Manager;
    public SpriteRenderer SR;
    public Sprite Sp;
    public int Num;
    public TextMeshPro txt;
    public void OnMouseDown()
    {
        if (OnEnter != null)
        {
            OnEnter(transform);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Train_Manager = GameObject.Find("Train_Manager");
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = Num.ToString();
    }
}
