using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public interface IInteractable
    {
        public string InteractMessage { get; }
        public void Interact();
    }
   
}
