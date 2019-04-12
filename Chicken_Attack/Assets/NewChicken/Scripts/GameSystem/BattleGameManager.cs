using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleGameManager : MonoBehaviour
{

    public int level;
    FightChicken player_chicken;
    FightChicken enemy_chicken;
    List<FightChicken> FC = new List<FightChicken>();
    public Slider PlayerHP;
    public Slider EnemyHP;
    bool gameend = false;
    string enemyPath = "Assets/Resources/EnemyData.json";
    void Start()
    {
        player_chicken = GameSaveNew.playerChicken;
        FC = IOHelper.GetData(enemyPath, typeof(List<FightChicken>)) as List<FightChicken>;
        enemy_chicken = FC[level-1];
        PlayerHP.maxValue = player_chicken.HP;
        PlayerHP.value = PlayerHP.maxValue;
        EnemyHP.maxValue = enemy_chicken.HP;
        EnemyHP.value = EnemyHP.maxValue;
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
        if(PlayerHP.value<=0&&!gameend)
        {
            gameend = true;
            print("Player Lose!");
        }
        if (EnemyHP.value <= 0 && !gameend)
        {
            gameend = true;
            print("Player Win!");
        }
    }
    void PlayerAttack()
    {
        if (!gameend)
        {
            EnemyHP.value -= player_chicken.Attack * Random.Range(0.95f, 1.05f);
            Invoke("EnemyAttack", 2);
        }
    }
    void EnemyAttack()
    {
        if (!gameend)
        {
            PlayerHP.value -= enemy_chicken.Attack * Random.Range(0.95f, 1.05f);
            Invoke("PlayerAttack", 2);
        }
    }
}
