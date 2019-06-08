using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideRetire : MonoBehaviour
{
    public Sprite Open;
    public Sprite Close;
    public Image image;
    public bool Hide = false;
    public Player_Chicken Player_Chicken;

    public void ChangeState()
    {
        if(GameSaveNew.Instance.PD.OldChicken.Count > 0)
        {
            Hide = !Hide;
            if (Hide)
            {
                image.sprite = Close;
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("RetireChicken"))
                {
                    Destroy(g);
                }
            }
            else if (!Hide)
            {
                image.sprite = Open;
                Player_Chicken.ShowRetire();
            }
        }
    }

}
