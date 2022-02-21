using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pikchin : MonoBehaviour
{
    public enum State { Idle, Move, Interact }

    NavMeshAgent agent;
    [SerializeField] State currentState;
    [SerializeField] float destReachedThreshold = .1f;

    public Transform destinationTestTransform;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = destinationTestTransform.position;
        if(currentState == State.Move && Vector3.Distance(transform.position, agent.destination) < destReachedThreshold)
        {
            currentState = State.Idle;
            agent.isStopped = true;
        }
    }

    public void SetDestination(Vector3 dest)
    {
        currentState = State.Move;
        agent.destination = dest;
        agent.isStopped = false;
    }
}
