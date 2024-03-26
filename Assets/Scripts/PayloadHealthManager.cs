using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayloadHealthManager : MonoBehaviour
{
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

    public void PayloadHit(float damage)
    {
        CurrentHealthPoints -= damage;
    }
}
