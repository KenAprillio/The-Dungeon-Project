using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Boons/Attack Speed Boons")]
public class AttackSpeedBoon : Boons
{
    public float AttackSpeedMultiplier;

    public AttackSpeedBoon()
    {
        BoonDescription = "<b>Serangan</b>mu menjadi lebih cepat";
    }

    public override void ActivateOnStart(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();

        animator.SetFloat("attackSpeed", AttackSpeedMultiplier);
    }
}
