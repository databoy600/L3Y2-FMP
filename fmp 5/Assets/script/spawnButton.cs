using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour, IInteractable
{
    public string InteractMessage => objectInteractMessage;

   [SerializeField]
   GameObject SpawnPrefab;

   [SerializeField]
   string objectInteractMessage;

   void Spawn()
   {
    var SpawnedObject = Instantiate(SpawnPrefab, transform.position + Vector3.up, Quaternion.identity);
     
    var randomSize = Random.Range(0.1f, 1f);
    SpawnedObject.transform.localScale = Vector3.one * randomSize;

    var randomColour = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
     SpawnedObject.GetComponent<MeshRenderer>().material.color = randomColour;
     

   }

   public void Interact()
   {
     Spawn();
   }
}
