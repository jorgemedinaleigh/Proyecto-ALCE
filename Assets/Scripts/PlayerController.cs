using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController CharacterController;
    [SerializeField] private float walkSpeed = 1f;

    private void Update()
    {
        HandleWalking();
    }

    private void HandleWalking()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.right * x + transform.forward * z;

        CharacterController.Move(movementDirection * walkSpeed * Time.deltaTime);
    }
}
