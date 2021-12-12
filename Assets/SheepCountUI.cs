using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepCountUI : MonoBehaviour
{
    [HideInInspector]
    public int maxSheepCount = 1;
    
    [Header("Components")]
    public Text text;
    
    private int sheepCount = 0;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void ResetSheepCount()
    {
        sheepCount = 0;
        UpdateText();
    }

    public void PlusSheep()
    {
        sheepCount++;
        UpdateText();
    }

    public void MinusSheep()
    {
        sheepCount--;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = "" + sheepCount + "/" + maxSheepCount;
    }
}