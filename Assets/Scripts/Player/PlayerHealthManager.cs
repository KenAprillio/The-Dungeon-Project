using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Player State Machine Reference")]
    [SerializeField] private PlayerStateMachine _player;

    [Header("Player Health and Defense")]
    public float MaxHealthPoints;
    public float CurrentHealthPoints;
    public float CurrentShieldPoints;
    public float MaxShieldPoints;

    [Header("UI Elements")]
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private Slider _playerShield;
    [SerializeField] private TMP_Text _playerShieldText;
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
        UpdateHealthbar();
    }

    public void UpdateHealthbar()
    {
        _playerHealth.value = (MaxHealthPoints == 0) ? 0 : CurrentHealthPoints / MaxHealthPoints;
        _playerShield.value = (MaxShieldPoints == 0) ? 0 : CurrentShieldPoints / MaxShieldPoints;
        _playerHealthText.text = CurrentHealthPoints + " / " + MaxHealthPoints;
        _playerShieldText.text = CurrentShieldPoints + " / " + MaxShieldPoints;
    }

    public void PlayerHit(float damage)
    {
        CurrentHealthPoints -= damage;
        UpdateHealthbar();

        _player.IsHit = true;

        if (CurrentHealthPoints <= 0)
            PlayerDead();
    }

    public void PlayerDead()
    {
        this.gameObject.SetActive(false);
    }
}
