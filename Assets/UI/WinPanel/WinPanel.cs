using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{

    [Serializable]
    public class Level
    {
        public Transform start;
        public int sheepCount = 1;
        public int gatesCount = 1;
        public int seconds = 60;

        internal int gatesClosed = 0;
    }

    [Header("Objects")]
    public SheepCountUI sheepCount;
    public CountdownUI countdown;
    
    [Header("Childs")]
    public GameObject panel;
    public Button nextButton;
    
    [Header("Settings")]
    public Level[] levels;

    private Dog dog;
    private Level level;
    private int currentLevel = 0;

    private void Awake()
    {
        panel = transform.GetChild(0).gameObject;
        dog = FindObjectOfType<Dog>();
    }

    private void Start()
    {
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
        sheepCount.gameObject.SetActive(false);
        countdown.gameObject.SetActive(false);
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
        sheepCount.ResetSheepCount();
        sheepCount.maxSheepCount = level.sheepCount;
        StartCoroutine(StartLevelAnimations());

        Debug.Log("STARTED LEVEL " + num);
    }

    IEnumerator StartLevelAnimations()
    {
        sheepCount.gameObject.SetActive(true);
        sheepCount.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.5F);
        
        countdown.gameObject.SetActive(false);
        if (level.seconds > 0)
        {
            countdown.gameObject.SetActive(true);
            countdown.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(0.5F);
            countdown.SetSeconds(level.seconds);
        }

        yield return null;
    }
}
