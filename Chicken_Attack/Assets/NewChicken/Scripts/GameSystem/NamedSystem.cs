using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NamedSystem : MonoBehaviour
{
    public Player_Chicken Player_Chicken;
    public TextMeshProUGUI Text;
    public Button Yes;
    public Button Yes2;
    public string Name;
    public TMP_InputField TMP_In;
    //变暗的遮罩
    public GameObject Mask;
    public GameObject Paper;
    public TextMeshProUGUI enemyCount;
    public TextMeshProUGUI enemyPower;
    public TextMeshProUGUI PaperName;
    private Vector2 countandpower;
    public Image HeadPic;
    public Sprite[] Pics;
    private Sprite Buffer;

    // Start is called before the first frame update
    void Start()
    {
        TMP_In.onEndEdit.AddListener(Input_End);
    }
    
    public void Input_End(string value)
    {
        Name = value;
    }

    public void Sure()
    {
        //Time.timeScale = 1;
        //Mask.SetActive(false);
        //GameSaveNew.Instance.PD.Chick[0].Name = Name;
        GameSaveNew.Instance.playerChicken.Retire = true;
        countandpower = GameSaveNew.Instance.playerChicken.ThisChickenBrief();
        PaperName.text = GameSaveNew.Instance.playerChicken.Name;
        Buffer = Pics[(int)GameSaveNew.Instance.playerChicken.Type];
        GameSaveNew.Instance.PD.OldChicken.Add(GameSaveNew.Instance.playerChicken);
        GameSaveNew.Instance.playerChicken = GameSaveNew.Instance.PD.Chick[0];
        GameSaveNew.Instance.playerChicken.Chick = false;
        GameSaveNew.Instance.PD.Chick.Remove(GameSaveNew.Instance.PD.Chick[0]);
        GameSaveNew.Instance.playerChicken.Name = Name;
        //保存&&刷新
        GameSaveNew.Instance.SaveAllData();
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("FightChicken"))
        {
            Destroy(g.GetComponent<MyFightChicken>().Current_Name_UI);
            Destroy(g);
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Chick"))
        {
            Destroy(g.GetComponent<Chick>().Current_Name_UI);
            Destroy(g);
        }
        GameObject[] gs = GameObject.FindGameObjectsWithTag("RetireChicken");
        foreach (GameObject g in gs)
        {
            Destroy(g.GetComponent<MyFightChicken>().Current_Name_UI);
            Destroy(g);
        }
        if (gs.Length == 0)
        {
            if (GameSaveNew.Instance.PD.OldChicken.Count == 1)
            {
                Player_Chicken.RefreshChicken();
            }
            else
            {
                Player_Chicken.RefreshChickenWithOutRetireChichen();
            }
        }
        else
        {
            Player_Chicken.RefreshChicken();
        }
        Text.text = "恭喜您！在您的精心照料下小鸡长大了\n快来给它起个好听的名字吧！";
        this.gameObject.SetActive(false);
        ShowPaper();
    }

    void ShowPaper()
    {
        Paper.SetActive(true);
        enemyCount.text = countandpower.x.ToString();
        enemyPower.text = countandpower.y.ToString();
        HeadPic.sprite = Buffer;
    }

}
