using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    private Transform Tr;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0);
    }
}
