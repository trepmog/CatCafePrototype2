using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDespawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with " + other);
        
        if (other.gameObject.CompareTag("Customer"))
        {
            //Debug.Log("Customer Encountered");
            
            if (other.gameObject.GetComponent<Customer>().GetHasCat())
            {
                Debug.Log("Customer Has Cat!");
            }                
        }        
    }
}
