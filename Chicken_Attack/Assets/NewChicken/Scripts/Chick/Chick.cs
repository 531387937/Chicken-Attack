using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                //喂养小鸡
            }
        }
    }
}
