using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    private Chicken Player;
    private Chicken Enemy;
    public float BestTime;
    public float GoodTime;
    public float NormalTime;
    public float BadTime;
    public enum ChickenState {Best, Good,Normal,Bad,None};
    public ChickenState MyChickenState = ChickenState.None;
    private float InPutLevel = 1;//玩家操作对鸡状态增益
    //初始血量寄存
    private float PHP;
    private float EHP;
    public Text text;
    private string WIN;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Battle").GetComponent<FeedToBattle>().BeforeBattlechicken;
        //敌机的攻击暂时以玩家鸡 为基准浮动，后期可以声望为基准。
        Enemy = new Chicken();
        Enemy.HP = Player.HP + Random.Range(-5,5);
        Enemy.Speed = Player.Speed + Random.Range(-5, 5);
        Enemy.Attak = Player.Attak + Random.Range(-5, 5);
        PHP = Player.HP;
        EHP = Enemy.HP;
        GameObject.Find("Battle").GetComponent<FeedToBattle>().BeforeBattlechicken = null;//置空
        GameObject.Find("Battle").GetComponent<FeedToBattle>().AfterBattlechicken = null;//置空
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "PHP:" + PHP + "/" + "EHP:" + EHP + "/" + "本次qte评分:" + MyChickenState.ToString() + "/" + WIN;
    }

    public void RoundEnd(float ToTal_Time)
    {
        if(ToTal_Time>BadTime)
        {
            MyChickenState = ChickenState.Bad; 
        }
        if(ToTal_Time<BestTime)
        {
            MyChickenState = ChickenState.Best;
        }
        if(ToTal_Time>BestTime&&ToTal_Time<=GoodTime)
        {
            MyChickenState = ChickenState.Good;
        }
        if(ToTal_Time>GoodTime&&ToTal_Time<=BadTime)
        {
            MyChickenState = ChickenState.Normal;
        }
        AbilityClear();
        
    }

    void AbilityClear()
    {
        InPutLevel = 1;
        StartFight();
    }

    void StartFight()
    {
        //播放动画
        switch(MyChickenState)
        {
            case ChickenState.Best:
                InPutLevel *= Random.Range(1.3f, 1.45f);
                Player.Speed *= InPutLevel;
                Player.Attak *= InPutLevel;
                Judge(Player, Enemy);
                break;
            case ChickenState.Good:
                InPutLevel *= Random.Range(1.05f, 1.3f);
                Player.Speed *= InPutLevel;
                Player.Attak *= InPutLevel;
                Judge(Player, Enemy);
                break;
            case ChickenState.Normal:
                InPutLevel *= Random.Range(0.95f, 1.05f);
                Player.Speed *= InPutLevel;
                Player.Attak *= InPutLevel;
                Judge(Player, Enemy);
                break;
            case ChickenState.Bad:
                InPutLevel *= Random.Range(0.8f, 0.95f);
                Player.Speed *= InPutLevel;
                Player.Attak *= InPutLevel;
                Judge(Player, Enemy);
                break;
        }
        print(MyChickenState + ":" + InPutLevel);
    }

    void Judge(Chicken player,Chicken enemy)//xy 19.3.28
    {
        if (player.Speed > enemy.Speed)//玩家鸡先
        {
            //动画
            EHP -= player.Attak;
            if(EHP<= enemy.HP / 2)
            {
                if (EHP < 0)
                {
                    Pwin(1);//玩家重伤敌人获胜
                }
                else if (EHP > 0)
                {
                    Pwin(0);//玩家普通获胜
                }
            }
            else if(EHP > enemy.HP / 2)
            {
                //动画
                PHP -= enemy.Attak;
                if (PHP <= player.HP / 2)
                {
                    if (PHP < 0)
                    {
                        Ewin(1);//敌人重伤玩家获胜
                    }
                    else if (PHP > 0)
                    {
                        Ewin(0);//敌人普通获胜
                    }
                }
                else if (PHP > player.HP / 2)
                {
                    GameObject.Find("GameManage").GetComponent<Black_White>().StartGame();
                    return;
                }
            }
        }
        else if(player.Speed < enemy.Speed)//敌机先
        {
            //动画
            PHP -= enemy.Attak;
            if (PHP <= player.HP / 2)
            {
                if (PHP < 0)
                {
                    Ewin(1);//敌人重伤玩家获胜
                }
                else if (PHP > 0)
                {
                    Ewin(0);//敌人普通获胜
                }
            }
            else if (PHP > player.HP / 2)
            {
                //动画
                EHP -= player.Attak;
                if (EHP <= enemy.HP / 2)
                {
                    if (EHP < 0)
                    {
                        Pwin(1);//玩家重伤敌人获胜
                    }
                    else if (EHP > 0)
                    {
                        Pwin(0);//玩家普通获胜
                    }
                }
                else if (EHP > enemy.HP / 2)
                {
                    GameObject.Find("GameManage").GetComponent<Black_White>().StartGame();
                    return;
                }
            }
        }
        else if(player.Speed == enemy.Speed)//速度相同时随机先
        {
            if (Random.Range(0, 1) < 0.5f)//玩家先
            {
                //动画
                EHP -= player.Attak;
                if (EHP <= enemy.HP / 2)
                {
                    if (EHP < 0)
                    {
                        Pwin(1);//玩家重伤敌人获胜
                    }
                    else if (EHP > 0)
                    {
                        Pwin(0);//玩家普通获胜
                    }
                }
                else if (EHP > enemy.HP / 2)
                {
                    //动画
                    PHP -= enemy.Attak;
                    if (PHP <= player.HP / 2)
                    {
                        if (PHP < 0)
                        {
                            Ewin(1);//敌人重伤玩家获胜
                        }
                        else if (PHP > 0)
                        {
                            Ewin(0);//敌人普通获胜
                        }
                    }
                    else if (PHP > player.HP / 2)
                    {
                        GameObject.Find("GameManage").GetComponent<Black_White>().StartGame();
                        return;
                    }
                }
            }
            else if(Random.Range(0, 1) >= 0.5f)//敌人先
            {
                //动画
                PHP -= enemy.Attak;
                if (PHP <= player.HP / 2)
                {
                    if (PHP < 0)
                    {
                        Ewin(1);//敌人重伤玩家获胜
                    }
                    else if (PHP > 0)
                    {
                        Ewin(0);//敌人普通获胜
                    }
                }
                else if (PHP > player.HP / 2)
                {
                    //动画
                    EHP -= player.Attak;
                    if (EHP <= enemy.HP / 2)
                    {
                        if (EHP < 0)
                        {
                            Pwin(1);//玩家重伤敌人获胜
                        }
                        else if (EHP > 0)
                        {
                            Pwin(0);//玩家普通获胜
                        }
                    }
                    else if (EHP > enemy.HP / 2)
                    {
                        GameObject.Find("GameManage").GetComponent<Black_White>().StartGame();
                        return;
                    }
                }
            }
        }
    }

    void Pwin(int i)//玩家获胜
    {
        Player.Life--;
        if (i == 0)//正常获胜
        {
            //动画
            //继续
            WIN = "玩家本轮获胜！！！！！！！！！！！！！";
        }
        else if (i == 1)//重伤对方获胜
        {
            //动画
            WIN = "玩家大获全胜，重伤敌人！！！！！！！！！！！！！";
        }
        GameObject.Find("Battle").GetComponent<FeedToBattle>().AfterBattlechicken = Player;//暂时对本机无消耗
        Invoke("Return", 3f);
    }
    void Ewin(int i)//敌人获胜
    {
        if (i == 0)//正常获胜
        {
            //动画
            //继续
            WIN = "敌人本轮获胜！！！！！！！！！！！！！";
            Player.Life--;
        }
        else if (i == 1)//重伤对方获胜
        {
            //动画
            WIN = "敌人大获全胜，重伤玩家！！！！！！！！！！！！！";
            Player.Life -= 2;
        }
        GameObject.Find("Battle").GetComponent<FeedToBattle>().AfterBattlechicken = Player;//暂时对本机无消耗
        Invoke("Return", 3f);
    }

    void Return()
    {
        GameObject.Find("Battle").GetComponent<FeedToBattle>().Do = true;
        FeedToBattle.NextScene = "XYTest";
        SceneManager.LoadScene("LoadScene");//跳转场景
    }
}
