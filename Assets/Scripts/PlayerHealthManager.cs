using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Player State Machine Reference")]
    [SerializeField] private PlayerStateMachine _player;

    [Header("Player Health and Defense")]
    public float MaxHealthPoints;
    public float CurrentHealthPoints;
    public float ShieldPoints;

    [Header("UI Elements")]
    private Slider _playerHealth;
    private Slider _playerShield;
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
    }

    public void PlayerHit(float damage)
    {
        CurrentHealthPoints -= damage;
        _player.IsHit = true;
    }


}
