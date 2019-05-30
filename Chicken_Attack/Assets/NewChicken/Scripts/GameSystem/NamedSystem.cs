using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NamedSystem : MonoBehaviour
{
    private string Name;
    public TMP_InputField TMP_In;
    //变暗的遮罩
    public GameObject Mask;

    // Start is called before the first frame update
    void Start()
    {
        TMP_In.onEndEdit.AddListener(Input_End);
    }
    
    public void Input_End(string value)
    {
        Name = value;
    }

    public void Sure()
    {
        Time.timeScale = 1;
        Mask.SetActive(false);
        //GameSaveNew.Instance.PD.Chick[0].Name = Name;
        GameSaveNew.Instance.playerChicken.Retire = true;
        GameSaveNew.Instance.PD.OldChicken.Add(GameSaveNew.Instance.playerChicken);
        GameSaveNew.Instance.playerChicken = GameSaveNew.Instance.PD.Chick[0];
        GameSaveNew.Instance.playerChicken.Chick = false;
        GameSaveNew.Instance.PD.Chick.Remove(GameSaveNew.Instance.PD.Chick[0]);
        GameSaveNew.Instance.playerChicken.Name = Name;
        //保存&&刷新
        GameSaveNew.Instance.SaveAllData();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
