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
    public Duel[] rebuildDuel;
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
        int[] actions = new int[5];
        for(int i=0;i<5;i++)
        {
            actions[i] = action[i];
        }
        rebuildDuel = new Duel[5];
        ReBuild(actions);
        for (int i = 0; i < 5; i++)
        {
            rebuildDuel[i] = (Duel)actions[i];
        }
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

    void ReBuild(int[] array)
    {
        for (int j = 0; j < array.Length - 1; j++)//遍历次数长度-1
        {
            int index = j;
            for (int i = j + 1; i < array.Length; i++)
            {
                if (array[i] < array[index])//找到最小的
                {
                    index = i;
                }
            }
            //交换位置
            int temp = array[index];
            array[index] = array[j];
            array[j] = temp;
        }
    }
}
