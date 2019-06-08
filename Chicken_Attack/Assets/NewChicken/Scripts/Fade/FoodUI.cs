using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Food
{
    绿豆 = 0,
    花生 = 1,
    麦子 = 2,
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
    public Food food;
    private bool CurrentFoodActive = false;
    public GameObject Cover;
    public Material Grey;

    private void Start()
    {
        ThisFoodImage = GetComponent<Image>();
        CanvasRectangle = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
        StartPos = ThisFoodImage.rectTransform.anchoredPosition;
        ThisFoodImage.sprite = NormalSprite;
        if (GameSaveNew.Instance.PD.FoodRights[(int)food])
        {
            CurrentFoodActive = true;
        }
        Debug.Log(food.ToString() + ":" + GameSaveNew.Instance.PD.FoodRights[(int)food]);
        Debug.Log(" CurrentFoodActive:" + CurrentFoodActive);
        if (!CurrentFoodActive)
        {
            //Cover.SetActive(false);
            GetComponent<Image>().material = Grey;
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
                else if (hit.collider.tag == "Chick")
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
                if(CurrentChicken.tag== "FightChicken")
                {
                    if (GameSaveNew.Instance.PD.Gold - Cost > 0)//有钱才可以购买
                    {
                        CurrentChicken.GetComponent<MyFightChicken>().self.Hungry += HungryADD;
                        GameSaveNew.Instance.PD.Gold -= Cost;
                        ThisFoodImage.sprite = NormalSprite;
                        if (CurrentChicken.GetComponent<MyFightChicken>().self.Hungry > 100)
                        {
                            CurrentChicken.GetComponent<MyFightChicken>().self.Hungry = 100;
                        }
                    }
                    else
                    {
                        ThisFoodImage.sprite = NormalSprite;
                    }
                }
                else if (CurrentChicken.tag == "Chick")
                {
                    if (GameSaveNew.Instance.PD.Gold - Cost * 1.2f > 0)//有钱才可以购买
                    {
                        CurrentChicken.GetComponent<Chick>().self.Grow += HungryADD * 0.8f;
                        GameSaveNew.Instance.PD.Gold -= Cost + 2;
                        ThisFoodImage.sprite = NormalSprite;
                    }
                    else
                    {
                        ThisFoodImage.sprite = NormalSprite;
                    }
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
}
