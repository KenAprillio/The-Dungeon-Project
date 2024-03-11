using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [Header("Enemy Health and Defense")]
    public float MaxHealthPoints;
    public float CurrentHealthPoints;

    /*[Header("UI Elements")]
    private Slider _playerHealth;
    private Slider _playerShield;*/

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
    }

    public void EnemyHit(float damage)
    {
        CurrentHealthPoints -= damage;
    }
}
