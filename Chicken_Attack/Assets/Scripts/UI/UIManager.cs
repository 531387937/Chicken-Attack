using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public ToolTip toolTip;
    private bool isShow = false;
    private bool isDrag = false;
    private void OnEnable()
    {
        GridImage.OnEnter += GridImage_OnEnter;
        GridImage.OnExit += GridImage_OnExit;
        GridImage.OnDrag += GridImage_OnDrag;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isShow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, null, out position);
            toolTip.SetLocationPosition(position);
            toolTip.Show();
            isShow = false;
        }
    }
    private void GridImage_OnEnter(Transform obj)
    {
        Chicken c = obj.gameObject.GetComponent<ChiCken_State>().ThisChicken;
        string text = GetTooltipText(c);
        toolTip.UpdateTolTip(text);
        isShow = true;
    }
    private void GridImage_OnExit()
    {
        isShow = false;
        toolTip.Hide();
    }
    private string GetTooltipText(Chicken chicken)
    {
        string te ="\n"+"种族:"+chicken.Type+"\n"+"生命值:" + chicken.HP + "\n" + "攻击力:" + chicken.Attak + "\n" + "速度:" + chicken.Speed + "\n"+"公鸡："+chicken.isCock+"\n";
        return te;
    }
    private void GridImage_OnDrag(Transform obj)
    {
        isDrag = true;
    }
}
