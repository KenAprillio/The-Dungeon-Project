using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MonumentHealthScript : MonoBehaviour
{
    [Header("Monument Health")]
    public float _maxHealthPoints;
    public float CurrentHealthPoints;

    [Header("UI Elements")]
    [SerializeField] private Image _healthBar;

    [Header("Narrative Stuff")]
    [SerializeField] private GameObject _storyCamera;
    [SerializeField] private GameObject _buildablesCollection;

    [Header("Fix Stuff")]
    [SerializeField] private int _fixCounter;
    [SerializeField] private float _healthFixAmount;
    public int BaseFixCost;
    [SerializeField] private int _fixCostAdder;
    public int FinalFixCost;

    [Header("Player Reference")]
    [SerializeField] private PlayerStateMachine _player;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = _maxHealthPoints;
        UpdateHealthbar();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealthPoints -= damage;
        UpdateHealthbar();

        if (CurrentHealthPoints <= 0)
        {
            TimeScaler.Instance.ActivateLosePanel();
            _player.enabled = false;
        }
    }

    private void UpdateHealthbar()
    {
        _healthBar.fillAmount = CurrentHealthPoints / _maxHealthPoints;
    }

    public bool MonumentHealthFix()
    {
        float totalHealth = CurrentHealthPoints + _healthFixAmount;
        if (CurrentHealthPoints == _maxHealthPoints)
        {
            return false;
        } 
        else if (totalHealth > _maxHealthPoints)
        {
            CurrentHealthPoints = _maxHealthPoints;
            UpdateHealthbar();
            _fixCounter++;
            return true;
        }
        else
        {
            CurrentHealthPoints = totalHealth;
            UpdateHealthbar();
            _fixCounter++;
            return true;
        }
    }

    public void ActivateStoryCamera()
    {
        _storyCamera.SetActive(true);
    }

    public void DeactivateStoryCamera()
    {
        _storyCamera.SetActive(false);
    }

    public void ActivateBuildables()
    {
        _buildablesCollection.SetActive(true);
    }

    public int UpdateCost()
    {
        FinalFixCost = BaseFixCost + (_fixCounter * _fixCostAdder);
        return FinalFixCost;
    }
}
