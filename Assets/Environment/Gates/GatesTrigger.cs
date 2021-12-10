using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GatesTrigger : MonoBehaviour
{
    public GameObject[] possibleObjects;
    
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (possibleObjects.Contains(other.gameObject))
        {
            onEnter.Invoke();
        }
        // TODO Check all sheep in the aviary
    }
}
