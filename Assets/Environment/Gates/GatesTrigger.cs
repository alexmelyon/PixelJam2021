using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GatesTrigger : MonoBehaviour
{
    public int maxSheepCount = 1;
    
    public UnityEvent onCloseGates;
    // public UnityEvent<int> onCountChanged;
    
    [HideInInspector]
    public int sheepsCount = 0;
    private SheepCountUI SheepCountUI;

    private void Awake()
    {
        SheepCountUI = FindObjectOfType<SheepCountUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isThisMySheep(other))
            return;
        
        sheepsCount++;
        // onCountChanged.Invoke(sheepsCount);
        SheepCountUI.PlusSheep();
        Debug.Log("SHEEP ENTER " + sheepsCount);

        if (sheepsCount >= maxSheepCount)
        {
            onCloseGates.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isThisMySheep(other))
            return;

        sheepsCount--;
        // onCountChanged.Invoke(sheepsCount);
        SheepCountUI.MinusSheep();
        
        Debug.Log("SHEEP EXIT " + sheepsCount);
    }

    private bool isThisMySheep(Collider other)
    {
        if(other.GetComponent<Sheep>() == null)
            return false;

        // if(!sheeps.Select(it => it.gameObject).Contains(other.gameObject))
        //     return false;

        return true;
    }
}
