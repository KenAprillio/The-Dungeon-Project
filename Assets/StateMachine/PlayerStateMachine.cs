using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // declare reference variables
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    // variables to store player input values
    Vector2 _currentMovementInput;
    //Vector3 _currentMovement;
    Vector3 _appliedMovement;
    bool _isMovementPressed;

    // variables responsible for player movements
    [Header("Movements Variables")]
    public float _moveSpeed;
    public float _rotationFactorPerFrame = 1.0f;
    [SerializeField] float _movementSmoothing = .2f;
    Vector3 cameraRelativeDirections;
    Vector2 _currentInputVector;
    Vector2 _smoothInputVelocity;
    

    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // getters and setters
    public Animator Animator { get { return _animator; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }

    private void Awake()
    {
        // initially set reference variables
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        // set the player input callbacks
        _playerInput.Player.Movement.started += OnMovementInput;
        _playerInput.Player.Movement.canceled += OnMovementInput;
        _playerInput.Player.Movement.performed += OnMovementInput;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        CameraRelativeControls(_currentMovementInput);
        // the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            // creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeDirections, Vector3.up);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void HandleMovement()
    {
        CameraRelativeControls(_appliedMovement);
        // IT IS THE WALKING BIT
        _characterController.SimpleMove((cameraRelativeDirections * _moveSpeed));
    }

    void CameraRelativeControls(Vector3 acceptedInput)
    {
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, acceptedInput, ref _smoothInputVelocity, _movementSmoothing);

        // Get Player Input Value
        float playerVecticalInput = _currentInputVector.y;
        float playerHorizontalInput = _currentInputVector.x;

        // Get Camera Directional Vectors
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        // Create Direction-Relative Input Vector
        Vector3 forwardRelativeVecticalInput = playerVecticalInput * forward;
        Vector3 rightRelativeVecticalInput = playerHorizontalInput * right;

        // Create Camera-Relative Movement
        cameraRelativeDirections = forwardRelativeVecticalInput + rightRelativeVecticalInput;
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    private void OnEnable()
    {
        // enable the character controls action map
        _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        // disable the character controls action map
        _playerInput?.Player.Disable();
    }
}
