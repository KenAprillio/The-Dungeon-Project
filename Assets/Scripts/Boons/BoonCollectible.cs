using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonCollectible : MonoBehaviour, IInteractable
{
    
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] private GameObject _boonSelector;

    

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Choose a boon!");

        _boonSelector.SetActive(true);
        gameObject.SetActive(false);

        return true;
    }
}

    
