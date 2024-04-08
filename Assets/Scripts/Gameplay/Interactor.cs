using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = .5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private GameObject _promptGameobject;

    PlayerInput _playerInput;
    SetTextToTextbox _setText;
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _setText = SetTextToTextbox.Instance;
    }

    private void Update()
    {
        // Create a detector sphere
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        // Check if gameobject found with the targeted layer
        if(_numFound > 0)
        {
            if (_colliders[0].GetComponent<IInteractable>() != null && _colliders[0].GetComponent<IInteractable>().isEnabled)
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
            else
            {
                _promptGameobject.SetActive(false);
            }
        } else
        {
            _promptGameobject.SetActive(false);

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
