using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator dogAnimator;
    private LoveMeter loveMeter;
    private Transform originalPosition;
    private string currentAnimation;
    private Dictionary<string, string> animationToBoolMapping;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        dogAnimator = this.GetComponentInChildren<Animator>();
        loveMeter = this.GetComponentInChildren<LoveMeter>();
        currentAnimation = "Idle";

        animationToBoolMapping = new Dictionary<string, string>();
        animationToBoolMapping.Add("Speak", "IsSpeaking");
        animationToBoolMapping.Add("Sit", "IsSitting");
        animationToBoolMapping.Add("Lay", "IsLaying");
        animationToBoolMapping.Add("Dead", "IsDead");
        animationToBoolMapping.Add("Walk", "IsWalking");
        animationToBoolMapping.Add("Idle", "IsIdle");
    }

    void Update()
    {
        // If we're done walking, go back to idle
        if (agent.velocity != Vector3.zero)
        {
            SwitchAnimation("Walk");
        }
        else if (currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Idle");
        }
    }

    public void Speak()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Speak");
        }
    }

    public void Dead()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Dead");
        }
    }

    public void Sit()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Sit");
        }
    }

    public void Lay()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Lay");
        }
    }
    
    public void Stop()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Idle");
        }
    }

    //public void Pet()
    //{
    //    if (!isWalking)
    //    {
    //        dogAnimator.Play("BeingPet");
    //    }
    //}

    public void Navigate(Transform destination)
    {
        SwitchAnimation("Walk");
        Vector3 destinationVector = destination.transform.position;
        agent.SetDestination(destinationVector);
    }

    //public void BeginFetch(GameObject objectToFetch)
    //{
    //    if (!this.isFetching)
    //    {
    //        originalPosition = this.transform;
    //        this.Navigate(objectToFetch.transform);
    //    }
    //}

    //public void EndFetch()
    //{
    //    if (this.isFetching)
    //    {
    //        this.Navigate(originalPosition);
    //        this.isFetching = false;
    //    }
    //}

    private void SwitchAnimation(string newAnimation)
    {
        dogAnimator.SetBool(animationToBoolMapping[currentAnimation], false);
        dogAnimator.SetBool(animationToBoolMapping[newAnimation], true);
        currentAnimation = newAnimation;
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
