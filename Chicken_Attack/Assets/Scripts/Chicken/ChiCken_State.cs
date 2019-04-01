using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChiCken_State : MonoBehaviour
{
    public Chicken ThisChicken = new Chicken();
    public Sprite[] Sp;
    // Start is called before the first frame update
    void Start()
    {      
        if (!ThisChicken.Alive)
        {
            ThisChicken.RandomInitial(Random.Range(0,2));
            ThisChicken.Alive = true;
        }
        GetComponent<SpriteRenderer>().sprite = Sp[(int)ThisChicken.Type];
        gameObject.transform.position = ThisChicken.pos;
    }
    private void OnMouseDown()
    {
        //ChickenList.chickenList.Remove(ThisChicken);
        //if(!ChickenList.chickenList.Contains(ThisChicken))
        //{
        //    Destroy(gameObject);
        //}
    }
    // Update is called once per frame
    void Update()
    {
        ThisChicken.pos = gameObject.transform.position;
        if(ThisChicken.Life<=0)
        {            
            GameSave.Instance.RemoveChicken(ThisChicken);
            Destroy(this.gameObject);
            GameSave.Instance.SaveAllData();
        }
    }
}
