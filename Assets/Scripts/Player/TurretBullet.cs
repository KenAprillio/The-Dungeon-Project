using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private EnemyHealthManager _healthManager;
    [SerializeField] private bool _isProjectile = false;
    private float _damage;
    public float Damage { set { _damage = value; } }

    private void OnEnable()
    {
        // Starts die countdown if its a projectile
        if (_isProjectile)
        {
            StartCoroutine("DieOvertime");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Blackboard enemyTree = other.GetComponentInChildren<Blackboard>();

            FloatVariable damageDealt = enemyTree.GetVariable<FloatVariable>("damageReceived");
            BoolVariable hittingEnemy = enemyTree.GetVariable<BoolVariable>("isHit");
            damageDealt.Value =_damage;
            hittingEnemy.Value = true;
        }
    }

    public IEnumerator DieOvertime()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);

    }
}
