using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{

    [Serializable]
    public class Level
    {
        public Transform start;
        public int gatesCount = 1;

        internal int gatesClosed = 0;
    }
    
    [Header("Childs")]
    public GameObject panel;
    
    [Header("Settings")]
    public Level[] levels;

    private Dog dog;
    private Level level;
    private int currentLevel = 0;

    private void Awake()
    {
        panel = transform.GetChild(0).gameObject;
        dog = FindObjectOfType<Dog>();
        StartLevel(0);
        Debug.Log("LEVEL 0: " + level);
    }

    public void GateClosed()
    {
        Debug.Log("GATE CLOSED " + level);
        level.gatesClosed++;
        if (level.gatesClosed == level.gatesCount)
        {
            ShowWinPanel();
        }
    }

    void ShowWinPanel()
    {
        Debug.Log("SHOW WIN PANEL");
        panel.gameObject.SetActive(true);
    }

    public void ShowFailPanel()
    {
        panel.gameObject.SetActive(true);
    }
    
    public void NextLevel()
    {
        Debug.Log("NEXT LEVEL");
        StartLevel(currentLevel + 1);
    }

    public void RepeatLevel()
    {
        StartLevel(currentLevel);
    }

    void StartLevel(int num)
    {
        panel.SetActive(false);
        
        currentLevel = num;
        level = levels[currentLevel];
        var next = levels[num];
        var nextStart = next.start;
        var nextPos = nextStart.position; 
        dog.transform.position = nextPos;
        
        Debug.Log("STARTED LEVEL " + num);
    }
}
