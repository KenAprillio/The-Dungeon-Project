using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public interface IInteractable
{
    public string InteractionPrompt { get; }
    public bool isEnabled { get; }

    public bool Interact(Interactor interactor);
}
