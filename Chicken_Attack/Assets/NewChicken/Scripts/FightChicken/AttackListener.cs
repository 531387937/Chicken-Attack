using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackListener : MonoBehaviour
{
    public void ATK(float timer)
    {
        SendMessageUpwards("Attack", timer);
    }
}
