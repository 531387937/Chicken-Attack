using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public float BestTime;
    public float GoodTime;
    public float NormalTime;
    public float BadTime;
    public enum ChickenState {Best, Good,Normal,Bad,None};
    public ChickenState MyChickenState = ChickenState.None;
    float ChickenA = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        ChickenA = 10;
StartFight();
    }
    void StartFight()
    {
        switch(MyChickenState)
        {
            case ChickenState.Best:
                ChickenA *= Random.Range(1.3f, 1.45f);
                break;
            case ChickenState.Good:
                ChickenA *= Random.Range(1.05f, 1.3f);
                break;
            case ChickenState.Normal:
                ChickenA *= Random.Range(0.95f, 1.05f);
                break;
            case ChickenState.Bad:
                ChickenA *= Random.Range(0.8f, 0.95f);
                break;
        }
        print(MyChickenState + ":" + ChickenA);
    }
}
