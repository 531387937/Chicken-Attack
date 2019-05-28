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
                Debug.Log("点击小鸡！！！");
                //Time.timeScale = 0;
                //GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
                //GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 0;
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
        }
    }
}
