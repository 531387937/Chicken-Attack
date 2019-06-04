using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chick : MonoBehaviour
{
    public FightChicken self;
    private GameObject GameUI;
    public GameObject Name_UI;
    private Vector3 Pos;
    [HideInInspector]
    public GameObject Current_Name_UI;
    private float Widthscale = 45f / 1080f;

    private void Start()
    {
        GameUI = GameObject.Find("Canvas");
        Name_UI = Resources.Load("Name") as GameObject;
        Pos = Camera.main.WorldToScreenPoint(this.transform.position);
        Current_Name_UI = Instantiate(Name_UI, Pos, Quaternion.identity, GameUI.transform);
        Current_Name_UI.GetComponentInChildren<TextMeshProUGUI>().text = self.Name;
        Current_Name_UI.GetComponent<HungrySlider>().thisChicken = self;
        print(self.Name);

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
        GetComponent<Animator>().SetFloat("Speed", Random.Range(1f, 1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Screen.width * Widthscale" + Screen.width * Widthscale);
        Pos = new Vector3(Camera.main.WorldToScreenPoint(this.transform.position).x, Camera.main.WorldToScreenPoint(this.transform.position).y + (Screen.width * Widthscale), 0);
        Current_Name_UI.transform.position = Pos;
        if (self.Grow >= 100)
        {
            Time.timeScale = 0;
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider == this.gameObject.GetComponent<Collider2D>())
                {
                    Debug.Log(this.gameObject.name +"点击小鸡！！！");
                    this.gameObject.GetComponent<Animator>().SetBool("Touch", true);
                }
            }
        }

        //if (Input.GetMouseButton(0))
        //{
        //    if (GetComponent<Collider2D>().OverlapPoint(Input.mousePosition))
        //    {
        //        //喂养小鸡
        //        //摸摸小鸡动画
        //        Debug.Log("点击小鸡！！！");
        //        //Time.timeScale = 0;
        //        //GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
        //        //GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
        //    }
        //}

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 0;
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.Mask.SetActive(false);
            GameObject.Find("EventSystem").GetComponent<Board>().namedSystem.gameObject.SetActive(true);//小鸡长大
        }
    }
}
