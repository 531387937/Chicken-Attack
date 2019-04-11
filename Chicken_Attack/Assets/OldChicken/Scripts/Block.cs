using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool CanTouch;
    public Black_White BW;
    public int num;
    private int xx;
    // Start is called before the first frame update
    void Start()
    {
        BW = GameObject.FindObjectOfType<Black_White>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        BW.SelectBlock(this);
    }
    public void SetPosition(int x,int y)
    {
        xx = x;
        num = y;
        switch(x)
        {
            case 0:
                transform.position = new Vector3(-7, transform.position.y, 0);
                break;
            case 1:
                transform.position = new Vector3(7, transform.position.y, 0);
                break;
        }
        switch(y)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, -3, 0);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x, -1, 0);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, 1, 0);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, 3, 0);
                break;
        }
    }
    public void MoveDown()
    {
        num--;
        SetPosition(xx, num);
        //transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(transform.position.x, transform.position.y - 2, 0), 20f*Time.deltaTime);
        //--num;
    }
}
