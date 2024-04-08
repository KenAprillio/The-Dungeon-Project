using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _collectibleBoon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _collectibleBoon.SetActive(true);
            Destroy(gameObject);
        }
    }
}
