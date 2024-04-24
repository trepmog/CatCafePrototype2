
using UnityEngine;

public class Customer : NavMeshCustomer
{
    private bool playerLeft = false;
    private bool hasCat = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        
        // make the customer go back to the door if the cat has been delivered
        // if (playerLeft && hasCat)
        // {
        //     agent.isStopped = false;
        //     target = GameObject.FindWithTag("Door").transform;
        //     agent.SetDestination(target.position);
        //     playerLeft = false;
        // }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cat"))
        {
            hasCat = true;
            Debug.Log("Cat entered the customer");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLeft = true;
            Debug.Log("Player left the customer");
        }
    }

}
