using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int baseDamage = 10;  // Base damage for each hit
    public StatModifier statModifier; // Reference to StatModifier for attack speed and other stats
    private Collider swordCollider;

    private void Start()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false; // Start with the collider disabled
    }

    // This method enables the collider at peak swing via animation event
    public void EnableHitbox()
    {
        swordCollider.enabled = true;
    }

    // This method disables the collider at the end of the swing
    public void DisableHitbox()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an EnemyAI
        EnemyAI enemy = other.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            // Calculate damage based on stats (modify if needed)
            int finalDamage = baseDamage;
            enemy.TakeDamage(finalDamage);
        }
    }
}


