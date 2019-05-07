using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Train: MonoBehaviour
{
    public GameObject TrainGameObj;

    public GameObject comfirm;

    public GameObject Short_Pt;
    private void Start()
    {
        TrainGameObj.SetActive(false);
    }

    public void ShowTrain()
    {
        TrainGameObj.SetActive(true);
        UIManager_NEW.Instance.CanTouch = false;
    }

    public void Exit()
    {
        TrainGameObj.SetActive(false);
        UIManager_NEW.Instance.CanTouch = true;
    }
    public void Go_Train()
    {
        if (GameSaveNew.Instance.PD.Pt >= 1)
        {
            comfirm.SetActive(true);
        }
        if (GameSaveNew.Instance.PD.Pt < 1)
        {
            Short_Pt.SetActive(true);
        }
    }
    public void Train_Class(string train)
    {
        SceneChange.SceneName = train;
    }
    public void Yes_Go()
    {
        SceneManager.LoadScene("LoadScene");
    }
    public void Back()
    {
        comfirm.SetActive(false);
    }
}
