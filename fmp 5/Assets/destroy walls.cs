using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroywalls : MonoBehaviour
{
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            Destroy(Door.gameObject);
        }
    }
}
