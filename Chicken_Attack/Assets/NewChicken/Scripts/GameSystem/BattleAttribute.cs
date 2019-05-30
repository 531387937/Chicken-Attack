using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAttribute
{
    public enum Duel
    {
        scissors,//剪刀
        rock,//石头
        paper//布
    }
    public Duel[] duel;
    public int[] action;
    public float power;

    public BattleAttribute(FightChicken FC)
    {
        power = FC.Power;
        action = new int[] { 0, 0, 1, 1, 2, 2 };
        Shuffle(action);
        duel = new Duel[5];
        for(int i=0;i<5;i++)
        {
            duel[i] =(Duel)action[i];
        }
        Debug.Log(duel[1].ToString());
    }


    void Shuffle(int[] intArray)
    {
        for (int i = 0; i < intArray.Length; i++)
        {
            int temp = intArray[i];
            int randomIndex = Random.Range(0, intArray.Length);
            intArray[i] = intArray[randomIndex];
            intArray[randomIndex] = temp;
        }
    }
}
