using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class BattleGameManager : MonoBehaviour
{
    public int level;
    FightChicken player_chicken;
    FightChicken enemy_chicken;
    public Slider PlayerHP;
    public Slider EnemyHP;
    public Slider PlayerSpirit;
    public Slider EnemySpirit;
    public TextMeshPro HP_Damage;
    public TextMeshPro MP_Damage;
    bool gameBegin = false;
    bool gameend = false;
    private string PlayerSkillName;
    private string PlayerSkillEffect;
    private string EnemySkillName;
    private string EnemySkillEffect;

    public Transform playerHPPos;
    public Transform playerMPPos;

    public Transform EnemyHPPos;
    public Transform EnemyMPPos;

    public TextMeshProUGUI UI_Text;

    public TextMeshProUGUI Gold;
    public GameObject EndGamePanel;
    private float timer = 3;

    public GameObject[] Chicken;
    public Transform PlayerPos;
    public Transform EnemyPos;

    private GameObject player;
    private GameObject enemy;
    List<LevelSet> FC;

    private bool playeratk=false;
    private bool enemyatk=false;

    private float PlayerSpeed;
    private float EnemySpeed;
    //string enemyPath = "Assets/Resources/EnemyData.json";
    void Start()
    {
        level = SceneChange.Level;
        player_chicken = GameSaveNew.Instance.ChooseChicken;
        PlayerSpeed = player_chicken.Speed;
        string aa = Resources.Load("EnemyData").ToString();
        FC = IOHelper.GetData(aa, typeof(List<LevelSet>),1) as List<LevelSet>;
        enemy_chicken = FC[level-1].EnemyChicken;
        EnemySpeed = enemy_chicken.Speed;
        //player_chicken.enemyChickens.Add(enemy_chicken);//将此敌人加入玩家此生遇到敌人队列
        //生成对战的两只鸡
        ChickenInit();
        //初始化血条
        SliderSet();
        //SkillRead();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>=-0.5)
        {
            timer -= Time.deltaTime;
            if(timer>=2)
            {
                UI_Text.text = "3";
            }
            else if(timer>=1)
            {
                UI_Text.text = "2";
            }
            else if(timer>=0)
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
            if(!gameBegin)
            GameStar();
        }
        if ((PlayerHP.value<=0||PlayerSpirit.value<=0)&&!gameend)
        {
            gameend = true;
            UI_Text.text = "You Lose!";
            EndGamePanel.SetActive(true);
            Time.timeScale = 1;
        }
        if ((EnemyHP.value <= 0||EnemySpirit.value<=0) && !gameend)
        {
            gameend = true;
            UI_Text.text = "You Win!";
            EndGamePanel.SetActive(true);
            Gold.text = "+" + FC[level - 1].GoldGet;
            GameSaveNew.Instance.PD.Gold+= FC[level - 1].GoldGet;
            GameSaveNew.Instance.PD.Prestige += FC[level - 1].PrestigeGet;
            GameSaveNew.Instance.PD.Pt += FC[level - 1].PtGet;
            GameSaveNew.Instance.SavePlayerData();
            Time.timeScale = 1;
        }
        if(gameend)
        {
            if(Input.GetMouseButtonDown(0))
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
    public void Attack(float timer)
    {
        if(playeratk)
        {
            PlayerAttack(timer);
        }
        if(enemyatk)
        {
            EnemyAttack(timer);
        }
    }
    //玩家的回合
    public void PlayerAttack(float time=1)
    {
        if (!gameend)
        {
            //重置两伤害的初始化
            HP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            HP_Damage.gameObject.transform.position = EnemyHPPos.position;
            MP_Damage.gameObject.transform.position = EnemyMPPos.position;
            MP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //进行伤害判定
            float damage =(int)player_chicken.Attack * Random.Range(0.95f, 1.05f)*time;
            HP_Damage.text = "-"+(int)damage;
            HP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(5.0f, 8.0f), 10.0f));
            EnemyHP.value -=(int)damage;
            damage= (int)player_chicken.Strong * Random.Range(0.95f, 1.05f)*time;
            MP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(5.0f, 8.0f), 10.0f));
            MP_Damage.text = "-" + (int)damage;
            EnemySpirit.value -= (int)damage;
            //进行速度的判定
            EnemySpeed += enemy_chicken.Speed;
            if(EnemySpeed>=PlayerSpeed)
            Invoke("EnemyReady", 2);
            else
            Invoke("PlayerReady", 2);
        }
    }
    //敌人的回合
    public void EnemyAttack(float time=1)
    {
        if (!gameend)
        {
            //重置两伤害的初始化
            HP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            MP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            HP_Damage.gameObject.transform.position = playerHPPos.position;
            MP_Damage.gameObject.transform.position = playerMPPos.position;
            //进行伤害判定
            float damage = (int)enemy_chicken.Attack * Random.Range(0.95f, 1.05f)*time;
            HP_Damage.text = "-" + (int)damage;
            HP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5.0f, -8.0f), 10.0f));
            PlayerHP.value -= (int)damage;
            damage = (int)enemy_chicken.Strong * Random.Range(0.95f, 1.05f)*time;
            MP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5.0f, -.0f), 10.0f));
            MP_Damage.text = "-" + (int)damage;
            PlayerSpirit.value -= (int)damage;
            //进行速度的判定
            PlayerSpeed += player_chicken.Speed;
            if (PlayerSpeed>=EnemySpeed)
            Invoke("PlayerReady", 2);
            else
            Invoke("EnemyReady", 2);
        }
    }
     //设置生命值等血条
    void SliderSet()
    {
        PlayerHP.maxValue = player_chicken.HP;
        PlayerHP.value = PlayerHP.maxValue;
        EnemyHP.maxValue = enemy_chicken.HP;
        EnemyHP.value = EnemyHP.maxValue;
        PlayerSpirit.maxValue = player_chicken.Spirit;
        PlayerSpirit.value = PlayerSpirit.maxValue;
        EnemySpirit.maxValue = enemy_chicken.Spirit;
        EnemySpirit.value = EnemySpirit.maxValue;
    }
    //生成对战的鸡
    void ChickenInit()
    {
        player = Instantiate(Chicken[(int)player_chicken.Type], PlayerPos.position,new Quaternion(0,0,0,1),this.transform);
        enemy = Instantiate(Chicken[(int)enemy_chicken.Type], EnemyPos.position, new Quaternion(0, 180, 0, 1),this.transform);
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
            if (a == 9)
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
