using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFightChicken : MonoBehaviour
{
    public FightChicken self;
    public int selfNum;
    private void Start()
    {
        StartCoroutine(SavePos());
    }
    private void Update()
    {
        selfNum = self.Ch_Num;
    }
    IEnumerator SavePos()
    {
        self.Pos = this.transform.position;
        yield return new WaitForSeconds(5);
        StartCoroutine(SavePos());
    }
}
