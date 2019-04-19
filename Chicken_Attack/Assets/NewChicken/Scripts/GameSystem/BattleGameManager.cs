using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleGameManager : MonoBehaviour
{

    public int level;
    FightChicken player_chicken;
    FightChicken enemy_chicken;
    public Slider PlayerHP;
    public Slider EnemyHP;
    public Slider PlayerSpirit;
    public Slider EnemySpirit;
    bool gameend = false;
    private string PlayerSkillName;
    private string PlayerSkillEffect;
    private string EnemySkillName;
    private string EnemySkillEffect;
    //string enemyPath = "Assets/Resources/EnemyData.json";
    void Start()
    {
        player_chicken = GameSaveNew.Instance.ChooseChicken;
        string aa = Resources.Load("EnemyData").ToString();
        List<FightChicken> FC = IOHelper.GetData(aa, typeof(List<FightChicken>),1) as List<FightChicken>;
        enemy_chicken = FC[level-1];
        SliderSet();
        //SkillRead();
        if (player_chicken.Speed >= enemy_chicken.Speed)
        {
            PlayerAttack();
        }
        else
            EnemyAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if((PlayerHP.value<=0||PlayerSpirit.value<=0)&&!gameend)
        {
            gameend = true;
            print("Player Lose!");
        }
        if ((EnemyHP.value <= 0||EnemySpirit.value<=0) && !gameend)
        {
            gameend = true;
            print("Player Win!");
        }
    }
    //玩家的回合
    void PlayerAttack()
    {
        if (!gameend)
        {
            enemy_chicken.Speed += enemy_chicken.Speed;
            EnemyHP.value -= player_chicken.Attack * Random.Range(0.95f, 1.05f);
            EnemySpirit.value -= player_chicken.Strong * Random.Range(0.95f, 1.05f);
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
            player_chicken.Speed += player_chicken.Speed;
            PlayerHP.value -= enemy_chicken.Attack * Random.Range(0.95f, 1.05f);
            PlayerSpirit.value -= enemy_chicken.Strong * Random.Range(0.95f, 1.05f);
            if(player_chicken.Speed>=enemy_chicken.Speed)
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
    void SkillRead()
    {
        string aa = Resources.Load("ChickenSkill").ToString();
        List<FightChickenSkill> skills = IOHelper.GetData(aa, typeof(List<FightChickenSkill>)) as List<FightChickenSkill>;
        PlayerSkillName = skills[(int)player_chicken.Type].SkillName;
        PlayerSkillEffect = skills[(int)player_chicken.Type].SkillEffect;
        EnemySkillName = skills[(int)enemy_chicken.Type].SkillName;
        EnemySkillEffect = skills[(int)enemy_chicken.Type].SkillEffect;
    }
}
