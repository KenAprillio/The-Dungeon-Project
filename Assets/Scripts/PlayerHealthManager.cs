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
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private Slider _playerShield;
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
        _playerHealth.maxValue = MaxHealthPoints;
        _playerHealth.value = CurrentHealthPoints;
    }

    public void PlayerHit(float damage)
    {
        CurrentHealthPoints -= damage;
        _playerHealth.value = CurrentHealthPoints;

        _player.IsHit = true;

        if (CurrentHealthPoints <= 0)
            PlayerDead();
    }

    public void PlayerDead()
    {
        this.gameObject.SetActive(false);
    }
}
