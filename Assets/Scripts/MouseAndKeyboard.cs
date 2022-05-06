using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKeyboard : PlayerInput
{
    private Vector2? mouseDownPos = null;
    float dragVectMultiplier = 0.01f;

    bool levelEnded = false;

    void Update()
    {
        if (!levelEnded)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mouseDownPos = null;
                DragVect = null;
            }
            else if (mouseDownPos != null)
            {
                Vector2 mouseCurPos = Input.mousePosition;
                var mouseDragVect = (mouseCurPos - mouseDownPos.Value) * dragVectMultiplier;
                if (mouseDragVect.magnitude > maxStretchLength)
                {
                    mouseDownPos += mouseDragVect - (mouseDragVect.normalized * maxStretchLength);
                }
                DragVect = Vector2.ClampMagnitude(mouseDragVect, maxStretchLength);
            }


            if (Input.GetMouseButtonDown(1))
            {
                PressingBreakBtn = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                PressingBreakBtn = false;
            }
        }
    }


    private void Start()
    {
        GameManager.Instance.OnGameEnd += OnEndLevel;
    }

    void OnEndLevel(int stars)
    {
        levelEnded = true;
    }
    //    // get z coordinate of world in screen coordinates
    //    private float ZScreen()
    //    {
    //        return Camera.main.WorldToScreenPoint(transform.position).z;
    //    }
    //
    //    Vector3 MousePosWorld()
    //    {
    //        var mousePositionS = Input.mousePosition;
    //        mousePositionS.z = ZScreen();
    //        return Camera.main.ScreenToWorldPoint(mousePositionS);
    //    }

}
