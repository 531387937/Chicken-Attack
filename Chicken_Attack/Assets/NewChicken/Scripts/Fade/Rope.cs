using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    BoxCollider2D box;
    public ChickenBreed ChickenBreed;
    float y;
    bool DO = true;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        y = this.transform.position.y;
        print("y:"+(y-10));
    }

    // Update is called once per frame
    void Update()
    {
        if (box.OverlapPoint(Input.mousePosition))
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                this.transform.position -= new Vector3(0, (y + Input.touches[0].position.y) / 100, 0);
            }
        }
        print(this.transform.position.y);
        if (this.transform.position.y < y - 120 && DO)
        {
            ChickenBreed.Three_Eggs();
            DO = false;
            Destroy(this.gameObject);
        }
    }
}
