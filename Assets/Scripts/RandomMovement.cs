using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // A simple class designed to send the NPCs around the map to various points.
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
    }

    void Update()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            NextPoint();
    }

    private void NextPoint()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        float x = Random.Range(-60, 60);
        float y = 1.2f;
        float z = Random.Range(-30, 30);
        agent.destination = new Vector3(x, y, z);
    }
}
