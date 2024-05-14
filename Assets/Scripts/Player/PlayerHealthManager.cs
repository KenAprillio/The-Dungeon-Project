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
    [SerializeField] private float _percentToRecover;

    [Header("UI Elements")]
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private Slider _playerShield;
    [SerializeField] private TMP_Text _playerShieldText;
    [SerializeField] private TMP_Text _playerKredits;


    [Header("Player Kredits")]
    public int PlayerKredits;
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
        UpdateHealthbar();
    }
    
    public void RecoverPlayer()
    {
        float healTotal = CurrentHealthPoints + ((_percentToRecover / 100) * MaxHealthPoints);
        CurrentHealthPoints = (healTotal > MaxHealthPoints) ? MaxHealthPoints : healTotal;

        if (MaxShieldPoints > 0)
        {
            float healShieldTotal = CurrentShieldPoints + ((_percentToRecover / 100) * MaxShieldPoints);
            CurrentShieldPoints = (healShieldTotal > MaxShieldPoints) ? MaxShieldPoints : healShieldTotal;
        }

        UpdateHealthbar();
    }

    public void ActivatePlayer()
    {
        _player.enabled = true;
    }

    public void DeactivatePlayer()
    {
        _player.enabled = false;
    }

    public void UpdateHealthbar()
    {
        _playerHealth.value = (MaxHealthPoints == 0) ? 0 : CurrentHealthPoints / MaxHealthPoints;
        _playerShield.value = (MaxShieldPoints == 0) ? 0 : CurrentShieldPoints / MaxShieldPoints;
        _playerHealthText.text = CurrentHealthPoints + " / " + MaxHealthPoints;
        _playerShieldText.text = CurrentShieldPoints + " / " + MaxShieldPoints;
    }

    public void UpdateKredits()
    {
        _playerKredits.text = "<sprite=\"kredits\" name=\"kredits_icon\"> " + PlayerKredits;
    }

    public void PlayerHit(float damage)
    {
        if (CurrentShieldPoints > 0)
        {
            CurrentShieldPoints -= damage;
            if (CurrentShieldPoints < 0)
            {
                CurrentShieldPoints = 0;
            }
        }
        else
        {
            CurrentHealthPoints -= damage;
            if (CurrentHealthPoints < 0)
            {
                CurrentShieldPoints = 0;
            }
        }
        UpdateHealthbar();

        _player.IsHit = true;

        if (CurrentHealthPoints <= 0)
            PlayerDead();
    }

    public void PlayerDead()
    {
        //this.gameObject.SetActive(false);
        GetComponent<PlayerStateMachine>().IsDead = true;
        TimeScaler.Instance.ActivateLosePanel();
    }
}
