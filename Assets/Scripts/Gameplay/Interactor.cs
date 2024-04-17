using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Variables")]
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = .5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    private int _numFound;

    [Header("UI Stuff")]
    [SerializeField] private Animator _canvasAnimator;
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private GameObject _promptGameobject;

    PlayerInput _playerInput;
    SetTextToTextbox _setText;
    TimeScaler _timeScaler;
    [SerializeField] bool _interactionReady;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _setText = SetTextToTextbox.Instance;
        _timeScaler = TimeScaler.Instance;
    }

    private void Update()
    {
        // Create a detector sphere
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        _interactionReady = (_numFound > 0) ? true : false;

        // Check if gameobject found with the targeted layer
        if (_interactionReady)
        {
            if (_colliders[0].GetComponent<IInteractable>() != null && _colliders[0].GetComponent<IInteractable>().isEnabled)
            {
                if (_colliders[0].tag == "Kredit")
                {
                    var interactable = _colliders[0].GetComponent<KreditInteract>();

                    _colliders[0].gameObject.SetActive(false);
                    GetComponent<PlayerHealthManager>().PlayerKredits += interactable.KreditValue;
                    GetComponent<PlayerHealthManager>().UpdateKredits();
                }
                else
                {
                    // Get Interactable Interface from the object
                    var interactable = _colliders[0].GetComponent<IInteractable>();

                    _promptText.text = _setText.SetText(interactable.InteractionPrompt);

                    _promptGameobject.SetActive(true);

                    if (interactable != null && _playerInput.Player.Interact.triggered)
                    {
                        interactable.Interact(this);
                    }
                }
            }
            else
            {
                _promptGameobject.SetActive(false);

                if (_timeScaler.IsBoonsUIActive)
                {
                    _timeScaler.DeactivateBoonsUI();
                }

                if (_timeScaler.IsBuildablesUIActive)
                {
                    _timeScaler.DeactivateBuildablesUI();
                }
            }
        } else
        {
            //Debug.LogWarning("For some reason this is causing the shit");
            _promptGameobject.SetActive(false);

            if (_timeScaler.IsBoonsUIActive)
            {
                _timeScaler.DeactivateBoonsUI();
            }

            if (_timeScaler.IsBuildablesUIActive)
            {
                _timeScaler.DeactivateBuildablesUI();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
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
