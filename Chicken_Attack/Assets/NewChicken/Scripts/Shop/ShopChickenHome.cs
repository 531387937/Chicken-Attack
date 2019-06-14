using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopChickenHome : MonoBehaviour
{
    private ShopSystem ShopSystem;
    public Texture[] ChickenHomes;
    public int[] Cost;
    public Text CostText;
    public RawImage HomeTex;
    public Vector3[] HomeScale;
    public int MaxLevel = 2;
    public Material UnActiveGrey;

    // Start is called before the first frame update
    void Start()
    {
        ShopSystem = GameObject.Find("ShopChickenS").GetComponent<ShopSystem>();
        if (GameSaveNew.Instance.PD.HomeLevel < MaxLevel)
        {
            CostText.text = Cost[GameSaveNew.Instance.PD.HomeLevel].ToString() + "G";
            HomeTex.texture = ChickenHomes[GameSaveNew.Instance.PD.HomeLevel + 1];
            HomeTex.transform.localScale = HomeScale[GameSaveNew.Instance.PD.HomeLevel];
        }
        else
        {
            CostText.text = "MAX";
            HomeTex.texture = ChickenHomes[GameSaveNew.Instance.PD.HomeLevel];
            HomeTex.transform.localScale = HomeScale[GameSaveNew.Instance.PD.HomeLevel - 1];
            HomeTex.material = UnActiveGrey;
            //this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Input.mousePosition))
            {
                ShopSystem.Broad.active = true;
                ShopSystem.Broad.GetComponent<Board_Shop>().SetSomeThing2Buy(Cost[GameSaveNew.Instance.PD.HomeLevel]);
                int Nextlevel = GameSaveNew.Instance.PD.HomeLevel + 1;
                ShopSystem.Broad.GetComponentInChildren<TextMeshProUGUI>().text = "是否花费" + Cost[GameSaveNew.Instance.PD.HomeLevel] + "G" + "升级房屋到等级"+ Nextlevel + "?";
            }
        }
    }

    public void Fresh()
    {
        if (GameSaveNew.Instance.PD.HomeLevel < MaxLevel)
        {
            CostText.text = Cost[GameSaveNew.Instance.PD.HomeLevel].ToString() + "G";
            HomeTex.texture = ChickenHomes[GameSaveNew.Instance.PD.HomeLevel + 1];
            HomeTex.transform.localScale = HomeScale[GameSaveNew.Instance.PD.HomeLevel];
        }
        else if (GameSaveNew.Instance.PD.HomeLevel == MaxLevel)
        {
            CostText.text = "MAX";
            HomeTex.texture = ChickenHomes[GameSaveNew.Instance.PD.HomeLevel];
            HomeTex.transform.localScale = HomeScale[GameSaveNew.Instance.PD.HomeLevel - 1];
            HomeTex.material = UnActiveGrey;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
