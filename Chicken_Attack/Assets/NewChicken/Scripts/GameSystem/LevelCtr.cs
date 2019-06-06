using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelCtr : MonoBehaviour
{
    private Button[] levels;
    // Start is called before the first frame update
    void Start()
    {
        levels = GetComponentsInChildren<Button>();
        for(int i=0;i<levels.Length;i++)
        {
            if(i<=GameSaveNew.Instance.PD.NowLevel-1)
            {
                levels[i].interactable = true;
                levels[i].GetComponentInChildren<Text>().enabled = true;
            }
            else
            {
                levels[i].interactable = false;
                levels[i].GetComponentInChildren<Text>().enabled = false;
            }
        }
    }
}
