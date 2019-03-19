using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChiCken_State : MonoBehaviour
{
    public Chicken ThisChicken = new Chicken();
    // Start is called before the first frame update
    void Start()
    {
        if (!ThisChicken.Alive)
        {
            
               ThisChicken.Type = Chicken.chickenType.Rookie;
            ThisChicken.Name = "菜鸡";
            ThisChicken.HP = Random.Range(12, 18);
            ThisChicken.Level = 1;
            ThisChicken.Exp = 0;
            ThisChicken.Attak = Random.Range(14, 20);
            ThisChicken.Speed = 10;
            ThisChicken.pos = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f), 0);
            ThisChicken.Gender = Random.Range(0, 5);
            if (ThisChicken.Gender == 0)
            {
                ThisChicken.isCock = false;
            }
            else
                ThisChicken.isCock = true;
            ThisChicken.Alive = true;
        }
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
    }
}
