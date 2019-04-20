using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_NEW : Singleton<UIManager>
{
    public ToolTip_NEW toolTip;
    private bool isShow = false;
    private bool isDrag = false;
    [SerializeField]
    private GridImage_NEW gridImage;
    public GameObject fa;
    private bool Showing = false;
    private void OnEnable()
    {
        GridImage_NEW.OnEnter += GridImage_OnEnter;
        GridImage_NEW.OnExit += GridImage_OnExit;
        GridImage_NEW.OnDrag += GridImage_OnDrag;
        GridImage_NEW.OnUp += GridImage_OnUp;
    }
    private void OnDisable()
    {
        GridImage_NEW.OnEnter -= GridImage_OnEnter;
        GridImage_NEW.OnExit -= GridImage_OnExit;
        GridImage_NEW.OnDrag -= GridImage_OnDrag;
        GridImage_NEW.OnUp -= GridImage_OnUp;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Showing&&gridImage!=null)
        {
            gridImage.timer += Time.deltaTime;
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
        gridImage = obj.gameObject.GetComponent<GridImage_NEW>();
        Showing = true;
        gridImage._time++;
        if (Showing && gridImage.timer <= 0.3f && gridImage._time == 2)
        {
            Showing = false;
            isShow = true;
            FightChicken c = obj.gameObject.GetComponent<MyFightChicken>().self;
        string text = GetTooltipText(c);
        toolTip.UpdateTolTip(text);
        }
    }
    private void GridImage_OnExit()
    {


        Showing = false;
        isShow = false;
       
        toolTip.Hide();
        if (gridImage != null)
        {
            gridImage._time = 0;
            gridImage.timer = 0;
        }
        //gridImage.gameObject.transform.SetParent(null);
    }
    private void GridImage_OnUp()
    {
        gridImage.gameObject.transform.SetParent(null);
    }
    private string GetTooltipText(FightChicken chicken)
    {
        string te ="生命值:" + chicken.HP + "\n" + "攻击力:" + chicken.Attack + "\n"+"斗志：" + chicken.Spirit + "\n"+ "气势:" + chicken.Strong + "\n"+"速度："+chicken.Speed+"\n";
        return te;
    }
    private void GridImage_OnDrag(Transform obj)
    {
        isDrag = true;
        Vector3 pos;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //这个fa看起来并不是必须的，可否去除xy19.3.28
        //不要去除，否则点击的时候会有位移
        fa.transform.position = new Vector3(pos.x, pos.y, 0);
        obj.SetParent(fa.transform);
    }
}
