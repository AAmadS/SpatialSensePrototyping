using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
     public Transform playerCamera; // Assign the player's camera (usually the VR headset camera)

    private void Update()
    {
        // Make the Canvas face the player's camera
        transform.LookAt(transform.position + playerCamera.forward);
    }
}
