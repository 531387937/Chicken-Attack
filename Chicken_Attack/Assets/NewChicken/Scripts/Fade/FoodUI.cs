using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Script
{
    class FoodUI : MonoBehaviour
    {
        public Image Body;//ui元素的本体, panel等
        public RectTransform CanvasRectangle; //image所在的画布
        private bool dragging;
        private Vector2 targetPosition;//移动时目标位置
        private Vector2 offset;//开始移动前记录鼠标与body之间的偏移距离
        public void Update()
        {
            if (dragging)
            {
                //以0.5倍的单位将ui向目标位置移动
                Body.rectTransform.anchoredPosition += (targetPosition - Body.rectTransform.anchoredPosition) * 0.5f;
            }
        }
        public void OnBeginDrag(BaseEventData baseEventData)
        {
            PointerEventData pointerEventData = baseEventData as PointerEventData;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                CanvasRectangle, pointerEventData.position, pointerEventData.pressEventCamera, out offset);
            //计算偏移量
            offset = Body.rectTransform.anchoredPosition - offset;
            dragging = true;
        }
        public void OnDrag(BaseEventData baseEventData)
        {
            PointerEventData pointerEventData = baseEventData as PointerEventData;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                CanvasRectangle, pointerEventData.position, pointerEventData.pressEventCamera, out targetPosition);
            //更新目标位置
            targetPosition = targetPosition + offset;
        }
        public void OnEndDrag(BaseEventData baseEventData)
        {
            dragging = false;
        }
    }
}
