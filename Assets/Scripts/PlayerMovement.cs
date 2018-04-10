using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private LineRenderer laser;
    private Vector3 teleportLocation;
    private int laserLength;
    private int groundRayLength;
    private bool canTeleport;
    private float nudgeAmount;

    public GameObject TeleportObject;
    public GameObject Player;
    public GameObject Dog;

    // Use this for initialization
    void Start ()
    {
        laser = this.GetComponentInChildren<LineRenderer>(true);
        laserLength = 5;
        groundRayLength = 1;
        canTeleport = false;
        nudgeAmount = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            laser.gameObject.SetActive(true);
            TeleportObject.SetActive(true);
            laser.SetPosition(0, gameObject.transform.position);

            // Test to see if we can teleport forward
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, transform.forward, out hit, laserLength))
            {
                canTeleport = true;
                teleportLocation = hit.point;
                laser.SetPosition(1, teleportLocation);
                TeleportObject.transform.position = teleportLocation;
            }
            else // do a second test to see if we can teleport down from where we are pointing
            {
                teleportLocation = gameObject.transform.position + transform.forward * laserLength;

                RaycastHit groundHit;
                if (Physics.Raycast(teleportLocation, -Vector3.up, out groundHit, groundRayLength))
                {
                    canTeleport = true;
                    teleportLocation = new Vector3(teleportLocation.x, groundHit.point.y, teleportLocation.z);
                }
                else
                {
                    canTeleport = false;
                }
            }

            if (canTeleport)
            {
                laser.SetPosition(1, teleportLocation);
                TeleportObject.transform.position = teleportLocation;
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
        {
            laser.gameObject.SetActive(false);
            TeleportObject.SetActive(false);

            if (canTeleport)
            {
                Player.transform.position = new Vector3(teleportLocation.x, Player.transform.position.y, teleportLocation.z);
                canTeleport = false;

                // Trigger dog to follow
                DogMovement dogMovement = Dog.GetComponent<DogMovement>();
                dogMovement.Navigate(Player.transform);
            }
        }
	}
}
