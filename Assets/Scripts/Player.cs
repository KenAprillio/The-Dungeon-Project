using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput input = null;
    CharacterController cc;
    Animator animator;


    Vector2 currentMovement;
    Vector2 currentInputVector;
    Vector2 smoothInputVelocity;


    bool movementPressed;

    [Header("Movement Variables")]
    [SerializeField] float movementSmoothing = .2f;
    public float moveSpeed;
    public float rotateSpeed;

    private void Awake()
    {
        // GIVING VALUE TO PLAYER INPUT VARIABLE
        input = new PlayerInput();
        
        // GET PLAYER NEEDED COMPONENTS
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CameraRelativeMovement();
    }

    private void FixedUpdate()
    {
    }

    #region Movement Script
    void CameraRelativeMovement()
    {
        // Smoothing Movement Speed
        currentInputVector = Vector2.SmoothDamp(currentInputVector, currentMovement, ref smoothInputVelocity, movementSmoothing);

        // Get Player Input Value
        float playerVecticalInput = currentInputVector.y;
        float playerHorizontalInput = currentInputVector.x;

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
        Debug.Log(cameraRelativeMovement);

        // IT IS THE WALKING BIT
        cc.SimpleMove((cameraRelativeMovement * moveSpeed));

        // CONDITIONS WHEN MOVEMENT IS PRESSED AND NOT PRESSED
        if (currentMovement != Vector2.zero)
        {
            // SET CHARACTER ROTATION BASED ON DIRECTIONS HEADED
            Quaternion toRotation = Quaternion.LookRotation(cameraRelativeMovement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, (rotateSpeed * 100) * Time.fixedDeltaTime);

            // TRIGGER ANIMATION WHEN WALKING
            animator.SetFloat("isWalking", 1);
        } else
        {
            // TRIGGER ANIMATION WHEN WALKING
            animator.SetFloat("isWalking", 0);
        }

    }
    #endregion

    #region InputSystem

    // ENABLE PLAYER INPUT WHEN GAMEOBJECT IS ENABLED
    void OnEnable()
    {
        input.Enable();

        // SUBSCRIBE TO ACTION INPUT
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    // DISABLE PLAYER INPUT WHEN GAMEOBJECT IS DISABLED
    void OnDisable()
    {
        input.Disable();

        // UNSUBSCRIBE TO ACTION INPUT
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
    }

    // METHOD TO BE CALLED WHEN PLAYER PROVIDES INPUT
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        currentMovement = value.ReadValue<Vector2>();
    }

    // METHOD TO BE CALLED WHEN PLAYER RELEASES INPUT
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        currentMovement = Vector2.zero;
    }
    #endregion
}
