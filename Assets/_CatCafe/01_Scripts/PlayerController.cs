using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Add in player's movement speed
    // [SerializeField] private Rigidbody rb;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private float _speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    private InputAction move;
    private InputAction interact;
    private bool isHoldingItem = false;
    private bool canPickup = false;
    private bool customerItem = false;
    private GameObject item = null;
    private GameObject customer = null;
    private float originalPosition;
    private float playerPositionY = 0.0f;

    private void Awake() {
        _playerInputManager = new PlayerInputManager();
        playerPositionY = gameObject.transform.position.y;
    }

    private void OnEnable() {
        // Enable the player input
        move = _playerInputManager.Player.Move;
        move.Enable();

        // E key and Mouse Left Button
        interact = _playerInputManager.Player.Interact;
        interact.Enable();
        interact.performed += interaction;
    }

    private void OnDisable() {
        // Disable the player input
        move.Disable();
        interact.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector3>();
        // Debug.Log(moveDirection);
    }

    private void FixedUpdate()
    {
        // Move the player
        _controller.Move(moveDirection * _speed * Time.deltaTime);
    }

    // when the key is pressed (E, or left mouse button), check if the item can be picked up (from OnTriggerEnter event)
    private void interaction(InputAction.CallbackContext context){
        // interact with the item
        if (context.performed && !isHoldingItem && canPickup)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.white;
            // item.GetComponent<Rigidbody>().isKinematic = true;
            var tempPosition = new Vector3(gameObject.transform.position.x + 1.5f, gameObject.transform.position.y, gameObject.transform.position.z + -1f);
            originalPosition = item.transform.position.y; // saves the original position of the object
            item.transform.position = tempPosition; // sets the position of the object to your hand position
            item.transform.parent = gameObject.transform; //makes the object become a child of the parent so that it moves with the hands
            gameObject.tag = "PlayerWithItem"; // changes the tag of the player so it won't collide with other items

            isHoldingItem = true;
            // Debug.Log("Item picked up");
        }
        else if(context.performed && isHoldingItem && customerItem)
        {
            var tempPosition = new Vector3(customer.transform.position.x + 1.5f, customer.transform.position.y + 1.5f, customer.transform.position.z + -1f);
            item.transform.position = tempPosition;
            item.transform.parent = customer.transform;
            gameObject.tag = "Player";
            isHoldingItem = false;
            // play animation based on the collistion detection
        }
        // pick up the item when the player is holding an item and presses the E key to drop the item
        else if(context.performed && isHoldingItem)
        {
            item.transform.parent = null; // removes the parent of the object so that it doesn't move with the hands
            item.transform.position = new Vector3(item.transform.position.x, originalPosition, item.transform.position.z); // sets the position of the object to the original position
            // item.GetComponent<Rigidbody>().isKinematic = false; // this makes a cute little dropping "animation" if the item is dropped
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, playerPositionY, gameObject.transform.position.z); // this forces the player to stick on the ground
            gameObject.tag = "Player";

            isHoldingItem = false;
            // Debug.Log("Item dropped");
        }
        else{
            Debug.Log("No item to pick up");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") || other.CompareTag("Cat")) // check if the player can pick up the item or cat
        {
            canPickup = true;
            if(!item || !isHoldingItem) // if item is null, then get the collider item
                item = other.gameObject;
            // Debug.Log("Item can be picked up");
        }
        else if (other.CompareTag("Customer") && isHoldingItem) // check if the player can deliver item to the customer
        {
            customerItem = true;
            customer = other.gameObject;
            // Debug.Log("Item can be given to the customer");
            // item = null; // gives away the right to control the cat
            // play animation based on the collistion detection
        }
    }

    private void OnTriggerExit(Collider other) {
        canPickup = false;
        customerItem = false;
        // Debug.Log("Item can no longer be picked up");
    }

    public bool GetIsHoldingItem()
    {
        return isHoldingItem;
    }

}