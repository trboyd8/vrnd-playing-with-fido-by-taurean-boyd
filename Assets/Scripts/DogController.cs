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
    private bool isFetching;
    private GameObject objectToFetch;
    private float originalStoppingDistance;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        dogAnimator = this.GetComponentInChildren<Animator>();
        loveMeter = this.GetComponentInChildren<LoveMeter>();
        currentAnimation = "Idle";
        isFetching = false;
        originalStoppingDistance = agent.stoppingDistance;

        animationToBoolMapping = new Dictionary<string, string>();
        animationToBoolMapping.Add("Speak", "IsSpeaking");
        animationToBoolMapping.Add("Sit", "IsSitting");
        animationToBoolMapping.Add("Lay", "IsLaying");
        animationToBoolMapping.Add("Dead", "IsDead");
        animationToBoolMapping.Add("Walk", "IsWalking");
        animationToBoolMapping.Add("Idle", "IsIdle");
        animationToBoolMapping.Add("Petting", "IsPetting");
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

        if (isFetching)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, objectToFetch.transform.position, 2.0f * Time.deltaTime, 0.0f));
            transform.position = Vector3.MoveTowards(transform.position, objectToFetch.transform.position, 2.0f * Time.deltaTime);
        }
    }

    public void Speak()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Speak");
            loveMeter.UpdateLove(0.05f);
        }
    }

    public void Dead()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Dead");
            loveMeter.UpdateLove(0.05f);
        }
    }

    public void Sit()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Sit");
            loveMeter.UpdateLove(0.05f);
        }
    }

    public void Lay()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Lay");
            loveMeter.UpdateLove(0.05f);
        }
    }
    
    public void Stop()
    {
        if (!currentAnimation.Equals("Walk"))
        {
            SwitchAnimation("Idle");
        }
    }

    public void BeginPetting()
    {
        if (!currentAnimation.Equals("Walk") && !currentAnimation.Equals("Petting"))
        {
            SwitchAnimation("Petting");
            loveMeter.UpdateLove(0.1f);
        }
    }

    public void EndPetting()
    {
        if (currentAnimation.Equals("Petting"))
        {
            SwitchAnimation("Idle");
        }
    }

    public void Navigate(Vector3 position)
    {
        SwitchAnimation("Walk");
        agent.SetDestination(position);
    }

    public void BeginFetch(GameObject fetchableObject)
    {
        if (!this.isFetching)
        {
            this.isFetching = true;
            originalPosition = this.transform;
            SwitchAnimation("Walk");
            objectToFetch = fetchableObject;
        }
    }

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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Pickup"))
    //    {
    //        // Pick up object
    //            // Make dog parent of object
    //            // Set to mouth position
    //    }
    //}
}
