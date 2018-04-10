using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour {

    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
	}

    public void Navigate(Transform destination)
    {
        // TODO: Make the rotation slower?
        this.transform.LookAt(destination);
        Vector3 destinationVector = destination.transform.position;
        agent.SetDestination(destinationVector);
    }
}
