using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private int level = 1;
    GameObject[] a;
    private List<Vector3> Ve;
    private bool canTouch = false;

    public GameObject Panel;
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
        Ve = new List<Vector3>();
        Train_Chicken = Instantiate(Chicken[(int)GameSaveNew.Instance.playerChicken.Type], Pos.transform.position,new Quaternion(0,0,0,1));
        Start_Train();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Start_Train()
    {
        GameSaveNew.Instance.PD.Pt--;
        //Apear_PaoPao();
        StartCoroutine(apearPaoPao());
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
    //增加难度
    void LevelUp()
    {
        if (level < 5)
        {
            level++;
            StartCoroutine(apearPaoPao());
        }
        else if(level==5&&!train_End)
        {
            level++;
            Train_End();
        }
    }
    public void Train_End()
    {
        train_End = true;
        switch(level)
        {
            case 1:
                GameSaveNew.Instance.playerChicken.Power += 0;
                ShowPanel(0);
                break;
            case 2:
                GameSaveNew.Instance.playerChicken.Power += 2;
                ShowPanel(2);
                break;
            case 3:
                GameSaveNew.Instance.playerChicken.Power += 5;
                ShowPanel(5);
                break;
            case 4:
                GameSaveNew.Instance.playerChicken.Power += 9;
                ShowPanel(9);
                break;
            case 5:
                GameSaveNew.Instance.playerChicken.Power += 14;
                ShowPanel(14);
                break;
            case 6:
                GameSaveNew.Instance.playerChicken.Power += 20;
                ShowPanel(20);
                break;
        }
        GameSaveNew.Instance.SaveAllData();
    }
    //生成谷物气泡
    IEnumerator apearPaoPao()
    {
        int[] Num = new int[level];
        for(int i=0;i<Num.Length;i++)
        {
            Num[i] = i+1;
        }
        Shuffle(Num);
        Ve.Clear();
        canTouch = false;
        a = new GameObject[level];
        for (int i = 0; i < level; i++)
        {
            Vector3 pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y),0);
            for(int c=0;c<Ve.Count;c++)
            {
                if(Vector3.Distance(pos,Ve[c])<=1.2f)
                {
                    pos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);
                    c = 0;
                }
            }
            Ve.Add(pos);
            a[i] = Instantiate(PaoPaos[Random.Range(0, PaoPaos.Length)], pos, new Quaternion(0, 0, 0, 1));
            a[i].GetComponent<PaoPao>().Num = Num[i];
            a[i].GetComponent<Animator>().speed = 0;
            yield return new WaitForSeconds(0.5f);
        }
        foreach(GameObject aa in a)
        {
            aa.GetComponent<Animator>().speed = 1;
            aa.GetComponent<PaoPao>().enabled = true;
            aa.transform.GetChild(0).gameObject.SetActive(false);
        }
        canTouch = true;
    }
    void Shuffle(int[] intArray)
    {
        for (int i = 0; i < intArray.Length; i++)
        {
            int temp = intArray[i];
            int randomIndex = Random.Range(0, intArray.Length);
            intArray[i] = intArray[randomIndex];
            intArray[randomIndex] = temp;
        }
    }

    void ShowPanel(int Value)
    {
        Panel.SetActive(true);
        int Inscreace = Mathf.CeilToInt(Value * GameSaveNew.Instance.buffer * Random.Range(0.95f, 1.05f));
        Panel.GetComponentInChildren<TextMeshProUGUI>().text = "本次训练中" + GameSaveNew.Instance.playerChicken.Name + "的战斗力增加了" + Inscreace;
    }
}
