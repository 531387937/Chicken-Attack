using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridImage : MonoBehaviour
{

    public static Action<Transform> OnEnter;

    public static Action OnExit;

    //public void OnPointerEnter(PointerEventData eventData)

    //{

    //    if (OnEnter != null)
    //    {

    //        OnEnter(transform);

    //    }

    //}
    public void OnMouseEnter()
    {
        if (OnEnter != null)
        {

            OnEnter(transform);

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