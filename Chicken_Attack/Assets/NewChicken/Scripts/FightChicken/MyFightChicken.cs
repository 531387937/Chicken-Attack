using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFightChicken : MonoBehaviour
{
    public FightChicken self;

    private void Start()
    {
        StartCoroutine(SavePos());
    }

    IEnumerator SavePos()
    {
        self.Pos = this.transform.position;
        yield return new WaitForSeconds(30);
        StartCoroutine(SavePos());
    }
}
