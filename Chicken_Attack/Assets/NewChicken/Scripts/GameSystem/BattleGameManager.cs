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
    List<LevelSet> FC;
    //string enemyPath = "Assets/Resources/EnemyData.json";
    void Start()
    {
        level = SceneChange.Level;
        player_chicken = GameSaveNew.Instance.ChooseChicken;
        print(player_chicken.Name);
        string aa = Resources.Load("EnemyData").ToString();
        FC = IOHelper.GetData(aa, typeof(List<LevelSet>),1) as List<LevelSet>;
        enemy_chicken = FC[level-1].EnemyChicken;
        player_chicken.enemyChickens.Add(enemy_chicken);//将此敌人加入玩家此生遇到敌人队列
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
            GameSaveNew.Instance.SaveAllData();
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
        if (player_chicken.Speed >= enemy_chicken.Speed)
        {
            PlayerAttack();
        }
        else
            EnemyAttack();
    }
    //玩家的回合
    void PlayerAttack()
    {
        if (!gameend)
        {
            //重置两伤害的初始化
            HP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            HP_Damage.gameObject.transform.position = EnemyHPPos.position;
            MP_Damage.gameObject.transform.position = EnemyMPPos.position;
            MP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //进行伤害判定
            float damage =(int)player_chicken.Attack * Random.Range(0.95f, 1.05f);
            HP_Damage.text = "-"+(int)damage;
            HP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(5.0f, 8.0f), 10.0f));
            EnemyHP.value -=(int)damage;
            damage= (int)player_chicken.Strong * Random.Range(0.95f, 1.05f);
            MP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(5.0f, 8.0f), 10.0f));
            MP_Damage.text = "-" + (int)damage;
            EnemySpirit.value -= (int)damage;
            //进行速度的判定
            enemy_chicken.Speed += enemy_chicken.Speed;
            if(enemy_chicken.Speed>=player_chicken.Speed)
            Invoke("EnemyAttack", 2);
            else
            Invoke("PlayerAttack", 2);
        }
    }
    //敌人的回合
    void EnemyAttack()
    {
        if (!gameend)
        {
            //重置两伤害的初始化
            HP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            MP_Damage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            HP_Damage.gameObject.transform.position = playerHPPos.position;
            MP_Damage.gameObject.transform.position = playerMPPos.position;
            //进行伤害判定
            float damage = (int)enemy_chicken.Attack * Random.Range(0.95f, 1.05f);
            HP_Damage.text = "-" + (int)damage;
            HP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5.0f, -8.0f), 10.0f));
            PlayerHP.value -= (int)damage;
            damage = (int)enemy_chicken.Strong * Random.Range(0.95f, 1.05f);
            MP_Damage.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5.0f, -.0f), 10.0f));
            MP_Damage.text = "-" + (int)damage;
            PlayerSpirit.value -= (int)damage;
            //进行速度的判定
            player_chicken.Speed += player_chicken.Speed;
            if (player_chicken.Speed>=enemy_chicken.Speed)
            Invoke("PlayerAttack", 2);
            else
            Invoke("EnemyAttack", 2);
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
