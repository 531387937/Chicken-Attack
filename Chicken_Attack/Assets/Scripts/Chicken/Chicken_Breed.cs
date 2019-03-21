using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken_Breed : MonoBehaviour
{
    private Chicken father;
    private Chicken mother;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ChiCken_State>().ThisChicken.isCock && father == null)
        {
            father = collision.gameObject.GetComponent<ChiCken_State>().ThisChicken;
            print("father is ready");
        }
        if (!collision.gameObject.GetComponent<ChiCken_State>().ThisChicken.isCock && mother == null)
        {
            mother = collision.gameObject.GetComponent<ChiCken_State>().ThisChicken;
            print("mother is ready");
        }
        if (mother != null && father != null)
        {
            Chicken child = new Chicken();
            child.BirthInitial(father, mother);
            GameSave.Instance.AddChicken(child, "New Born");
            print("New Born!");
            mother = null;
            father = null;
        }
    }
}
