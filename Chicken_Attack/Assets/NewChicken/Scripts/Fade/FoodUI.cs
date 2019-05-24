using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Script
{
    class FoodUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Image ThisFoodImage;
        private RectTransform CanvasRectangle; 
        private Vector2 StartPos;
        public Sprite NormalSprite;
        public Sprite CanUseSprite;
        private GameObject CurrentChicken;
        public int HungryADD;

        private void Start()
        {
            ThisFoodImage = GetComponent<Image>();
            CanvasRectangle = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>();
            StartPos = ThisFoodImage.rectTransform.anchoredPosition;
            ThisFoodImage.sprite = NormalSprite;
        }

        public void OnDrag(PointerEventData eventData)
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
                else
                {
                    CurrentChicken = null;
                    ThisFoodImage.sprite = NormalSprite;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ThisFoodImage.rectTransform.anchoredPosition = StartPos;
            if (CurrentChicken != null && ThisFoodImage.sprite == CanUseSprite)
            {
                CurrentChicken.GetComponent<MyFightChicken>().self.Hungry += HungryADD;
                ThisFoodImage.sprite = NormalSprite;
                if (CurrentChicken.GetComponent<MyFightChicken>().self.Hungry > 100)
                {
                    CurrentChicken.GetComponent<MyFightChicken>().self.Hungry = 100;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }
    }
}
