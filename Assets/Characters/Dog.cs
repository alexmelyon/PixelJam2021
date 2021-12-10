using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer influenceSprite;
    public float minInfluenceRadius = 5F;
    public float maxInfluenceRadius = 20F;

    private MovePanel movePanel;

    private void Awake()
    {
        movePanel = FindObjectOfType<MovePanel>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float radius = Mathf.Lerp(minInfluenceRadius, maxInfluenceRadius, movePanel.direction.magnitude);
        influenceSprite.transform.localScale = new Vector3(radius, radius, 1);
    }
}
