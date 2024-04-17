using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonCollectible : MonoBehaviour, IInteractable
{
    
    [SerializeField] private string _prompt;
    [SerializeField] private bool _isEnabled;

    public bool isEnabled => _isEnabled;
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject _boonChooser;
    private TimeScaler _timeScaler;

    private void Start()
    {
        _timeScaler = TimeScaler.Instance;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Choose a boon!");

        _timeScaler.ActivateBoonsUI();
        //gameObject.SetActive(false);

        return true;
    }
}

    
