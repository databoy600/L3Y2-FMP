using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupGameObjects : MonoBehaviour
{
    public GameObject Cube;
    public Transform hand;
    // Start is called before the first frame update
    void Start()
    {
        Cube.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Drop();
        }
    }

    void Drop()
    {
        hand.DetachChildren();
        Cube.transform.eulerAngles = new Vector3(Cube.transform.position.x, Cube.transform.position.z, Cube.transform.position.y);
        //Cube.GetComponent<Rigidbody>().isKinematic = false;
        Cube.GetComponent<Rigidbody>().useGravity = true;
        Cube.GetComponent<BoxCollider>().enabled = true;
    }
    void Pickup()
    {
        //Cube.GetComponent<Rigidbody>().isKinematic = true;

        Cube.GetComponent<Rigidbody>().useGravity = false;

        Cube.transform.position = hand.transform.position;
        Cube.transform.rotation = hand.transform.rotation;

        Cube.GetComponent<BoxCollider>().enabled = false;

        Cube.transform.SetParent(hand);
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                Pickup();
            }
        }
    }
    
}
