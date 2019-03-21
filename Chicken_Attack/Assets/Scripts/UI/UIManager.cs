using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public ToolTip toolTip;
    private bool isShow = false;
    private bool isDrag = false;
    [SerializeField]
    private GridImage gridImage;
    private bool Showing = false;
    private void OnEnable()
    {
        GridImage.OnEnter += GridImage_OnEnter;
        GridImage.OnExit += GridImage_OnExit;
        GridImage.OnDrag += GridImage_OnDrag;
    }
    private void OnDestroy()
    {
        GridImage.OnEnter -= GridImage_OnEnter;
        GridImage.OnExit -= GridImage_OnExit;
        GridImage.OnDrag -= GridImage_OnDrag;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Showing)
        {
            gridImage.timer += Time.deltaTime;
            print("AAAAAAAAAAAA");
        }
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
        gridImage = obj.gameObject.GetComponent<GridImage>();
        Showing = true;
        gridImage._time++;
        if (Showing && gridImage.timer <= 0.3f && gridImage._time == 2)
        {
            Showing = false;
            isShow = true;
            Chicken c = obj.gameObject.GetComponent<ChiCken_State>().ThisChicken;
        string text = GetTooltipText(c);
        toolTip.UpdateTolTip(text);
        }
    }
    private void GridImage_OnExit()
    {


        Showing = false;
        isShow = false;
        toolTip.Hide();
        gridImage._time = 0;
           gridImage.timer = 0;
    }
    private string GetTooltipText(Chicken chicken)
    {
        string te ="\n"+"种族:"+chicken.Type+"\n"+"生命值:" + chicken.HP + "\n" + "攻击力:" + chicken.Attak + "\n" + "速度:" + chicken.Speed + "\n"+"公鸡："+chicken.isCock+"\n";
        return te;
    }
    private void GridImage_OnDrag(Transform obj)
    {
        isDrag = true;
        Vector3 pos;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        obj.position = new Vector3(pos.x, pos.y, 0);
    }
}
