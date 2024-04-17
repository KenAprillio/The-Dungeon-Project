using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PayloadHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _payloadName;
    [SerializeField] private Image _payloadHealthbar;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetUpUI(string payloadName)
    {
        _payloadName.text = payloadName;
        _payloadHealthbar.fillAmount = 1;
        gameObject.SetActive(true);

        _animator.SetBool("isActive", true);
    }

    public void UpdateHealthbar(float currentHealth, float maxHealth)
    {
        if (currentHealth > 0)
            _payloadHealthbar.fillAmount = currentHealth / maxHealth;
        else
        {
            _payloadHealthbar.fillAmount = 0;
            StartCoroutine(nameof(WaitBeforeDeactivating));
        }
    }

    IEnumerator WaitBeforeDeactivating()
    {
        yield return new WaitForSeconds(2);
        _animator.SetBool("isActive", false);

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
