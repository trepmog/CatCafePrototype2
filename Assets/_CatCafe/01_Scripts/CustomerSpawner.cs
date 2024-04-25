using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public ConvoView convoView;
    private Transform spawnPoint;
    private Transform destination;

    void Start()
    {
        // Get the location of the Front Door
        GameObject entrance = GameObject.FindWithTag("FrontDoor");
        // Set the spawn point to be Front Door location
        spawnPoint = entrance.transform;
        // Spawn a customer one time
        SpawnCustomer();
    }

    void Awake()
    {
        // Get the location of the chair
        GameObject chair = GameObject.FindGameObjectWithTag("Chair");
        destination = chair.transform;
    }

    void SpawnCustomer()
    {
        // Create a customer and give them the destination of the chair
        GameObject customerObject = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        NavMeshAgent agent = customerObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);

        // Subscribe to ConvoView to allow conversation UI to open when player interacts with it
        Customer customer = customerObject.GetComponent<Customer>();
        if (customer != null && convoView != null)
        {
            customer.OnInteract += convoView.ShowConvoUI;  // Subscribe here
        }
    }
}
