using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChiCken_State : MonoBehaviour
{
    public Chicken ThisChicken = new Chicken();
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = ThisChicken.pos;
    }
    private void OnMouseDown()
    {
        print("Click");
        ChickenList.chickenList.Remove(ThisChicken);
        print("Removed");
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        ThisChicken.pos = gameObject.transform.position;
    }
}
