using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridImage_NEW : MonoBehaviour
{

    public static Action<Transform> OnEnter;

    public static Action OnExit;
    public static Action OnUp;
    public static Action<Transform> OnDrag;
    public int _time=0;
   public float timer;
    public bool IsDrag = false;
    //public void OnPointerEnter(PointerEventData eventData)

    //{

    //    if (OnEnter != null)
    //    {

    //        OnEnter(transform);

    //    }

    //}
    public void OnMouseDown()
    {
        if (OnEnter != null)
        {
            OnEnter(transform);
            
        }
    }
    public void OnMouseUp()
    {IsDrag = false;
        if (OnUp != null)

        {
            
            OnUp();

        }
    }
    public void OnMouseDrag()
    {
        IsDrag = true;
        if (OnDrag!=null)
        {
            OnDrag(transform);
           
        }
    }
    public void OnMouseExit()
    { 
        if (OnExit != null)

        {

            OnExit();

        }
    }
    //public void OnPointerExit(PointerEventData eventData)

    //{

    //    if (OnExit != null)

    //    {

    //        OnExit();

    //    }

    //}


}