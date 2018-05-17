using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator dogAnimator;
    private AudioSource audioSource;
    private LoveMeter loveMeter;
    private GameObject objectToFetch;
    public GameObject player;
    private Dictionary<string, string> animationToBoolMapping;
    private string currentAnimation;
    private bool isFetching;
    private bool isReturning;
    private bool isDancing;
    private float spinSpeed;

    private float originalStoppingDistance;


	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        dogAnimator = this.GetComponentInChildren<Animator>();
        audioSource = this.GetComponent<AudioSource>();
        loveMeter = this.GetComponentInChildren<LoveMeter>();
        currentAnimation = "Idle";
        isFetching = false;
        isDancing = false;
        spinSpeed = 50.0f;
        originalStoppingDistance = agent.stoppingDistance;

        animationToBoolMapping = new Dictionary<string, string>();
        animationToBoolMapping.Add("Speak", "IsSpeaking");
        animationToBoolMapping.Add("Sit", "IsSitting");
        animationToBoolMapping.Add("Lay", "IsLaying");
        animationToBoolMapping.Add("Dead", "IsDead");
        animationToBoolMapping.Add("Walk", "IsWalking");
        animationToBoolMapping.Add("Idle", "IsIdle");
        animationToBoolMapping.Add("Petting", "IsPetting");
        animationToBoolMapping.Add("Jump", "IsJumping");
    }

    void Update()
    {
        if (isDancing)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
        else
        {
            // If we're done walking, go back to idle
            if (agent.velocity != Vector3.zero)
            {
                SwitchAnimation("Walk");
            }
            else if (currentAnimation.Equals("Walk"))
            {
                SwitchAnimation("Idle");
                if (isReturning)
                {
                    objectToFetch.transform.SetParent(null);
                    objectToFetch.GetComponent<Rigidbody>().isKinematic = false;
                    isReturning = false;
                    loveMeter.UpdateLove(0.1f);
                }
            }
        }
    }

    public void Speak()
    {
        if (!currentAnimation.Equals("Walk") && !isDancing)
        {
            audioSource.Play();
            SwitchAnimation("Speak");
            loveMeter.UpdateLove(0.05f);
        }
    }

    public void Dead()
    {
        if (!currentAnimation.Equals("Walk") && !isDancing)
        {
            SwitchAnimation("Dead");
            loveMeter.UpdateLove(0.05f);
        }
    }

    public void Sit()
    {
        if (!currentAnimation.Equals("Walk") && !isDancing)
        {
            SwitchAnimation("Sit");
            loveMeter.UpdateLove(0.05f);
        }
    }

    public void Lay()
    {
        if (!currentAnimation.Equals("Walk") && !isDancing)
        {
            SwitchAnimation("Lay");
            loveMeter.UpdateLove(0.05f);
        }
    }
    
    public void Stop()
    {
        if (!currentAnimation.Equals("Walk") && !isDancing)
        {
            SwitchAnimation("Idle");
        }
    }

    public void BeginPetting()
    {
        if (!currentAnimation.Equals("Walk") && !currentAnimation.Equals("Petting") && !isDancing)
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
        if (!isDancing && !isFetching)
        {
            SwitchAnimation("Walk");
            agent.SetDestination(position);
        }
    }

    public void BeginFetch(GameObject fetchableObject)
    {
        if (!this.isFetching && !isDancing)
        {
            this.isFetching = true;
            SwitchAnimation("Walk");
            objectToFetch = fetchableObject;
            agent.stoppingDistance = 0.2f;
            this.Navigate(objectToFetch.transform.position);
            objectToFetch.GetComponent<Rigidbody>().velocity = Vector3.zero;
            objectToFetch.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            objectToFetch.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void EndFetch()
    {
        if (this.isFetching)
        {
            agent.stoppingDistance = originalStoppingDistance;
            this.Navigate(player.transform.position);
            this.isFetching = false;
            this.isReturning = true;
        }
    }

    public void Dance()
    {
        SwitchAnimation("Jump");
        isDancing = true;
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void SwitchAnimation(string newAnimation)
    {
        if (currentAnimation == "Speak")
        {
            audioSource.Stop();
        }

        dogAnimator.SetBool(animationToBoolMapping[currentAnimation], false);
        dogAnimator.SetBool(animationToBoolMapping[newAnimation], true);
        currentAnimation = newAnimation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            if (this.isFetching && !this.isReturning)
            {
                this.EndFetch();
                other.gameObject.transform.SetParent(this.transform);
            }
        }
    }
}
