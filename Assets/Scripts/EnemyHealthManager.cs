using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    private Camera _camera;

    [Header("Enemy Health and Defense")]
    public float MaxHealthPoints;
    public float CurrentHealthPoints;

    [Header("UI Elements")]
    [SerializeField] private Canvas _healthbarCanvas;
    [SerializeField] private Image _healthbar;

    [Header("Weapon Related Antics")]
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSource;
    [SerializeField] private float _projectileForce;
    public float Damage;

    private EnemyPooler _enemyPooler;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        CurrentHealthPoints = MaxHealthPoints;
        _enemyPooler = EnemyPooler.Instance;

        Blackboard tree = GetComponentInChildren<Blackboard>();

        TransformVariable mainObjective = tree.GetVariable<TransformVariable>("mainObjective");
        mainObjective.Value = GameObject.FindGameObjectWithTag("MainObjective").GetComponent<Transform>();

        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void Update()
    {
        _healthbarCanvas.transform.LookAt(_healthbar.transform.position + _camera.transform.forward);
    }

    public void ThrowPipe()
    {
        GameObject currentProjectile = _enemyPooler.SpawnFromPool("Pipes", _projectileSource.position, transform.rotation);

        currentProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileForce, ForceMode.Impulse);
        currentProjectile.GetComponent<EnemyWeaponCollider>().Damage = Damage;
    }

    public void EnemyHit(float damage)
    {
        CurrentHealthPoints -= damage;
        UpdateHealthbar();

        if(CurrentHealthPoints <= 0)
            gameObject.SetActive(false);
    }

    public void UpdateHealthbar()
    {
        _healthbar.fillAmount = CurrentHealthPoints / MaxHealthPoints;
    }
}
