using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeCtr : MonoBehaviour
{
    public Sprite[] Sp;
    public Vector3[] HomeScale;

    public GameObject child;
    public Sprite[] ChildSp;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sp[GameSaveNew.Instance.PD.HomeLevel];
        gameObject.transform.localScale = HomeScale[GameSaveNew.Instance.PD.HomeLevel];
        child.GetComponent<SpriteRenderer>().sprite = ChildSp[GameSaveNew.Instance.PD.HomeLevel];
    }
}
