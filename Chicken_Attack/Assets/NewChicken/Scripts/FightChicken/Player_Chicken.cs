using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class Player_Chicken : MonoBehaviour
{
    public GameObject[] ga; 
    // Start is called before the first frame update
    void Start()
    {
        //按照鸡的种类生成鸡
        Instantiate(ga[(int)GameSaveNew.playerChicken.Type],new Vector3(0,0,0),new Quaternion(0,0,0,1));
    }
    
}
