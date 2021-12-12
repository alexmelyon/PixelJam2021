using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{

    [Serializable]
    public class Level
    {
        public Transform start;
        public int sheepCount = 1;
        // public int gatesCount = 1;
        public GatesTrigger[] triggers;
        public Gates[] gates;
        public Sheep[] sheep;
        public int seconds = 60;

        internal int gatesClosed = 0;
        internal Vector3[] sheepTransform;
    }

    [Header("Objects")]
    public SheepCountUI sheepCountUI;
    public CountdownUI countdownUI;
    
    [Header("Childs")]
    public GameObject panel;
    public Button nextButton;
    public Text title;
    
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
        for (int i = 0; i < levels.Length; i++)
        {
            var l = levels[i];
            l.sheepTransform = new Vector3[l.sheep.Length];
            for (int j = 0; j < l.sheep.Length; j++)
            {
                l.sheepTransform[j] = l.sheep[j].transform.position;
            }
        }
        StartLevel(0);
        Debug.Log("LEVEL 0: " + level);
    }

    public void GateClosed()
    {
        Debug.Log("GATE CLOSED " + level);
        level.gatesClosed++;
        if (level.gatesClosed == level.gates.Length)
        {
            ShowWinPanel();
        }
    }

    void ShowWinPanel()
    {
        Debug.Log("SHOW WIN PANEL");
        // nextButton.transform.GetChild(0).GetComponent<Text>().text = "Победа";
        nextButton.gameObject.SetActive(true);
        
        panel.gameObject.SetActive(true);
        // sheepCountUI.gameObject.SetActive(false);
        // countdownUI.gameObject.SetActive(false);
    }

    public void ShowFailPanel()
    {
        // nextButton.transform.GetChild(0).GetComponent<Text>().text = "Поражение";
        nextButton.gameObject.SetActive(false);
        
        panel.gameObject.SetActive(true);
    }
    
    public void NextLevel()
    {
        Debug.Log("NEXT LEVEL");
        StartLevel(currentLevel + 1);
    }

    public void RepeatLevel()
    {
        // StartLevel(currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StartLevel(int num)
    {
        if (num >= levels.Length)
        {
            num = 0;
        }
        panel.SetActive(false);
        
        currentLevel = num;
        level = levels[currentLevel];
        var next = levels[num];
        var nextStart = next.start;
        var nextPos = nextStart.position; 
        dog.transform.position = nextPos;
        foreach (var levelGate in level.gates)
        {
            levelGate.OpenGates();
        }
        for(int i = 0; i < level.sheep.Length; i++)
        {
            var s = level.sheep[i]; 
            s.transform.position = level.sheepTransform[i];
            s.GetComponent<NavMeshAgent>().Warp(s.transform.position);
            s.GetComponent<NavMeshAgent>().SetDestination(s.transform.position);
            s.GetComponent<Rigidbody>().velocity = new Vector3();
            s.GetComponent<Rigidbody>().angularVelocity = new Vector3();
        }
        foreach (var t in level.triggers)
        {
            t.sheepsCount = 0;
        }
        sheepCountUI.ResetSheepCount();
        sheepCountUI.maxSheepCount = level.sheepCount;
        StartCoroutine(StartLevelAnimations());

        Debug.Log("STARTED LEVEL " + num);
    }

    IEnumerator StartLevelAnimations()
    {
        sheepCountUI.gameObject.SetActive(true);
        sheepCountUI.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.5F);
        
        countdownUI.gameObject.SetActive(false);
        if (level.seconds > 0)
        {
            countdownUI.gameObject.SetActive(true);
            countdownUI.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(0.5F);
            countdownUI.SetSeconds(level.seconds);
        }

        yield return null;
    }
}
