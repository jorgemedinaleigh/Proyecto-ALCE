using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController CharacterController;

    [Header("Player Stats")]
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float playerReach = 2f;

    private Interactable currentInteractable;

    private void Update()
    {
        HandleWalking();
        CheckInteraction();
        InteractWithObject();
    }

    private void HandleWalking()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.right * x + transform.forward * z;

        CharacterController.Move(movementDirection * walkSpeed * Time.deltaTime);
    }

    private void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if(Physics.Raycast(ray, out hit, playerReach))
        {
            if(hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if(newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        HUDController.instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable()
    {
        HUDController.instance.DisableInteractionText();

        if(currentInteractable)
        {
            currentInteractable = null;
        }
    }

    private void InteractWithObject()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
