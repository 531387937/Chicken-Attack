using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedToShop : MonoBehaviour
{
    private static bool isNoDestroyHandler = true;//是否没有DontDestroyOnLoad处理

    // Start is called before the first frame update
    void Start()
    {
        if (isNoDestroyHandler)
        {
            isNoDestroyHandler = false;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (!isNoDestroyHandler)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToShop()
    {
        //GameSave.Instance.PD
    }
}
