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
        StartCoroutine("DieOvertime");

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (_isProjectile)
            {
                other.GetComponent<PlayerHealthManager>().PlayerHit(_damage);
                gameObject.SetActive(false);
            }
            else
            {
                other.GetComponent<PlayerHealthManager>().PlayerHit(_healthManager.Damage);
            }
            Debug.Log("Damaged Player!!");
        }
    }

    public IEnumerator DieOvertime()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);

    }
}
