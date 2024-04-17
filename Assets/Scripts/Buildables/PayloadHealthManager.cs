using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PayloadHealthManager : MonoBehaviour
{
    [Header("Payload Health and Defense")]
    public float MaxHealthPoints;
    public float CurrentHealthPoints;

    [Header("UI Elements")]
    [SerializeField] private PayloadHealthUIManager _payloadHealthUIManager;
    private PayloadHealthUI _assignedPayloadHealthUI;

    [Header("Shooting Antics")]
    [SerializeField] private Transform _projectileSource;
    [SerializeField] private float _projectileForce;
    [SerializeField] private float _damage;

    [Header("Interactable Variable")]
    [SerializeField] private GameObject _interactCube;

    private EnemyPooler _enemyPooler;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealthPoints = MaxHealthPoints;
        _enemyPooler = EnemyPooler.Instance;
    }

    private void OnEnable()
    {
        CurrentHealthPoints = MaxHealthPoints;
        SetUpHealthUI();
    }

    // Setting up payload health UI
    public void SetUpHealthUI()
    {
        // Gets list of available health UIs
        List<GameObject> healthUIList = _payloadHealthUIManager.PayloadHealthUIList;

        // Search in the list for one that is not active
        foreach (GameObject healthUI in healthUIList)
        {
            // if found one that is not active, then assign that UI to this payload
            if (!healthUI.activeSelf)
            {
                _assignedPayloadHealthUI = healthUI.GetComponent<PayloadHealthUI>();
                break;
            }
        }

        // Set up the name and value of the UI
        _assignedPayloadHealthUI.SetUpUI(name);
    }

    // The name describes itself
    public void ShootTurret()
    {
        // Spawn bullet from the pool of object
        GameObject currentProjectile = _enemyPooler.SpawnFromPool("Bullets", _projectileSource.position, _projectileSource.rotation);

        // Give force to the bullet and add damage
        currentProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileForce, ForceMode.Impulse);
        currentProjectile.GetComponent<TurretBullet>().Damage = _damage;
    }

    // This function is called when the payload is hit
    public void PayloadHit(float damage)
    {
        CurrentHealthPoints -= damage;
        Debug.Log("Im Hit!" + damage);

        // Updates the healthbar UI
        _assignedPayloadHealthUI.UpdateHealthbar(CurrentHealthPoints, MaxHealthPoints);

        if (CurrentHealthPoints <= 0)
        {
            // Calls the dead method
            gameObject.SetActive(false);
            _assignedPayloadHealthUI = null;

            _interactCube.SetActive(true);
        }
    }
}
