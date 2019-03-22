using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHome : Singleton<ChickenHome>
{
    public Sprite[] SpRender;
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<SpriteRenderer>().sprite= SpRender[GameSave.Instance.PD.a];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChickenHomeUpdate()
    {
        if(GameSave.Instance.PD.a<GameSave.Instance.PD.MaxChicken.Length)
        GameSave.Instance.PD.a++;
        GetComponent<SpriteRenderer>().sprite = SpRender[GameSave.Instance.PD.a];
        GameSave.Instance.PD.CurrentMaxChicken = GameSave.Instance.PD.MaxChicken[GameSave.Instance.PD.a];
    }
}
