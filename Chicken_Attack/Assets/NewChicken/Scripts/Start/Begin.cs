using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : MonoBehaviour
{
    public void ReSetData()
    {
        GameSaveNew.Instance.DestoryALLData();
    }
}
