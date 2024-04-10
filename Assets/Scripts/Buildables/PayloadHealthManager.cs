using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    [Header("Shooting Antics")]
    [SerializeField] private Transform _projectileSource;
    [SerializeField] private float _projectileForce;
    [SerializeField] private float _damage;

    private EnemyPooler _enemyPooler;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
        _enemyPooler = EnemyPooler.Instance;
    }

    public void ShootTurret()
    {
        GameObject currentProjectile = _enemyPooler.SpawnFromPool("Bullet", _projectileSource.position, transform.rotation);

        currentProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileForce, ForceMode.Impulse);
        currentProjectile.GetComponent<TurretBullet>().Damage = _damage;
    }

    public void PayloadHit(float damage)
    {
        CurrentHealthPoints -= damage;
    }
}
