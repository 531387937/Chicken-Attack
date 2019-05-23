using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager_NEW : Singleton<UIManager_NEW>
{
    //public ToolTip_NEW toolTip;
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI Prestige;
    public TextMeshProUGUI Pt;
    //private bool isShow = false;
    //private bool isDrag = false;
    [SerializeField]
    private GridImage_NEW gridImage;
    //暂时没有用了
    public GameObject fa;
    //private bool Showing = false;
    //新的鸡的属性显示
    //public GameObject ChickenPanel;
    //鸡的属性
    public TextMeshProUGUI ATK;
    //public TextMeshProUGUI HP;
    //public TextMeshProUGUI Spirit;
    //public TextMeshProUGUI Strong;

    public bool CanTouch = true;
    //鸡的大头照
    public Image Head;
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

    // Update is called once per frame
    void Update()
    {
        //if(Showing&&gridImage!=null)
        //{
        //    gridImage.timer += Time.deltaTime;
        //}
        //if(isShow)
        //{
        //    Vector2 position;
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, Input.mousePosition, null, out position);
        //    //toolTip.SetLocationPosition(position);
        //    //toolTip.Show();
        //    isShow = false;
        //}
        Gold.text = GameSaveNew.Instance.PD.Gold.ToString();
        Prestige.text = GameSaveNew.Instance.PD.Prestige.ToString();
        Pt.text = GameSaveNew.Instance.PD.Pt.ToString();
        ATK.text = GameSaveNew.Instance.playerChicken.Power.ToString();
            //HP.text = GameSaveNew.Instance.playerChicken[GameSaveNew.Instance.ChooseChicken].HP.ToString();
            //Spirit.text = GameSaveNew.Instance.playerChicken[GameSaveNew.Instance.ChooseChicken].Spirit.ToString();
            //Strong.text = GameSaveNew.Instance.playerChicken[GameSaveNew.Instance.ChooseChicken].Strong.ToString();
    }
    //没有拖拽动画，暂且取消拖拽事件
    private void GridImage_OnEnter(Transform obj)
    {
        //gridImage = obj.gameObject.GetComponent<GridImage_NEW>();
        //Showing = true;
        //gridImage._time++;
        //if (Showing && gridImage.timer <= 0.3f && gridImage._time == 2)
        //{
        //    Showing = false;
        //    isShow = true;
        //    FightChicken c;
        //    if (obj.tag == "FightChicken")
        //    {
        //        c = obj.gameObject.GetComponent<MyFightChicken>().self;
        //        string text = GetTooltipText(c);
        //        toolTip.UpdateTolTip(text);
        //    }
        //    else if(obj.tag == "Chick")
        //    {
        //        c = obj.gameObject.GetComponent<Chick>().self;
        //        string text = GetTooltipText(c);
        //        toolTip.UpdateTolTip(text);
        //    }
        //}



        //直接将被点击的鸡成为被选择的鸡，用于前往战斗、训练、繁殖等
        //if (CanTouch)
        //{
            //gridImage = obj.gameObject.GetComponent<GridImage_NEW>();
            //ChickenPanel.SetActive(true);
        //}
    }

    private void GridImage_OnExit()
    {
        //Showing = false;
        //isShow = false;
       
        //toolTip.Hide();
        //if (gridImage != null)
        //{
        //    gridImage._time = 0;
        //    gridImage.timer = 0;
        //}
        ////gridImage.gameObject.transform.SetParent(null);
    }

    private void GridImage_OnUp()
    {
        //gridImage.gameObject.transform.SetParent(null);
    }

    //private string GetTooltipText(FightChicken chicken)
    //{
    //    string te ="生命值：" + chicken.HP + "\n" + "攻击力：" + chicken.Attack + "\n"+"斗志：" + chicken.Spirit + "\n"+ "气势：" + chicken.Strong +"\n";
    //    return te;
    //}

    private void GridImage_OnDrag(Transform obj)
    {
        //isDrag = true;
        //Vector3 pos;
        //pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ////这个fa看起来并不是必须的，可否去除xy19.3.28
        ////不要去除，否则点击的时候会有位移
        ////好哒xy19.4.26
        //fa.transform.position = new Vector3(pos.x, pos.y, 0);
        //obj.SetParent(fa.transform);
    }

}
