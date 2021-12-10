using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePanel : MonoBehaviour
{
    public Camera camera;

    [Header("Childs")] public Image circle;
    public Image finger;

    [Header("Out")] public Vector2 direction = new Vector2();

    private Vector2 defPos;

    private void OnEnable()
    {
        defPos = circle.transform.position;
        if (camera != null)
        {
            camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool isButtonDown = Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
        bool isButtonHold = Input.GetMouseButton(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved;
        bool isButtonUp = Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended;
        Vector2 inputPos = new Vector2();
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            inputPos = Input.mousePosition;
        }
        else if (Input.touchCount > 0)
        {
            inputPos = Input.touches[0].position;
        }

        if (isButtonDown)
        {
            var rect = (RectTransform) transform;
            Vector2 point;
            bool res = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, inputPos, camera, out point);
            if (res)
            {
                // Debug.Log("POINT " + point);
                circle.transform.position = point + defPos;
            }
        }
        else if (isButtonHold)
        {
            var rect = circle.rectTransform;
            Vector2 point;
            bool res = RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, inputPos, camera, out point);
            if (res)
            {
                finger.transform.position = new Vector3(point.x, point.y, 0) + circle.transform.position;
            }
        }
        else if (isButtonUp)
        {
            // circle.transform.position = defPos;
            finger.transform.position = circle.transform.position;
        }

        Vector2 diff = finger.transform.position - circle.transform.position;
        var radius = circle.rectTransform.sizeDelta.x / 2;
        this.direction = diff / radius;
        if (diff.magnitude > radius)
        {
            this.direction = diff.normalized;
            finger.transform.position = this.direction * radius;
            finger.transform.position += circle.transform.position;
        }
    }
}