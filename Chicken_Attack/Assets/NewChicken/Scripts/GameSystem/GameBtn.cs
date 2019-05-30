using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBtn : MonoBehaviour
{
    public BattleAttribute.Duel duel;
    public BattleGameManager manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowAct()
    {
        this.gameObject.SetActive(false);
        switch(duel)
        {
            case BattleAttribute.Duel.paper:
                manager.PlayerCurrent_duel = BattleAttribute.Duel.paper;
                break;
            case BattleAttribute.Duel.rock:
                manager.PlayerCurrent_duel = BattleAttribute.Duel.rock;
                break;
            case BattleAttribute.Duel.scissors:
                manager.PlayerCurrent_duel = BattleAttribute.Duel.scissors;
                break;
        }
        manager.Round(); 
    }
}
