using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit_Train : MonoBehaviour
{
    public GameObject[] Chicken;
    public Vector2 min;
    public Vector2 max;
    public GameObject Pos;
    private GameObject Train_Chicken;
    public GameObject[] PaoPaos;
    public int maxPaoPao;
    private bool train_End;
    private int currentNum=1;
    private int level = 3;
    GameObject[] a;
    public int train_Num;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PaoPao.OnEnter += OnEnter;
    }
    private void OnDisable()
    {
        PaoPao.OnEnter -= OnEnter;
    }
    void Start()
    {
        train_Num = GameSaveNew.Instance.ChooseChicken;
        Train_Chicken = Instantiate(Chicken[(int)GameSaveNew.Instance.playerChicken[train_Num].Type], Pos.transform.position,new Quaternion(0,0,0,1));
        Start_Train();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Start_Train()
    {
        GameSaveNew.Instance.PD.Pt--;
        Apear_PaoPao();
    }
    void Apear_PaoPao()
    {
        a=new GameObject[level];
        for (int i = 0; i < level; i++)
        {
            a[i] = Instantiate(PaoPaos[Random.Range(0, PaoPaos.Length)], new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0), new Quaternion(0, 0, 0, 1));
            a[i].GetComponent<PaoPao>().Num = i + 1;
        }
    }

    private void OnEnter(Transform obj)
    {
        if (currentNum == obj.GetComponent<PaoPao>().Num)
        {
            currentNum++;
            obj.GetComponent<PaoPao>().SR.sprite = obj.GetComponent<PaoPao>().Sp;
            obj.GetComponent<Animator>().speed = 0;
        }
        else
        {
            foreach (GameObject q in a)
            {
                Destroy(q);
            }
            Train_End();
        }
        if(currentNum==level+1)
        {
            currentNum = 1;
            foreach (GameObject q in a)
            {
                Destroy(q, 0.5f);
            }
            Invoke("LevelUp", 2f);
        }
    }
    void LevelUp()
    {
        level++;
        if (level < 6)
        {
            Apear_PaoPao();
        }
        if(level==5&&!train_End)
        {
            Train_End();
        }
    }
    public void Train_End()
    {
        train_End = true;
        switch(level)
        {
            case 3:
                break;
            case 4:
                GameSaveNew.Instance.playerChicken[train_Num].Strong += 1;
                break;
            case 5:
                GameSaveNew.Instance.playerChicken[train_Num].Strong += 1;
                GameSaveNew.Instance.playerChicken[train_Num].Speed += 0.1f;
                break;
            case 6:
                GameSaveNew.Instance.playerChicken[train_Num].Strong += 2;
                GameSaveNew.Instance.playerChicken[train_Num].Speed += 0.2f;
                break;
        }
        GameSaveNew.Instance.SaveAllData();
    }
}
