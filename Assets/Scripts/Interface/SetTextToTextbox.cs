using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetTextToTextbox : MonoBehaviour
{
    /*[TextArea(2, 3)]
    [SerializeField] private string _message = "Press BUTTONPROMPT to interact";*/

    public static SetTextToTextbox Instance;

    [Header("Setup for Sprites")]
    [SerializeField] private ListOfTmpSpriteAssets _listOfTmpSpriteAssets;
    [SerializeField] private DeviceType _deviceType;

    private PlayerInput _playerInput;
    private TMP_Text _textBox;

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

        _playerInput = new PlayerInput();
        _textBox = GetComponent<TMP_Text>();
    }

    [ContextMenu("Set Text")]
    public string SetText(string message)
    {
        if ((int)_deviceType > _listOfTmpSpriteAssets.SpriteAssets.Count-1)
        {
            Debug.Log($"Missing Sprite Asset for {_deviceType}");
            return null;
        }

        string outMessage = CompleteTextWithButtonPromptSprite.ReadAndReplaceBinding(
            message,
            _playerInput.Player.Interact.bindings[(int)_deviceType],
            _listOfTmpSpriteAssets.SpriteAssets[(int)_deviceType]);

        return outMessage;
    }

    public string SetSecondText(string message)
    {
        if ((int)_deviceType > _listOfTmpSpriteAssets.SpriteAssets.Count - 1)
        {
            Debug.Log($"Missing Sprite Asset for {_deviceType}");
            return null;
        }

        string outMessage = CompleteTextWithButtonPromptSprite.ReadAndReplaceBinding(
            message,
            _playerInput.Player.SecondInteract.bindings[(int)_deviceType],
            _listOfTmpSpriteAssets.SpriteAssets[(int)_deviceType]);

        return outMessage;
    }

    private enum DeviceType
    {
        Keyboard = 0,
        Gamepad = 1
    }

}
