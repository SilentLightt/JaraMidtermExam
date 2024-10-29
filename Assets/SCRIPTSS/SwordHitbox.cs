using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public StatModifier statModifier; // Reference to StatModifier for player's stats
    public GameObject damageTextPrefab; // Reference to the Damage Text prefab
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
        if (enemy != null && statModifier != null)
        {
            // Use player's currentAttack from StatModifier for damage
            int finalDamage = statModifier.currentAttack;
            enemy.TakeDamage(finalDamage);

            // Spawn damage text
            ShowDamageText(finalDamage, other.transform.position);
        }
    }

    private void ShowDamageText(int damage, Vector3 position)
    {
        if (damageTextPrefab != null)
        {
            // Instantiate the damage text prefab at the hit position
            GameObject damageTextInstance = Instantiate(damageTextPrefab, position, Quaternion.identity);

            // Set the text to display the damage amount
            TextMeshPro textMesh = damageTextInstance.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
                textMesh.text = damage.ToString();
            }
        }
    }
}


