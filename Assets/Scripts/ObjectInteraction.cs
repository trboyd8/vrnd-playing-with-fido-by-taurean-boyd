using UnityEngine;

public class ObjectInteraction : MonoBehaviour {

    public OVRInput.Controller thisController;
    private float throwForce = 2f;

    void OnTriggerStay(Collider other)
    {
        Debug.Log("You hit an object");
        if (other.CompareTag("Grabbable"))
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, thisController))
            {
                GrabObject(other);
            }
            else if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, thisController))
            {
                ThrowObject(other);
            }
        }

        if (other.CompareTag("Dog"))
        {
            // Implement Petting
        }
    }

    private void GrabObject(Collider other)
    {
        other.transform.SetParent(gameObject.transform);
        other.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ThrowObject(Collider other)
    {
        other.transform.SetParent(null);
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = OVRInput.GetLocalControllerVelocity(thisController) * throwForce;
        rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(thisController);
    }
}
