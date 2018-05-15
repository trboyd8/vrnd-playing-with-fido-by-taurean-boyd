using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            other.transform.position = new Vector3(0, 1, 0);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
