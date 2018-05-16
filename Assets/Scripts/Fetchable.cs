using UnityEngine;

public class Fetchable : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    public bool HasBeenThrown;
    public DogController dogController;

	// Use this for initialization
	void Start ()
    {
        thisRigidbody = this.GetComponent<Rigidbody>();
        HasBeenThrown = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (HasBeenThrown && collision.gameObject.CompareTag("Ground"))
        {
            dogController.BeginFetch(this.gameObject);
            HasBeenThrown = false;
        }
    }
}
