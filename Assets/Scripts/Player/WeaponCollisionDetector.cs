using MBT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollisionDetector : MonoBehaviour
{
    public PlayerStateMachine player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Attacked " + other.name +" with " + player._currentDamage + " damage");
            Blackboard enemyTree = other.GetComponentInChildren<Blackboard>();

            FloatVariable damageDealt = enemyTree.GetVariable<FloatVariable>("damageReceived");
            BoolVariable hittingEnemy = enemyTree.GetVariable<BoolVariable>("isHit");
            damageDealt.Value = player._currentDamage;
            hittingEnemy.Value = true;
        }
    }
}
