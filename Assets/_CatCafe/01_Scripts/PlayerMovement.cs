using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private float _speed = 5.0f;
    private PlayerInteract playerInteract;
    private InputAction move;
    private InputAction interact;
    private Vector3 moveDirection = Vector3.zero;
    private float playerPositionY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        _playerInputManager = new PlayerInputManager();
        playerPositionY = gameObject.transform.position.y;
        playerInteract = GetComponent<PlayerInteract>();
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
        interact.performed -= interaction;
    }

    private void FixedUpdate()
    {
        // Move the player
        _controller.Move(moveDirection * _speed * Time.deltaTime);
    }


    void Update()
    {
        // Handle movement input
        moveDirection = move.ReadValue<Vector3>();  // Translate 2D input to 3D world movement
    }

    private void interaction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerInteract.Interact();
        }
        
    }
}
