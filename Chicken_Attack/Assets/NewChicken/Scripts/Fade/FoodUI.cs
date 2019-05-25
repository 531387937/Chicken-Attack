using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Food
{
    绿豆 = 0,
    花生 = 1,
    麦子 = 2
}

class FoodUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image ThisFoodImage;
    private RectTransform CanvasRectangle;
    private Vector2 StartPos;
    public Sprite NormalSprite;
    public Sprite CanUseSprite;
    private GameObject CurrentChicken;
    public int HungryADD = 5;
    public int Cost = 5;
    private GameSaveNew GameData;
    public Food food;
    private bool CurrentFoodActive = false;
    public GameObject Cover;

    private void Start()
    {
        ThisFoodImage = GetComponent<Image>();
        CanvasRectangle = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
        GameData = GameObject.FindGameObjectWithTag("GM").GetComponent<GameSaveNew>();
        StartPos = ThisFoodImage.rectTransform.anchoredPosition;
        ThisFoodImage.sprite = NormalSprite;
        if (GameData.PD.FoodRights[(int)food])
        {
            CurrentFoodActive = true;
        }
        Debug.Log(food.ToString() + ":" + GameData.PD.FoodRights[(int)food]);
        Debug.Log(" CurrentFoodActive:" + CurrentFoodActive);
        if (CurrentFoodActive)
        {
            Cover.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CurrentFoodActive)
        {
            ThisFoodImage.transform.position = Input.mousePosition;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "FightChicken")
                {
                    CurrentChicken = hit.collider.gameObject;
                    ThisFoodImage.sprite = CanUseSprite;
                    Debug.Log("Target Position: " + hit.collider.gameObject.name);
                }
            }
            else
            {
                CurrentChicken = null;
                ThisFoodImage.sprite = NormalSprite;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CurrentFoodActive)
        {
            ThisFoodImage.rectTransform.anchoredPosition = StartPos;
            if (CurrentChicken != null && ThisFoodImage.sprite == CanUseSprite)
            {
                if (GameData.PD.Gold - Cost > 0)//有钱才可以购买
                {
                    CurrentChicken.GetComponent<MyFightChicken>().self.Hungry += HungryADD;
                    GameData.PD.Gold -= Cost;
                    ThisFoodImage.sprite = NormalSprite;
                    if (CurrentChicken.GetComponent<MyFightChicken>().self.Hungry > 100)
                    {
                        CurrentChicken.GetComponent<MyFightChicken>().self.Hungry = 100;
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
}
