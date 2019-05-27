using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
    public FightChicken self;

    private void Start()
    {
        StartCoroutine(SavePos());
        Invoke("ChangeTag", 1f);//临时避免对小鸡喂食
    }

    IEnumerator SavePos()
    {
        self.Pos = this.transform.position;
        yield return new WaitForSeconds(5);
        StartCoroutine(SavePos());
    }

    void ChangeTag()
    {
        this.tag = "Chick";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                //喂养小鸡
                //摸摸小鸡动画
            }
        }
    }
}
