using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    [Header("Objects")]
    public WinPanel winPanel;

    [Header("Components")]
    public Text text;
    
    private int secondsLeft = -1;
    
    public void SetSeconds(int seconds)
    {
        secondsLeft = seconds;
        UpdateText();
        if (seconds > 0)
        {
            StartCoroutine(OneSecond());
        }
    }

    IEnumerator OneSecond()
    {
        secondsLeft--;
        UpdateText();
        if (secondsLeft == 0)
        {
            winPanel.ShowFailPanel();
            yield break;
        }
        yield return new WaitForSeconds(1F);
    }

    void UpdateText()
    {
        text.text = "" + secondsLeft;
    }
}
