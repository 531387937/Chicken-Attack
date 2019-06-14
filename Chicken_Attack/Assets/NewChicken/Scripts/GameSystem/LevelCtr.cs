using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelCtr : MonoBehaviour
{
    private Button[] levels;

    List<LevelSet> FC;
    private void Awake()
    {
        string aa = Resources.Load("EnemyData").ToString();
        FC = IOHelper.GetData(aa, typeof(List<LevelSet>), 1) as List<LevelSet>;

    }
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
                levels[i].GetComponentInChildren<Text>().text =(i + 1).ToString();
                levels[i].GetComponentInChildren<TextMeshProUGUI>().text = (FC[i].EnemyChicken.Power - FC[i].EnemyChicken.Power % 10 + 20).ToString();
            }
            else
            {
                levels[i].interactable = false;
                levels[i].GetComponentInChildren<Text>().enabled = false;
                levels[i].GetComponentInChildren<TextMeshProUGUI>().text = "???";
            }
        }
    }
}
