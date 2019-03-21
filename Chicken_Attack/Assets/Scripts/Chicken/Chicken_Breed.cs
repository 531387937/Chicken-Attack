using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chicken_Breed : MonoBehaviour
{
    private Chicken father;
    private Chicken mother;
    private GameObject[] parents;
    public GameObject Chicken_Home;
    // Start is called before the first frame update
    void Start()
    {
        parents = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ChiCken_State>().ThisChicken.isCock)
        {
            if(parents[0]!=null)
            {
                parents[0].transform.position = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.0f, 3.0f), 0);
            }
            parents[0] = collision.gameObject;
            Chicken_Home.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            father = collision.gameObject.GetComponent<ChiCken_State>().ThisChicken;
            Chicken_Home.transform.GetChild(0).GetComponent<Image>().sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            print("father is ready");
        }
        if (!collision.gameObject.GetComponent<ChiCken_State>().ThisChicken.isCock)
        {
            if (parents[1] != null)
            {
                parents[1].transform.position = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.0f, 3.0f), 0);
            }
            parents[1] = collision.gameObject;
            Chicken_Home.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            mother = collision.gameObject.GetComponent<ChiCken_State>().ThisChicken;
            Chicken_Home.transform.GetChild(1).GetComponent<Image>().sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            print("mother is ready");
        }
        if (mother != null && father != null)
        {
            Chicken child = new Chicken();
            child.BirthInitial(father, mother);
            GameSave.Instance.AddChicken(child, "New Born");
            Chicken_Home.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            Chicken_Home.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
            parents[0].transform.position = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.0f, 3.0f), 0);
            parents[1].transform.position = new Vector3(Random.Range(-3.0f, 4.0f), Random.Range(-2.0f, 3.0f), 0);
            parents = null;
            mother = null;
            father = null;
        }
    }
    
}
