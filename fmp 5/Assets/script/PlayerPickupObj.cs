using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObj : MonoBehaviour
{

    [Header("Pick variables")]
    public GameObject currentlyHeldObj;
    public Transform holdingPoint;
    public float pickupRange;
    public LayerMask pickupMask;
    public bool isHoldingObj;
    RaycastHit hit;

    [Header("Throwing")]
    public float throwingForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F) && !isHoldingObj)
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickupRange, pickupMask))
            {
                currentlyHeldObj = hit.transform.gameObject; 

                Pickup(currentlyHeldObj);
            }
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            Drop(currentlyHeldObj);
        }

        if(isHoldingObj)
        {
            moveObjToPoint();
        }
    }

    void moveObjToPoint()
    {
        hit.transform.position = holdingPoint.position;
    }

    void Pickup(GameObject currentObj)
    {
        Debug.Log("pickup");

        // Disable the rigidbody
        currentObj.GetComponent<Rigidbody>().useGravity = false;

        // move the object to the holding point
        currentObj.transform.position = holdingPoint.position;

        isHoldingObj = true;

    }

    void Drop(GameObject currentObj)
    {
        Debug.Log("drop");

        // enabled the rigidbody
        currentObj.GetComponent<Rigidbody>().useGravity = true;

        currentObj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwingForce, ForceMode.Impulse);

        currentlyHeldObj = null;

        isHoldingObj = false;
    }

}
