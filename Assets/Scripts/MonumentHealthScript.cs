using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonumentHealthScript : MonoBehaviour
{
    [Header("Monument Health")]
    public float _maxHealthPoints;
    public float CurrentHealthPoints;

    [Header("UI Elements")]
    [SerializeField] private Image _healthBar;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = _maxHealthPoints;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealthPoints -= damage;
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        _healthBar.fillAmount = CurrentHealthPoints / _maxHealthPoints;
    }
}
