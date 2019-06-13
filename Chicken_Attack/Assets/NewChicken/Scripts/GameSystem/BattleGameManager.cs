using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class BattleGameManager : MonoBehaviour
{
    public Sprite[] sprites;
    [HideInInspector]
    public int level;
    FightChicken enemy_chicken;
    bool gameBegin = false;
    bool gameend = false;
    public TextMeshProUGUI UI_Text;
    public TextMeshProUGUI End_Text;

    public TextMeshProUGUI Gold;
    public TextMeshProUGUI Prestige;
    public TextMeshProUGUI Pt;
    public GameObject EndGamePanel;
    private float timer = 3;

    public GameObject[] Chicken;
    [HideInInspector]
    public Transform PlayerPos;
    [HideInInspector]
    public Transform EnemyPos;

    [HideInInspector]
    public GameObject player_Chicken;
    private GameObject enemy;

    private bool playeratk = false;
    private bool enemyatk = false;

    private float PlayerSpeed;
    private float EnemySpeed;

    private BattleAttribute Player;
    private BattleAttribute Enemy;
    [HideInInspector]
    public GameObject Acts;
    [HideInInspector]
    public GameObject BtnGroup;
    [HideInInspector]
    public GameObject EnemyGroup;

    public BattleAttribute.Duel PlayerCurrent_duel;
    private int CurrnetRound = 0;

    public GameObject Player_Hurt;
    public GameObject Enemy_Hurt;

    private int CurrHurt_Player;
    private int CurrHurt_Enemy;
    private int TotalHurt_Player;
    private int TotalHurt_Enemy;

    public Transform HurtPos_Player;
    public Transform HurtPos_Enemy;
    public AudioSource[] sounds;


    public SpriteRenderer Player_Image;
    public SpriteRenderer Enemy_Image;

    public PlayableDirector win;
    public PlayableDirector lose;
    public PlayableDirector peace;

    [SerializeField]
    private Slider playerSlider;
    [SerializeField]
    private Slider enemySlider;
    //关卡信息
    List<LevelSet> FC;
    //string enemyPath = "Assets/Resources/EnemyData.json";
    void Awake()
    {
        playerSlider.value = 0.5f;
        enemySlider.value = 0.5f;
        level = SceneChange.Level;

        string aa = Resources.Load("EnemyData").ToString();
        FC = IOHelper.GetData(aa, typeof(List<LevelSet>), 1) as List<LevelSet>;
        Player = new BattleAttribute(GameSaveNew.Instance.playerChicken);
        Enemy = new BattleAttribute(FC[level].EnemyChicken);
        enemy_chicken = FC[level].EnemyChicken;
        //player_chicken.enemyChickens.Add(enemy_chicken);//将此敌人加入玩家此生遇到敌人队列
        //生成对战的两只鸡
        ChickenInit();
        //初始化血条
        //SkillRead();
        for (int i = 0; i < 5; i++)
        {
            BtnGroup.transform.GetChild(i).GetComponent<GameBtn>().duel = Player.duel[i];
            BtnGroup.transform.GetChild(i).GetComponent<Image>().sprite = sprites[(int)Player.duel[i]];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= -0.5)
        {
            timer -= Time.deltaTime;
            if (timer >= 2)
            {
                UI_Text.text = "3";
            }
            else if (timer >= 1)
            {
                UI_Text.text = "2";
            }
            else if (timer >= 0)
            {
                UI_Text.text = "1";
            }
            else
            {
                UI_Text.text = "Start!";
            }
        }
        else
        {
            if (!gameBegin)
                GameStart();
        } 
       
        if (gameend)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                SceneChange.SceneName = "QiZiNewChicken";
                SceneManager.LoadScene("LoadScene");
            }
        }

        if(TotalHurt_Player!=0&&TotalHurt_Enemy!=0)
        {
            playerSlider.value = (float)TotalHurt_Player /(float)(TotalHurt_Player + TotalHurt_Enemy);
            enemySlider.value =(float)TotalHurt_Enemy / (float)(TotalHurt_Enemy + TotalHurt_Player);
        }
    }
    void GameStart()
    {
        UI_Text.text = null;
        gameBegin = true;
        StartCoroutine(NextRound());

    }
    //生成对战的鸡
    void ChickenInit()
    {
        player_Chicken = Instantiate(Chicken[(int)GameSaveNew.Instance.playerChicken.Type], PlayerPos.position, new Quaternion(0, 0, 0, 1), this.transform);
        enemy = Instantiate(Chicken[(int)enemy_chicken.Type], EnemyPos.position, new Quaternion(0, 180, 0, 1), this.transform);
    }
    //进行回合的战斗
    public void Round()
    {
        EnemyGroup.transform.GetChild(CurrnetRound).gameObject.SetActive(false);
        Acts.SetActive(false);
        Player_Image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Enemy_Image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Player_Image.gameObject.transform.position = new Vector3(-6.75f, 2.18f, 0);
        Enemy_Image.gameObject.transform.position = new Vector3(6.75f, 2.18f, 0);
        Player_Image.gameObject.SetActive(true);
        Enemy_Image.gameObject.SetActive(true);
        Player_Image.sprite = sprites[(int)PlayerCurrent_duel];
        Enemy_Image.sprite = sprites[(int)Enemy.duel[CurrnetRound]];

       
        if (PlayerCurrent_duel == BattleAttribute.Duel.scissors)
        {
            if(Enemy.duel[CurrnetRound]== BattleAttribute.Duel.scissors)
            {
                peace.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power*Random.Range(0.97f,1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.rock)
            {
                lose.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power*1.1f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power*0.9f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.paper)
            {
                win.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
        }
        else if (PlayerCurrent_duel == BattleAttribute.Duel.rock)
        {
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.scissors)
            {
                win.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.rock)
            {
                peace.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.paper)
            {
                lose.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 1.1f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 0.9f * Random.Range(0.97f, 1.03f));
            }
        }
        else if (PlayerCurrent_duel == BattleAttribute.Duel.paper)
        {
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.scissors)
            {
                lose.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 1.1f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 0.9f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.rock)
            {
                win.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.paper)
            {
                peace.Play();
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
        }
        StartCoroutine(NextRound());
        Invoke("ShowHurt", 2.3f);
    }

    IEnumerator NextRound()
    {
        yield return new WaitForSeconds(3.5f);
        if (CurrnetRound == 5)
        {
            GameResult();
        }
        else
        {
            UI_Text.gameObject.SetActive(true);
            UI_Text.fontSize = 85;
            UI_Text.text = "Round " + (CurrnetRound + 1);
            yield return new WaitForSeconds(1f);
            Acts.SetActive(true);
            UI_Text.gameObject.SetActive(false);
        }
    }

    private void ShowHurt()
    {
        switch (PlayerCurrent_duel)
        {
            case BattleAttribute.Duel.scissors:
                player_Chicken.GetComponent<Animator>().SetTrigger("Attack2");
                break;
            case BattleAttribute.Duel.paper:
                player_Chicken.GetComponent<Animator>().SetTrigger("Attack1");
                break;
            case BattleAttribute.Duel.rock:
                player_Chicken.GetComponent<Animator>().SetTrigger("Attack");
                break;
        }
        switch (Enemy.duel[CurrnetRound])
        {
            case BattleAttribute.Duel.scissors:
                enemy.GetComponent<Animator>().SetTrigger("Attack2");
                break;
            case BattleAttribute.Duel.paper:
                enemy.GetComponent<Animator>().SetTrigger("Attack1");
                break;
            case BattleAttribute.Duel.rock:
                enemy.GetComponent<Animator>().SetTrigger("Attack");
                break;
        }
        CurrnetRound++;
        Player_GetHurt();
        Enemy_GetHurt();
    }

    private void GameResult()
    {
        if(TotalHurt_Enemy<=TotalHurt_Player)
        {
            StartCoroutine(PlayerWin());
        }
        if (TotalHurt_Enemy > TotalHurt_Player)
        {
            StartCoroutine(PlayerLose());
        }
    }

    public void Player_GetHurt()
    {
        Player_Hurt.transform.position = HurtPos_Player.position;
        Player_Hurt.SetActive(true);
        Player_Hurt.GetComponent<TextMeshPro>().text = CurrHurt_Enemy.ToString();
        Player_Hurt.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player_Hurt.GetComponent<Rigidbody>().AddForce(new Vector3(-105, 60, 0));
        TotalHurt_Enemy += CurrHurt_Enemy;
    }

    public void Enemy_GetHurt()
    {
       Enemy_Hurt.transform.position = HurtPos_Enemy.position;
        Enemy_Hurt.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Enemy_Hurt.GetComponent<Rigidbody>().AddForce(new Vector3(105, 60, 0));
        TotalHurt_Player += CurrHurt_Player;
        Enemy_Hurt.SetActive(true);
        Enemy_Hurt.GetComponent<TextMeshPro>().text = CurrHurt_Player.ToString();
    }

    IEnumerator PlayerWin()
    {
        GameSaveNew.Instance.playerChicken.enemyChickens.Add(Enemy.power);
        enemy.GetComponent<Animator>().SetTrigger("Defeat");
        yield return new WaitForSeconds(1.3f);
        sounds[0].Play();
        yield return new WaitForSeconds(1.5f);
        Gold.text ="+"+ FC[level].GoldGet.ToString();
        if (GameSaveNew.Instance.PD.NowLevel <= level)
        {
            Prestige.text = "+" + FC[level].PrestigeGet.ToString();
            GameSaveNew.Instance.PD.NowLevel++;
        }
        else
        {
            Prestige.text = "+0";
        }
        Pt.text = "+" + FC[level].PtGet.ToString();
        EndGamePanel.SetActive(true);
        gameend = true;
        GameSaveNew.Instance.PD.Gold += FC[level].GoldGet;
        GameSaveNew.Instance.PD.Prestige += FC[level].PrestigeGet;
        GameSaveNew.Instance.PD.Pt += FC[level].PtGet;
        End_Text.gameObject.SetActive(true);
        End_Text.text = "你的鸡取得了胜利！";
    }

    IEnumerator PlayerLose()
    {
        player_Chicken.GetComponent<Animator>().SetTrigger("Defeat");
        yield return new WaitForSeconds(1.3f);
        sounds[0].Play();
        yield return new WaitForSeconds(1.5f);
        EndGamePanel.SetActive(true);
        gameend = true;
        End_Text.gameObject.SetActive(true);
        End_Text.text = "你的鸡输掉了比赛！";
    }

}
