using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    private Transform Tr;
    public float speed;
    public Sprite[] sprites;

    private HP_Train HT;

    private GameObject TrainC;
    private bool Pass;
    // Start is called before the first frame update
    void Start()
    {
        HT = GameObject.Find("TrainManager").GetComponent<HP_Train>();
        Tr = GetComponent<Transform>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 2)];
    }

    // Update is called once per frame
    void Update()
    {
        if(TrainC==null)
        {
            TrainC= GameObject.FindGameObjectWithTag("FightChicken");
        }
        transform.position -= new Vector3(speed * Time.deltaTime, 0);
        if(TrainC.transform.position.x>=transform.position.x+1&&!Pass)
        {
            Pass = true;
            HT.SucceedNum += 1;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "FightChicken")
        {
            HT.Train_End();
        }
    }
}
