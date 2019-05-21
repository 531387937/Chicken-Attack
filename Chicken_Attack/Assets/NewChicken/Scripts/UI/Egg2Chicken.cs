using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg2Chicken : MonoBehaviour
{
    public GameObject a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToChi()
    {
        a.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
