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

    [Header("Enemy Flash Damage")]
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Material[] _materials;
    [SerializeField] private float _blinkIntensity;
    [SerializeField] private float _blinkDuration;
    [SerializeField] private float _blinkTimer;
    private Coroutine _damageFlashCoroutine; 


    private EnemyPooler _enemyPooler;
    private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _materials = new Material[_skinnedMeshRenderer.materials.Length];

        for (int i = 0; i < _skinnedMeshRenderer.materials.Length; i++)
        {
            _materials[i] = _skinnedMeshRenderer.materials[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        CurrentHealthPoints = MaxHealthPoints;
        _enemyPooler = EnemyPooler.Instance;
        _enemySpawner = EnemySpawner.Instance;
        

        Blackboard tree = GetComponentInChildren<Blackboard>();

        TransformVariable mainObjective = tree.GetVariable<TransformVariable>("mainObjective");
        mainObjective.Value = GameObject.FindGameObjectWithTag("MainObjective").GetComponent<Transform>();

        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void Update()
    {
        _healthbarCanvas.transform.LookAt(_healthbar.transform.position + _camera.transform.forward);
    }

    private void OnEnable()
    {
        CurrentHealthPoints = MaxHealthPoints;
        UpdateHealthbar();

        SetFlashColor();
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
        CallDamageFlash();

        if (CurrentHealthPoints <= 0)
            EnemyDie();
    }

    public void CallDamageFlash()
    {
        _damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        // Sets the color
        SetFlashColor();

        // Lerp the flash amount
        float currentFlashAmount = 0;
        float elapsedTime = 0f;
        while (elapsedTime < _blinkDuration)
        {
            // Iterate elapsedTime
            elapsedTime += Time.deltaTime;

            // Lerp the flash amount
            currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime / _blinkDuration));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        // Sets the color
        for(int i = 0; i < _materials.Length; i++)
        {
            _materials[i].color = Color.white;
        }
    }

    private void SetFlashAmount(float amount)
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            float intensity = (amount * _blinkIntensity) + 1f;
            _materials[i].color = Color.white * intensity;
        }
    }

    public void EnemyDie()
    {
        gameObject.SetActive(false);
        _enemySpawner.RemoveEnemiesFromWave(gameObject);

        // Randomly drops kredits
        bool dropKredits = Random.value < .5f;
        if (dropKredits)
            _enemyPooler.SpawnFromPool("Kredits", transform.position, Quaternion.identity);

    }

    public void UpdateHealthbar()
    {
        _healthbar.fillAmount = CurrentHealthPoints / MaxHealthPoints;
    }
}
