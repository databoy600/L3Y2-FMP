using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
   [SerializeField]
   Camera playerCamera;

   [SerializeField]
   TextMeshProUGUI interactionText;

   [SerializeField]
   float interactionDistance = 5f;

   IInteractable currentTargetedInteractable;

   public void Update()
   {
      UpdateCurrentInteractable();

      UpdateInteractionText();

      CheckForInteractionInput();
   }

// where it would be in the middle of the screen
   void UpdateCurrentInteractable()
   {
      var ray = playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
       // for the distance for the Raycast
      Physics.Raycast(ray, out var hit, interactionDistance);

      currentTargetedInteractable = hit.collider?.GetComponent<IInteractable>();
   } 


   void UpdateInteractionText()
   {
     if (currentTargetedInteractable == null)
     {
        interactionText.text = string.Empty;
        return;
     }

      interactionText.text = currentTargetedInteractable.InteractMessage;
   }

   void CheckForInteractionInput()
   {
    // have to press E to interact
     if (Input.GetKeyDown(KeyCode.E) && currentTargetedInteractable != null)
      {
        currentTargetedInteractable.Interact();
      }
   }
}
