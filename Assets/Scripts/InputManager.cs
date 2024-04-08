using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInput _playerInput;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerInput = new PlayerInput();
    }

    public event Action onInteracting;
    private void OnInteract()
    {
        if(onInteracting != null)
            onInteracting();
    }
}
