using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator dogAnimator;
    private LoveMeter loveMeter;
    private bool isFetching;
    private bool isFollowing;
    private bool isWalking;
    private Transform originalPosition;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        dogAnimator = this.GetComponentInChildren<Animator>();
        loveMeter = this.GetComponentInChildren<LoveMeter>();
        isFetching = false;
        isWalking = false;

    }

    private void Update()
    {
        if (agent.velocity != Vector3.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        
        // TODO: Change this so you don't have to do it every frame
        dogAnimator.SetBool("IsWalking", isWalking);
    }

    public void Speak()
    {
        if (!isWalking)
        {
            dogAnimator.Play("Bark");
        }
    }

    public void Dead()
    {
        if (!isWalking)
        {
            dogAnimator.Play("Dead");
        }
    }

    public void Sit()
    {
        if (!isWalking)
        {
            dogAnimator.Play("Sit");
        }
    }

    public void Lay()
    {
        if (!isWalking)
        {
            dogAnimator.Play("Lay");
        }
    }

    public void Come()
    {
        // Trigger a state to tell the dog to walk toward the player
    }

    public void Follow()
    {
        isFollowing = true;
        // Set the dog to automatically follow the player
    }

    public void Stop()
    {
        isFollowing = false;
        // Set the dog to stop following the player
    }

    public void Navigate(Transform destination)
    {
        // TODO: Make the rotation slower?
        //this.transform.LookAt(destination);
        Vector3 destinationVector = destination.transform.position;
        agent.SetDestination(destinationVector);
    }

    public void BeginFetch(GameObject objectToFetch)
    {
        if (!this.isFetching)
        {
            originalPosition = this.transform;
            this.Navigate(objectToFetch.transform);
        }
    }

    public void EndFetch()
    {
        if (this.isFetching)
        {
            this.Navigate(originalPosition);
            this.isFetching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Pick up object
                // Make dog parent of object
                // Set to mouth position
        }
    }
}
