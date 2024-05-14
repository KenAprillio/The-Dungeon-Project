using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISecondInteractable : IInteractable
{
    public string SecondInteractionPrompt { get; }
    public bool SecondInteract(Interactor interactor);
}
