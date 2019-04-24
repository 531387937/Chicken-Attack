using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSet
{
    public FightChicken EnemyChicken;
    public int GoldGet;
    public int PrestigeGet;
    public int PtGet;
    public LevelSet()
    {
        EnemyChicken = new FightChicken("aa");
        GoldGet = 10;
        PrestigeGet = 10;
        PtGet = 1;
    }
}
