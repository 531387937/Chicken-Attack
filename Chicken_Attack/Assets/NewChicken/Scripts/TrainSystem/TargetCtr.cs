using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCtr : MonoBehaviour
{

    public GameObject target;
    public float Speed;
    public float minX;
    public float maxX;

    private bool left=false;
    public bool targetIn=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(!left)
        {
            target.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
        }
     if(left)
        {
            target.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
        }
     if(target.transform.position.x>maxX)
        {
            left = true;
        }
    else if(target.transform.position.x<minX)
        {
            left = false;
        }
        print(left);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Target")
        targetIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
            targetIn = false;
    }
}
