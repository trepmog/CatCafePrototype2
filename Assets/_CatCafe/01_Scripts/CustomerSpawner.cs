using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public ConvoView convoView;
    private Transform spawnPoint;
    private Transform destination;
    private GameObject entrance;
	private DataLoader dataManager;

	private static int nextCharacterSpawnIndex = 0;

	void Start()
    {
		dataManager = GameManager.Instance.GetComponent<DataLoader>();
		// Get the location of the Front Door
		entrance = GameObject.FindWithTag("FrontDoor");
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
		CustomerEntry customerEntry = dataManager.customerDatabase.customers[nextCharacterSpawnIndex];
		string prefabName = $"Assets/_CatCafe/03_Prefabs/Customer{customerEntry.name}.prefab";
		GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabName);
		if ( prefab == null )
		{
			prefab = customerPrefab; // default
			Debug.LogError( "Missing prefab " + prefabName );
		}

		// Create a customer and give them the destination of the chair
		GameObject customerObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        NavMeshAgent agent = customerObject.GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
        agent.SetDestination(destination.position);

        // Subscribe to ConvoView to allow conversation UI to open when player interacts with it
        Customer customer = customerObject.GetComponent<Customer>();
        if (customer != null && convoView != null)
        {
            customer.OnInteract += convoView.ShowConvoUI;  // Subscribe here
        }
    }

    public void MoveToExit(Customer customer)
    {
        destination = entrance.transform;
        NavMeshAgent agent = customer.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);

        //Debug.Log("destination and navmesh agent set for despawn");
    }
}
