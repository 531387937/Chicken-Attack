using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static string SceneName;
    public static int Level;

    public void GoLoadingScene(string name)
    {
        //if (name == "ChickenBreedScene"||name == "XYShop" && GameSaveNew.Instance.PD.ShopChicken != null)
        //{
        //    return;
        //}
        SceneName = name;
        SceneManager.LoadScene("LoadScene");
    }
    public void ChooseLevel(int level)
    {
        Level = level;
    }
}
