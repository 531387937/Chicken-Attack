using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class BattleGameManager : MonoBehaviour
{
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
    public Transform PlayerPos;
    public Transform EnemyPos;

    private GameObject player;
    private GameObject enemy;
    List<LevelSet> FC;

    private bool playeratk = false;
    private bool enemyatk = false;

    private float PlayerSpeed;
    private float EnemySpeed;
    //string enemyPath = "Assets/Resources/EnemyData.json";
    void Start()
    {
        level = SceneChange.Level;
        string aa = Resources.Load("EnemyData").ToString();
        FC = IOHelper.GetData(aa, typeof(List<LevelSet>), 1) as List<LevelSet>;
        enemy_chicken = FC[level - 1].EnemyChicken;
        //player_chicken.enemyChickens.Add(enemy_chicken);//将此敌人加入玩家此生遇到敌人队列
        //生成对战的两只鸡
        ChickenInit();
        //初始化血条
        //SkillRead();
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
        if (PlayerSpeed >= EnemySpeed)
        {
            PlayerReady();
        }
        else
            EnemyReady();
    }
    //生成对战的鸡
    void ChickenInit()
    {
        player = Instantiate(Chicken[(int)GameSaveNew.Instance.playerChicken.Type], PlayerPos.position, new Quaternion(0, 0, 0, 1), this.transform);
        enemy = Instantiate(Chicken[(int)enemy_chicken.Type], EnemyPos.position, new Quaternion(0, 180, 0, 1), this.transform);
    }
    void PlayerReady()
    {
        if (!gameend)
        {
            playeratk = true;
            enemyatk = false;
            int a = Random.Range(0, 10);
            if (a == 9)
                player.GetComponent<Animator>().SetTrigger("Attack1");
            else
                player.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
    void EnemyReady()
    {
        if (!gameend)
        {
            playeratk = false;
            enemyatk = true;
            int a = Random.Range(0, 10);
            if (a >= 5)
                enemy.GetComponent<Animator>().SetTrigger("Attack1");
            else
                enemy.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
    //种族读取技能
    //void SkillRead()
    //{
    //    string aa = Resources.Load("ChickenSkill").ToString();
    //    List<FightChickenSkill> skills = IOHelper.GetData(aa, typeof(List<FightChickenSkill>)) as List<FightChickenSkill>;
    //    PlayerSkillName = skills[(int)player_chicken.Type].SkillName;
    //    PlayerSkillEffect = skills[(int)player_chicken.Type].SkillEffect;
    //    EnemySkillName = skills[(int)enemy_chicken.Type].SkillName;
    //    EnemySkillEffect = skills[(int)enemy_chicken.Type].SkillEffect;
    //}
}
