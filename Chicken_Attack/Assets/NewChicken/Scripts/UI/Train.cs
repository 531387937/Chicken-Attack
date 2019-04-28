using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train: MonoBehaviour
{
    public GameObject TrainGameObj;

    private void Start()
    {
        TrainGameObj.SetActive(false);
    }

    public void ShowTrain()
    {
        TrainGameObj.SetActive(true);
    }

    public void Exit()
    {
        TrainGameObj.SetActive(false);
    }

}
