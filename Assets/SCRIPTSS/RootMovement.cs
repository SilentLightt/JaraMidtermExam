using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f; // Movement speed multiplier

    private void Update()
    {
        // Get player input (e.g., forward or backward movement)
        float vertical = Input.GetAxis("Vertical");

        // Set the "Walk" or "Run" parameter in the Animator based on input
        animator.SetFloat("Speed", Mathf.Abs(vertical));

        // Move the character forward or backward based on input and Root Motion
        if (vertical != 0)
        {
            transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);
        }
    }
}

