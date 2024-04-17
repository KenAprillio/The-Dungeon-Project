using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KreditInteract : MonoBehaviour, IInteractable
{
    [Header("Interactor Stuff")]
    [SerializeField] private string _prompt;
    [SerializeField] private bool _isEnabled;

    public string InteractionPrompt => _prompt;
    public bool isEnabled => _isEnabled;

    public int KreditValue;

    public bool Interact(Interactor interactor)
    {
        return true;
    }
}
