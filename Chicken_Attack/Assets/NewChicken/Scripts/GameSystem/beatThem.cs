using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beatThem : MonoBehaviour
{
    public GameObject helpPanel;

    public GameObject hide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(helpPanel.gameObject.activeSelf==true&&Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
            helpPanel.SetActive(false);
            hide.SetActive(true);
        }
    }

    public void showPanel()
    {
        helpPanel.SetActive(true);
        Time.timeScale = 0;
        hide.SetActive(false);
    }
}
