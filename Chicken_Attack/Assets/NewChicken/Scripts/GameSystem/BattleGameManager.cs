using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class BattleGameManager : MonoBehaviour
{
    public Sprite[] sprites;
    [HideInInspector]
    public int level;
    FightChicken enemy_chicken;
    bool gameBegin = false;
    bool gameend = false;
    public TextMeshProUGUI UI_Text;

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

    public AudioSource[] sounds;
    //string enemyPath = "Assets/Resources/EnemyData.json";
    void Awake()
    {
        
        level = SceneChange.Level;

        string aa = Resources.Load("EnemyData").ToString();
       List<LevelSet> FC = IOHelper.GetData(aa, typeof(List<LevelSet>), 1) as List<LevelSet>;
        Player = new BattleAttribute(GameSaveNew.Instance.playerChicken);
        Enemy = new BattleAttribute(FC[level+1].EnemyChicken);
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
                UI_Text.text = "Star!";
            }
        }
        else
        {
            if (!gameBegin)
                GameStar();
        }
       
        if (gameend)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneChange.SceneName = "QiZiNewChicken";
                SceneManager.LoadScene("LoadScene");
            }
        }
    }
    void GameStar()
    {
        UI_Text.text = null;
        gameBegin = true;
       
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
        switch(Enemy.duel[CurrnetRound])
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
        if (PlayerCurrent_duel == BattleAttribute.Duel.scissors)
        {
            if(Enemy.duel[CurrnetRound]== BattleAttribute.Duel.scissors)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power*Random.Range(0.97f,1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.rock)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power*1.1f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power*0.9f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.paper)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
        }
        else if (PlayerCurrent_duel == BattleAttribute.Duel.rock)
        {
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.scissors)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.rock)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.paper)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 1.1f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 0.9f * Random.Range(0.97f, 1.03f));
            }
        }
        else if (PlayerCurrent_duel == BattleAttribute.Duel.paper)
        {
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.scissors)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 1.1f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 0.9f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.rock)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
            if (Enemy.duel[CurrnetRound] == BattleAttribute.Duel.paper)
            {
                CurrHurt_Enemy = Mathf.CeilToInt(Enemy.power * 0.9f * Random.Range(0.97f, 1.03f));
                CurrHurt_Player = Mathf.CeilToInt(Player.power * 1.1f * Random.Range(0.97f, 1.03f));
            }
        }
        CurrnetRound++;
        Invoke("NextRound", 1.5f);
        Invoke("ShowHurt", 0.3f);
    }

    private void NextRound()
    {
        Acts.SetActive(true);
    }

    private void ShowHurt()
    {
        Player_Hurt.SetActive(true);
        Player_Hurt.GetComponent<TextMeshPro>().text = CurrHurt_Enemy.ToString();
        TotalHurt_Enemy += CurrHurt_Enemy;
        TotalHurt_Player += CurrHurt_Player;
        Enemy_Hurt.SetActive(true);
        Enemy_Hurt.GetComponent<TextMeshPro>().text = CurrHurt_Player.ToString();
        if(CurrnetRound==5)
        {
            sounds[0].Play();
            GameResult();
        }
    }

    private void GameResult()
    {
        if(TotalHurt_Enemy<=TotalHurt_Player)
        {

        }
        if (TotalHurt_Enemy > TotalHurt_Player)
        {

        }
    }
}
