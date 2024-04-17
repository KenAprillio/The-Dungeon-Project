using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildablesInteract : MonoBehaviour, IInteractable
{
    [Header("Interactor Stuff")]
    [SerializeField] private string _prompt;
    [SerializeField] private bool _isEnabled;

    public string InteractionPrompt => _prompt;
    public bool isEnabled => _isEnabled;


    [Header("Gameobject References")]
    [SerializeField] private GameObject _buildablesChooser;
    [SerializeField] private Animator _canvasAnimator;

    private TimeScaler _timeScaler;

    private void Start()
    {
        _timeScaler = TimeScaler.Instance;
    }

    public bool Interact(Interactor interactor)
    {
        _timeScaler.ActivateBuildablesUI();
        _buildablesChooser.GetComponent<BuildablesChooser>().BuildablesLocation = transform.parent.gameObject;
        Debug.Log("Choose a Buildables!");

        return true;
    }
}
