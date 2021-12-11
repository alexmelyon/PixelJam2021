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
            yield return new WaitForSeconds(5F);
            Vector3 newPos = new Vector3(Random.value * walkDistance, 0, Random.value * walkDistance) + agent.transform.position;
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
        Debug.Log(gameObject.name + " " + next);
        _state = next;
        switch (next)
        {
            case Behavior.RANDOM_WALKING: StartCoroutine(RandomWalking()); break;
            case Behavior.RUN_FROM_DOG: StartCoroutine(RunFromDog()); break;
        }
    }
}
