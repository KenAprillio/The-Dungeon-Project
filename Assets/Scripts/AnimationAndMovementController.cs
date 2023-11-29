using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    // declare reference variables
    PlayerInput _playerInput;
    CharacterController _characterController;
    Animator _animator;

    // variables to store player input values
    Vector2 _currentMovementInput;
    Vector3 _currentMovement;
    bool _isMovementPressed;

    // variables responsible for player movements
    [Header("Movements Variables")]
    Vector2 _currentInputVector;
    Vector2 _smoothInputVelocity;
    [SerializeField] float _movementSmoothing = .2f;
    public float _moveSpeed;
    public float _rotationFactorPerFrame = 1.0f;


    private void Awake()
    {
        // initially set reference variables
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        // set the player input callbacks
        _playerInput.Player.Movement.started += OnMovementInput;
        _playerInput.Player.Movement.canceled += OnMovementInput;
        _playerInput.Player.Movement.performed += OnMovementInput;
    }

    // handler function to set the player input values
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }


    void HandleAnimation()
    {
        // get parameter values from animator
        bool isWalking = _animator.GetBool("isWalking");

        // starts walking if movement pressed is true and not already walking
        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool("isWalking", true);
        }
        // stops walking if movement pressed is false and already walking
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool("isWalking", false);
        }
    }

    void HandleRotation(Vector3 cameraRelativeMovement)
    {
        // the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        if (_isMovementPressed)
        {
            // creates a new rotation based on where the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeMovement, Vector3.up);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void CameraRelativeMovement()
    {
        // Smoothing Movement Speed
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, _currentMovementInput, ref _smoothInputVelocity, _movementSmoothing);

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
        Vector3 cameraRelativeMovement = forwardRelativeVecticalInput + rightRelativeVecticalInput;


        // IT IS THE WALKING BIT
        _characterController.SimpleMove((cameraRelativeMovement * _moveSpeed));

        HandleRotation(cameraRelativeMovement);
    }

    // Update is called once per frame
    void Update()
    {
        CameraRelativeMovement();
        HandleAnimation();
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
