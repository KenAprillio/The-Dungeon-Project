using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boons/Shield Boons")]
public class ShieldBoon : Boons
{
    public float ShieldAdded;

    public ShieldBoon()
    {
        BoonDescription = "<b>Perisai</b>mu menjadi lebih kuat.";
    }

    public override void ActivateOnStart(GameObject parent)
    {
        PlayerHealthManager playerHealth = parent.GetComponent<PlayerHealthManager>();

        playerHealth.MaxShieldPoints += ShieldAdded;
        playerHealth.CurrentShieldPoints += ShieldAdded;

        playerHealth.UpdateHealthbar();
    }
}
