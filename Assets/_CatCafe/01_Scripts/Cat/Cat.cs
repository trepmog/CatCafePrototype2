using UnityEngine;
using System.Collections.Generic;

public class Cat : Item
{
    [SerializeField] public List<Material> materials;
    public enum CatState
    {
        IDLE,
        WALK,
        SLEEP,
        EAT,
        PLAY,
        PICKED,
        HAPPY,
        ANGRY,
        HUNGRY
    }

    private CatState state;
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        state = CatState.IDLE;
    }

    private void Update() {
        if (state == CatState.PLAY && !playerController.GetIsHoldingItem())
        {
            // play animation
            gameObject.GetComponent<MeshRenderer>().material = materials[1]; // happy cat material
            // Debug.Log("Cat is playing");
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if(other.CompareTag("PlayArea"))
        {
            state = CatState.PLAY;
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f,0f,1f,0.8f); // magenta
            // Debug.Log("Cat is playing");
            // gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.5f,0.5f,0.5f,0.8f); // grey
        }
        else if (other.CompareTag("Customer"))
        {
            // play animation based on the collistion detection
            // state = CatState.PLAY;
            // gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f,1f,1f,0.8f); // cyan
            gameObject.GetComponent<MeshRenderer>().material = materials[1]; // happy cat material
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(other.CompareTag("PlayArea"))
        {
            state = CatState.IDLE;
            // Debug.Log("Cat is playing");
            gameObject.GetComponent<MeshRenderer>().material = materials[0];
            // gameObject.GetComponent<MeshRenderer>().material.color = new Color(0f,1f,1f,0.8f); // cyan
        }
    }

    public CatState GetState()
    {
        return state;
    }

    public void SetState(CatState newState)
    {
        state = newState;
    }
    
}
