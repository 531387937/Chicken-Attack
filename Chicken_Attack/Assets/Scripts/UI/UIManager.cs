using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public ToolTip toolTip;
    private bool isShow = false;
    private void OnEnable()
    {
        GridImage.OnEnter += GridImage_OnEnter;
        GridImage.OnExit += GridImage_OnExit;
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
        string te ="名字:"+chicken.Name+"\n"+"种族:"+chicken.Type+"\n"+"生命值:" + chicken.HP + "\n" + "攻击力:" + chicken.Attak + "\n" + "速度:" + chicken.Speed + "\n";
        return te;
    }
}
