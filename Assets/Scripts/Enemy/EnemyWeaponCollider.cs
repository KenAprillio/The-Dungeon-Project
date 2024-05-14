using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollider : MonoBehaviour
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

        if (_healthManager != null)
            _damage = _healthManager.Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if collided object is player or the main objective
        if (other.tag == "Player" || other.tag == "MainObjective" || other.tag == "Turret" || other.tag == "Wall")
        {
            if (other.tag == "MainObjective")
            {
                _damage += 10;
            }

            // Checks if THIS is a projectile
            if (_isProjectile)
            {
                // If projectile, apply damage to target and set gameobject false when hit

                if (other.tag == "MainObjective")
                {
                    other.GetComponent<MonumentHealthScript>().TakeDamage(_damage);
                }
                else if (other.tag == "Turret" || other.tag == "Wall")
                {
                    other.GetComponent<PayloadHealthManager>().PayloadHit(_damage);
                }
                else
                {
                    other.GetComponent<PlayerHealthManager>().PlayerHit(_damage);
                }
                gameObject.SetActive(false);
            }
            else
            {
                // If not projectile, just apply damage

                if (other.tag == "MainObjective")
                    other.GetComponent<MonumentHealthScript>().TakeDamage(_damage);
                else if (other.tag == "Turret" || other.tag == "Wall")
                {
                    other.GetComponent<PayloadHealthManager>().PayloadHit(_damage);
                    Debug.Log("Damaged Player!!");

                }
                else
                    other.GetComponent<PlayerHealthManager>().PlayerHit(_damage);
            }
        }
    }

    

    // Set gameobject false in 5 seconds after projectile is thrown
    public IEnumerator DieOvertime()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);

    }
}
