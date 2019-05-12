using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    private Transform Tr;
    public float speed;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        Tr = GetComponent<Transform>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, 2)];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0);
    }
}
