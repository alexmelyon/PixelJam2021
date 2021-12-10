using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePanel : MonoBehaviour
{
    public Image circle;
    public Image finger;
    
    private Vector2 defPos;
    
    void Start()
    {
    }

    private void OnEnable()
    {
        defPos = circle.transform.position;
        Debug.Log("DEFAULT " + defPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var rect = (RectTransform) transform;
            Vector2 point;
            bool res = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.current, out point);
            if (res)
            {
                Debug.Log("POINT " + point);
                circle.transform.position = point + defPos;
            }
        }
        else if (Input.GetMouseButton(0))
        {
        }
        else
        {
            circle.transform.position = defPos;
        }
    }
}
