using UnityEngine;

public class Item : MonoBehaviour
{

    // Do item specific responses
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f,1f,0f,0.8f); // green
            // Debug.Log("Item touched");
        }
        else if (other.CompareTag("Customer"))
        {
            // play animation based on the collistion detection
        }
    }

    protected virtual void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            // Debug.Log("Item left alone");
        }
    }
}
