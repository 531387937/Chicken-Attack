using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBreed_New : MonoBehaviour
{
    public Chicken gongji;
    public Chicken muji;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        Chicken child = new Chicken();
        child.BirthInitial(gongji, muji);
        gongji.Life--;
        muji.Life--;
        GameSave.Instance.AddChicken(child, "New Born");
    }
}
