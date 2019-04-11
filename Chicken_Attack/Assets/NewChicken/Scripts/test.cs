using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    FightChicken FC;
    // Start is called before the first frame update
    void Start()
    {
        FC = new FightChicken("Qizi");
        FC.setCock();
        print(FC.getName());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
