using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private Spawn _spawn;
    [SerializeField] private Material[] _materials;
    private GameObject customer;

    private void Start() {
        customer = GameObject.FindWithTag("Customer");
    }

    private void Update() {
        // check if the customer left with the cat, if so, destroy that gameObject and spawn a new one
        // if (!customer)
        // {
        //     customer = GameObject.FindWithTag("Customer");
        // }
        // else if(customer.GetComponent<Customer>().hasLeft && customer.GetComponent<Customer>().getDestinationReached() && customer.GetComponent<Customer>().hasCat)
        // {
        //     Destroy(customer);
        //     // randomly select a skin for the customer and spawn it
        //     var newMaterial = _materials[Random.Range(0, _materials.Length)];
        //     customer = _spawn.spawnNewObject("Customer");
        //     customer.GetComponentInChildren<Renderer>().material = newMaterial;
        // }
        }
        // Debug.Log("Current customer status: " + customer + " - has left: " + customer.GetComponent<Customer>().hasLeft);
}


