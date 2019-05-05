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
    public int succeed_Num=0;
    private int timer=0;
    public int MissNum = 0;
    private bool train_End;
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
        Train_Chicken = Instantiate(Chicken[(int)GameSaveNew.Instance.ChooseChicken.Type], Pos.transform.position, Pos.transform.rotation);
        Start_Train();
    }

    // Update is called once per frame
    void Update()
    {
        if((MissNum+succeed_Num==maxPaoPao)&&(!train_End))
        {
            Train_End();
        }
    }
    public void Start_Train()
    {
        GameSaveNew.Instance.PD.Pt--;
        Apear_PaoPao();
    }
    void Apear_PaoPao()
    {
        GameObject a;
        timer++;
        a=Instantiate(PaoPaos[Random.Range(0, PaoPaos.Length)], new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0), new Quaternion(0, 0, 0, 1));
        if(timer<maxPaoPao)
        Invoke("Apear_PaoPao", Random.Range(0.3f, 0.8f));
    }

    private void OnEnter(Transform obj)
    {
        succeed_Num++;
        Destroy(obj.gameObject);
    }

    void Train_End()
    {
        train_End = true;
        if (succeed_Num <= 3 && succeed_Num > 0)
        {
            GameSaveNew.Instance.ChooseChicken.Spirit += 1;
        }
        if (succeed_Num <= 5&&succeed_Num>3)
        {
            GameSaveNew.Instance.ChooseChicken.Spirit += 2;
            GameSaveNew.Instance.ChooseChicken.Speed += 0.05f;
        }
        if(succeed_Num>5&&succeed_Num<7)
        {
            GameSaveNew.Instance.ChooseChicken.Spirit += 3;
            GameSaveNew.Instance.ChooseChicken.Speed += 0.1f;
        }
        if (succeed_Num > 7 && succeed_Num < 10)
        {
            GameSaveNew.Instance.ChooseChicken.Spirit += 4;
            GameSaveNew.Instance.ChooseChicken.Speed += 0.15f;
        }
        if(succeed_Num==10)
        {
            GameSaveNew.Instance.ChooseChicken.Spirit += 5;
            GameSaveNew.Instance.ChooseChicken.Speed += 0.2f;
        }
        GameSaveNew.Instance.SaveAllData();
    }
}
