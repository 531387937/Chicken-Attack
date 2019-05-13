using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Train : MonoBehaviour
{
    //成功跳跃的数量
    public int SucceedNum = 0;
    //使用的贴图
    public Vector3 tempPos;
    public GameObject[] Chicken;
    private GameObject Train_Chicken;
    public GameObject m_hurdle;
    //栅栏的数量
    public int HurdleNum;

    public float JumpSpeed;

    private float MaxSpeed;
    private bool Falling = false;
    private bool Jumping=false;
    // Start is called before the first frame update
    void Start()
    {
        MaxSpeed = JumpSpeed;
        Train_Chicken = Instantiate(Chicken[(int)(GameSaveNew.Instance.playerChicken.Type)], Vector3.zero, Quaternion.identity);
        for (int i=0;i<HurdleNum;i++)
        {
            Instantiate(m_hurdle,tempPos,new Quaternion(0,0,0,1));
            tempPos.x += Random.Range(8f, 13f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(JumpSpeed<=0&&!Falling)
        {
            Down();
        }
        if(Train_Chicken.transform.position.y <= 0.1f&&Falling)
        {
            Train_Chicken.transform.position = new Vector3(Train_Chicken.transform.position.x, 0, Train_Chicken.transform.position.z);
            Land();
        }
        if(Input.GetMouseButtonDown(0)&&!Jumping&&!Falling)
        {
            Jumping = true;
        }
        if(Jumping&&!Falling)
        {
            Jump();
        }
        if(Falling&&Jumping)
        {
            Train_Chicken.transform.position = new Vector3(Train_Chicken.transform.position.x, Train_Chicken.transform.position.y - JumpSpeed * Time.deltaTime, Train_Chicken.transform.position.z);
            JumpSpeed += 14f * Time.deltaTime;
        }
    }
    void Jump()
    {
        Train_Chicken.transform.position = new Vector3(Train_Chicken.transform.position.x, Train_Chicken.transform.position.y + JumpSpeed * Time.deltaTime, Train_Chicken.transform.position.z);
        JumpSpeed -= 14f* Time.deltaTime;
    }
    void Down()
    {
        Falling = true;
    }
    void Land()
    {
        JumpSpeed = MaxSpeed;
        Jumping = false;
        Falling = false;
    }
    public void Train_End()
    {

    }
}
