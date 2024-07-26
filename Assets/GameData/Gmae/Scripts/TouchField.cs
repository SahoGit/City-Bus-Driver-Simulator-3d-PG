using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool Pressed;
    [HideInInspector] public Vector2 TouchDist;
    Vector2 PointerOld;
    //int PointerId;

    Vector2 zeroVec = Vector2.zero;

    void Update()
    {
        if (Pressed)
        {
            if (Input.touchCount > 0)
            {
                TouchDist = Input.GetTouch(0).position - PointerOld;
                PointerOld = Input.GetTouch(0).position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = zeroVec;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Pressed)
            PointerOld = eventData.position;

        Pressed = true;
        //PointerId = eventData.pointerId;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        //if (Input.touchCount == 0)
            Pressed = false;
    }

    private void OnDisable()
    {
        Pressed = false;
    }
}
