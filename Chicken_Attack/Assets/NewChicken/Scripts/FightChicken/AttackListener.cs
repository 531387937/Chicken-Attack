using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackListener : MonoBehaviour
{
    public void ATK(float timer)
    {
        if (SceneManager.GetActiveScene().name == "BattleNew")
        {
            SendMessageUpwards("Attack", timer);
        }
    }
}
