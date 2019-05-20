using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    Animator Animator;
    Vector2 began = Vector2.zero;
    Vector2 End = Vector2.zero;
    public int FingerDistance = 5;
    public float AreaScale = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches[0].phase == TouchPhase.Began)
        {
            if (Input.touches[0].position.x > Screen.width / AreaScale)
                began = Input.touches[0].position;
            Debug.Log(began);
        }
        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            if (Input.touches[0].position.x > Screen.width / AreaScale)
                End = Input.touches[0].position;
        }
        if(began != Vector2.zero && End != Vector2.zero)
        {
            if (began.x > End.x + FingerDistance)//左滑
            {
                Debug.Log("左滑");
                Animator.SetBool("IsShow", true);
            }
            else if (began.x + FingerDistance < End.x)//右滑
            {
                Debug.Log("右滑");
                Animator.SetBool("IsShow", false);
            }
            began = Vector2.zero;
            End = Vector2.zero;
        }
    }
}
