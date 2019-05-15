using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NamedSystem : MonoBehaviour
{
    private string Name;

    public TMP_InputField TMP_In;
    // Start is called before the first frame update
    void Start()
    {
        TMP_In.onEndEdit.AddListener(Input_End);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Input_End(string value)
    {
        Name = value;
    }
    public void Sure()
    {
        GameSaveNew.Instance.PD.Chick[0].Name = Name;
    }
}
