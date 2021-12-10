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
    
    private Behavior state = Behavior.RANDOM_WALKING;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(RandomWalking());
    }

    IEnumerator RandomWalking()
    {
        while (state == Behavior.RANDOM_WALKING)
        {
            yield return new WaitForSeconds(5F);
            Debug.Log("WALK " + gameObject.name);
            Vector3 newPos = new Vector3(Random.value * walkDistance, 0, Random.value * walkDistance) + agent.transform.position;
            agent.Warp(agent.transform.position);
            agent.SetDestination(newPos);
        }
        yield break;
    }
}
