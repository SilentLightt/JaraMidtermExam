using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public StatModifier statModifier; // Reference to StatModifier for player's stats
    public GameObject damageTextPrefab; // Reference to the Damage Text prefab
    public float raycastRange = 2.0f; // Range of the sword attack raycast

    private void PerformRaycastAttack()
    {
        // Define the starting position and direction of the raycast
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        // Perform the raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, raycastRange))
        {
            Debug.Log("Raycast hit: " + hit.collider.name); // Log the object hit by the raycast

            // Check if the object hit has the EnemyAI component
            EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
            if (enemy != null && statModifier != null)
            {
                Debug.Log("Enemy detected via raycast: " + hit.collider.name + ". Applying damage.");

                // Use player's currentAttack from StatModifier for damage
                int finalDamage = statModifier.currentAttack;
                enemy.TakeDamage(finalDamage);

                // Spawn damage text at the hit point
                ShowDamageText(finalDamage, hit.point);
            }
            else
            {
                Debug.Log("Raycast hit a non-enemy object: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any objects.");
        }
    }

    private void ShowDamageText(int damage, Vector3 position)
    {
        if (damageTextPrefab != null)
        {
            // Instantiate the damage text prefab at the specified position
            GameObject damageTextInstance = Instantiate(damageTextPrefab, position, Quaternion.identity);

            // Set the text to display the damage amount
            TextMeshPro textMesh = damageTextInstance.GetComponentInChildren<TextMeshPro>();
            if (textMesh != null)
            {
                textMesh.text = damage.ToString();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Draw a line representing the raycast direction and range
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * raycastRange);

        // Draw a sphere at the end of the ray to indicate the range limit
        Gizmos.DrawWireSphere(transform.position + transform.forward * raycastRange, 0.1f);
    }
    // Method to be called when the player performs an attack (triggered by input or animation)
    public void Attack()
    {
        PerformRaycastAttack();
    }
    //public StatModifier statModifier; // Reference to StatModifier for player's stats
    //public GameObject damageTextPrefab; // Reference to the Damage Text prefab
    //private Collider swordCollider;

    //private void Start()
    //{
    //    swordCollider = GetComponent<Collider>();
    //    //swordCollider.enabled = false; // Start with the collider disabled
    //}

    //// This method enables the collider at peak swing via animation event
    //public void EnableHitbox()
    //{
    //    swordCollider.enabled = true;
    //}

    //// This method disables the collider at the end of the swing
    //public void DisableHitbox()
    //{
    //    swordCollider.enabled = false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Sword collided with enemy: " + other.name);

    //        EnemyAI enemy = other.GetComponent<EnemyAI>();
    //        if (enemy != null && statModifier != null)
    //        {
    //            Debug.Log("Applying damage to enemy: " + other.name);

    //            int finalDamage = statModifier.currentAttack;
    //            enemy.TakeDamage(finalDamage);

    //            Transform damageTextSpawnPoint = enemy.GetDamageTextSpawnPoint();
    //            ShowDamageText(finalDamage, damageTextSpawnPoint != null ? damageTextSpawnPoint.position : other.transform.position);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Sword hit a non-enemy object: " + other.name);
    //    }
    //    //Debug.Log("Sword collided with: " + other.name); // Log whenever a collision occurs

    //    //// Check if the collider belongs to an EnemyAI
    //    //EnemyAI enemy = other.GetComponent<EnemyAI>();
    //    //if (enemy != null && statModifier != null)
    //    //{
    //    //    // Log that the enemy was detected and we’re applying damage
    //    //    Debug.Log("Enemy detected: " + other.name + ". Applying damage.");

    //    //    // Use player's currentAttack from StatModifier for damage
    //    //    int finalDamage = statModifier.currentAttack;
    //    //    enemy.TakeDamage(finalDamage);

    //    //    // Spawn damage text at the enemy's specified spawn point
    //    //    Transform damageTextSpawnPoint = enemy.GetDamageTextSpawnPoint(); // Assuming this method is added to EnemyAI
    //    //    ShowDamageText(finalDamage, damageTextSpawnPoint != null ? damageTextSpawnPoint.position : other.transform.position);
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("Collision detected but no EnemyAI component found on: " + other.name);
    //    //}
    //}
    //private void ShowDamageText(int damage, Vector3 position)
    //{
    //    if (damageTextPrefab != null)
    //    {
    //        // Instantiate the damage text prefab at the specified position
    //        GameObject damageTextInstance = Instantiate(damageTextPrefab, position, Quaternion.identity);

    //        // Set the text to display the damage amount
    //        TextMeshPro textMesh = damageTextInstance.GetComponentInChildren<TextMeshPro>();
    //        if (textMesh != null)
    //        {
    //            textMesh.text = damage.ToString();
    //        }
    //    }
    //}

}


