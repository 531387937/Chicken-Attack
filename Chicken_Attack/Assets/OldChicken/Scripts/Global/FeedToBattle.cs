using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FeedToBattle : MonoBehaviour
{
    public Chicken BeforeBattlechicken;//传入鸡
    public Chicken AfterBattlechicken;//传出鸡
    private GameObject ChoiceChicken;//当前选中的鸡寄存
    private static bool isNoDestroyHandler = true;//是否没有DontDestroyOnLoad处理
    private bool battle = false;//是否对战
    private bool cancel = false;//取消对战
    public GameObject BattleUI;
    public bool Do = false;
    public static string NextScene;
    // Start is called before the first frame update
    void Start()
    {
        if (isNoDestroyHandler)
        {
            isNoDestroyHandler = false;
            DontDestroyOnLoad(this.gameObject);
            BattleUI = GameObject.Find("BattleUI");
            BattleUI.SetActive(false);
        }
        else if (!isNoDestroyHandler)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "XYTest")//在喂食场景中
        {
            if (Do)
            {
                if (AfterBattlechicken != null)
                {
                    this.GetComponent<SpriteRenderer>().enabled = true;
                    this.GetComponent<Collider2D>().enabled = true;
                    AfterBattlechicken.pos = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);//将鸡放归于场景中
                    GameSave.Instance.ReturnChicken(AfterBattlechicken);
                    GameSave.Instance.SaveAllData();//存档
                    Debug.Log("鸡鸡战斗归来！！！");
                    Debug.Log("返回鸡：" + AfterBattlechicken.ToString());
                    AfterBattlechicken = null;
                }
                //按键监听
                BattleUI = GameObject.FindGameObjectWithTag("BattleUI");
                Button go = GameObject.Find("GO").GetComponent<Button>();
                go.onClick.AddListener(Battle);
                Button notgo = GameObject.Find("NOTGO").GetComponent<Button>();
                notgo.onClick.AddListener(Cancel);
                BattleUI.SetActive(false);
                Do = false;
            }
        }
        
        if (battle)
        {
            battle = false;
            GameSave.Instance.RemoveChicken(ChoiceChicken.GetComponent<ChiCken_State>().ThisChicken);//剔除本鸡
            Destroy(ChoiceChicken);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            BattleUI.SetActive(true);
            GameSave.Instance.SaveAllData();//存档
            SceneChange.SceneName = "XYbattle";
            SceneManager.LoadScene("LoadScene");//跳转场景
        }

        if (cancel)
        {
            cancel = false;
            BattleUI.SetActive(false);
            ChoiceChicken.transform.position = new Vector3(Random.Range(-2,2), Random.Range(-2, 2), 0);//将鸡放归于场景中
            ChoiceChicken.SetActive(true);//显示鸡
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Chicken"&& !collision.gameObject.GetComponent<GridImage>().IsDrag && collision.gameObject.GetComponent<ChiCken_State>().ThisChicken.isCock)
        {
            //提示是否要派此鸡出战
            BattleUI.SetActive(true);
            ChoiceChicken = collision.gameObject;
            BeforeBattlechicken = collision.gameObject.GetComponent<ChiCken_State>().ThisChicken;
            ChoiceChicken.SetActive(false);//隐藏鸡
        }
    }

    public void Battle()
    {
        battle = true;
    }

    public void Cancel()
    {
        cancel = true;
    }

}
