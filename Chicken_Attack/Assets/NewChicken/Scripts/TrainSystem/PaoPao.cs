using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class PaoPao : MonoBehaviour
{
    public static Action<Transform> OnEnter;
    private GameObject Train_Manager;
    private float timer = 0;
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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>2)
        {
            Destroy(this.gameObject);
        }
    }
    public void Miss()
    {
        Train_Manager.GetComponent<Spirit_Train>().MissNum++;
    }
}
