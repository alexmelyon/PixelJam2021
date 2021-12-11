using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GatesTrigger : MonoBehaviour
{
    public Sheep[] sheeps;
    
    public UnityEvent onCloseGates;
    public UnityEvent<int> onCountChanged;

    private int sheepsCount = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if(!isThisMySheep(other))
            return;
        
        sheepsCount++;
        onCountChanged.Invoke(sheepsCount);
        Debug.Log("SHEEP ENTER " + sheepsCount);

        if (sheepsCount == sheeps.Length)
        {
            onCloseGates.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Sheep>() == null)
            return;

        sheepsCount--;
        onCountChanged.Invoke(sheepsCount);
        
        Debug.Log("SHEEP EXIT " + sheepsCount);
    }

    private bool isThisMySheep(Collider other)
    {
        if(other.GetComponent<Sheep>() == null)
            return false;

        if(!sheeps.Select(it => it.gameObject).Contains(other.gameObject))
            return false;

        return true;
    }
}
