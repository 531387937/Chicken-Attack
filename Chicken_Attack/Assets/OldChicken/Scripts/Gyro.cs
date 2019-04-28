
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//陀螺仪控制图片移动
public class Gyro : MonoBehaviour
{
    public Mode mode;
    public float Speed = 2f;
    public float limitPosX = 5;
    private float startPosX;
    private Vector3 currentPos;

    public enum Mode
    {
        CamMode,
        PlaneMode
    }

    protected void Start()
    {
        startPosX = transform.position.x;
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.1f;
    }

    protected void Update()
    {
        switch (mode)
        {
           case Mode.CamMode:
                if (Mathf.Abs(startPosX) + Mathf.Abs(currentPos.x) > limitPosX)
                {
                    if (Input.gyro.attitude.ToEuler().x < 0)
                    {
                        transform.position = currentPos + new Vector3(0.5f, 0, 0);
                    }
                    if (Input.gyro.attitude.ToEuler().x > 0)
                    {
                        transform.position = currentPos - new Vector3(0.5f, 0, 0);
                    }
                    currentPos = transform.position;
                    return;
                }
                if (Mathf.Abs(Input.gyro.attitude.ToEuler().x) > 0.35)
                {
                    this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.transform.localPosition
                        + new Vector3(Input.gyro.attitude.ToEuler().x, 0, 0), Speed * Time.deltaTime);
                }
               break;
           case Mode.PlaneMode:
                if (Mathf.Abs(startPosX) + Mathf.Abs(currentPos.x) > limitPosX)
                {
                    if (Input.gyro.attitude.ToEuler().x < 0)
                    {
                        transform.position = currentPos - new Vector3(0.5f, 0, 0);
                    }
                    if (Input.gyro.attitude.ToEuler().x > 0)
                    {
                        transform.position = currentPos + new Vector3(0.5f, 0, 0);
                    }
                    currentPos = transform.position;
                    return;
                }
                if (Mathf.Abs(Input.gyro.attitude.ToEuler().x) > 0.35)
                {
                    this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.transform.localPosition
                        - new Vector3(Input.gyro.attitude.ToEuler().x, 0, 0), Speed * Time.deltaTime);
                }
               break;
        }
        currentPos = this.transform.position;
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(50, 100, 500, 20), "Label : " + Input.gyro.attitude.ToEuler().x);
    //}
    
}

