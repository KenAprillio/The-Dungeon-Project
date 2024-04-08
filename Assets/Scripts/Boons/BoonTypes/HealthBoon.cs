using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Boons/Health Boons")]
public class HealthBoon : Boons
{
    public float HealthAdded;
    
    public HealthBoon()
    {
        BoonDescription = "<b>Darah</b>mu bertambah lebih banyak.";
    }

    public override void ActivateOnStart(GameObject parent)
    {
        PlayerHealthManager playerHealth = parent.GetComponent<PlayerHealthManager>();

        playerHealth.MaxHealthPoints += HealthAdded;
        playerHealth.CurrentHealthPoints += HealthAdded;

        playerHealth.UpdateHealthbar();
    }
}
