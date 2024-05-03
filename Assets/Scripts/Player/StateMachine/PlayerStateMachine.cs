using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // declare reference variables
    PlayerInput _playerInput;
    Animator _animator;
    Rigidbody _rigidBody;

    // variables to store player input values
    Vector2 _currentMovementInput;
    Vector3 _appliedMovement;
    bool _isMovementPressed;
    bool _isDashPressed = false;
    bool _isDashing = false;
    bool _isAbleToDash = true;
    bool _isAttackPressed = false;
    bool _isAttacking = false;
    [SerializeField] bool _isHit = false;


    // variables responsible for player movements
    [Header("Movements Variables")]
    public float _moveSpeed;
    public float _rotationFactorPerFrame = 1.0f;
    [SerializeField] float _movementSmoothing = .2f;
    Vector3 cameraRelativeDirections;
    Vector2 _currentInputVector;
    Vector2 _smoothInputVelocity;
    public float _dashSpeed;
    float _currentAttack;

    [Header("Attack Combos!")]
    public List<AttackSO> _comboList;
    [HideInInspector] public float _currentDamage;


    // state variables
    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    // getters and setters
    public Animator Animator { get { return _animator; } }
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rigidbody { get { return _rigidBody; } set { _rigidBody = value; } }
    public Vector3 CameraRelativeDirections { get { return cameraRelativeDirections; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }
    public Vector2 CurrentMovementInput { get { return _currentMovementInput; } }
    public float AppliedMovementX { get { return _appliedMovement.x; } set { _appliedMovement.x = value; } }
    public float AppliedMovementY { get { return _appliedMovement.y; } set { _appliedMovement.y = value; } }
    public bool IsDashPressed { get { return _isDashPressed; } }
    public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    public bool IsAbleToDash { get { return _isAbleToDash; } set { _isAbleToDash = value; } }
    public float DashSpeed { get { return _dashSpeed; } }
    public bool IsAttackPressed { get { return _isAttackPressed; } }
    public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    public bool IsHit { get { return _isHit; } set { _isHit = value; } }
    public float CurrentAttack { get { return _currentAttack; } set { _currentAttack = value; } }
    public List<AttackSO> ComboList { get { return _comboList; } }
    public float CurrentDamage { set { _currentDamage = value; } }

    bool _isPaused = false;


    private void Awake()
    {
        // initially set reference variables
        _playerInput = new PlayerInput();
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        // setup state
        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

        // set the player input callbacks
        _playerInput.Player.Movement.started += OnMovementInput;
        _playerInput.Player.Movement.canceled += OnMovementInput;
        _playerInput.Player.Movement.performed += OnMovementInput;
        _playerInput.Player.Dash.started += OnDashInput;
        _playerInput.Player.Dash.canceled += OnDashInput;
        _playerInput.Player.Attack.started += OnAttackInput;
        _playerInput.Player.Attack.canceled += OnAttackInput;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateStates();
        _isMovementPressed = _playerInput.Player.Movement.inProgress;
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdateState();
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        CameraRelativeControls(_appliedMovement);
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
        _rigidBody.MovePosition(transform.position + (cameraRelativeDirections * _moveSpeed * Time.deltaTime));
    }

    IEnumerator DashCooldown()
    {
        _isDashing = false;
        yield return new WaitForSeconds(.5f);
        _isAbleToDash = true;
    }

    public void ResetAttack()
    {
        _isAttacking = false;
        _currentAttack = 0;
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

    #region Control Input Codes
    void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
    }

    void OnDashInput(InputAction.CallbackContext context)
    {
        _isDashPressed = context.ReadValueAsButton();
    }

    void OnAttackInput(InputAction.CallbackContext context)
    {
        _isAttackPressed = context.ReadValueAsButton();
    }

    void OnPause()
    {
        TimeScaler.Instance.PauseGame();
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
        _animator.SetBool("isWalking", false);
    }
    #endregion
}
