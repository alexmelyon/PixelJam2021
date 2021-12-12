using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour
{
    public enum Behavior
    {
        RANDOM_WALKING, RUN_FROM_DOG
    }

    public float walkDistance = 5F;
    public float runDistance = 10F;

    private Behavior _state;
    private NavMeshAgent agent;
    private Dog dog;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        dog = FindObjectOfType<Dog>();
    }

    private void Start()
    {
        SetState(Behavior.RANDOM_WALKING);
    }

    IEnumerator RandomWalking()
    {
        while (_state == Behavior.RANDOM_WALKING)
        {
            float seconds = Random.value * 5F + 2;
            yield return new WaitForSeconds(seconds);
            float x = Random.value * 2 - 1;
            float z = Random.value * 2 - 1;
            Vector3 newPos = new Vector3(x * walkDistance, 0, z * walkDistance) + agent.transform.position;
            agent.Warp(agent.transform.position);
            agent.SetDestination(newPos);
        }
    }

    IEnumerator RunFromDog()
    {
        while (_state == Behavior.RUN_FROM_DOG)
        {
            var diff = (transform.position - dog.transform.position).normalized * runDistance;
            var dest = transform.position + diff;
            agent.Warp(agent.transform.position);
            agent.SetDestination(dest);
            while ((dest - agent.transform.position).magnitude > 1F)
            {
                yield return new WaitForSeconds(1F);
            }
            
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ZoneInfluence>() != null)
        {
            SetState(Behavior.RUN_FROM_DOG);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if (other.GetComponent<ZoneInfluence>() != null)
        // {
        //     SetState(Behavior.RANDOM_WALKING);
        // }
    }

    void SetState(Behavior next)
    {
        // Debug.Log(gameObject.name + " " + next);
        _state = next;
        switch (next)
        {
            case Behavior.RANDOM_WALKING: StartCoroutine(RandomWalking()); break;
            case Behavior.RUN_FROM_DOG: StartCoroutine(RunFromDog()); break;
        }
    }
}
