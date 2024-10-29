using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public StatModifier playerStats;
    //public StatModifier enemyStats;
    //public TextMeshProUGUI playerHealthText;
    //public TextMeshProUGUI enemyHealthText;
    //public GameObject combatTextPrefab; // Reference to the Combat Text Prefab (3D TextMeshPro)
    //public Transform playerTransform; // Reference to the enemy's Transform

    //private void Start()
    //{
    //    UpdateHealthUI();
    //}

    //public void AttackPlayer()
    //{
    //    if (enemyStats != null && playerStats != null)
    //    {
    //        float damage = enemyStats.CalculateDamage(playerStats, out bool isCritical, out bool isMissed);

    //        if (isMissed)
    //        {
    //            SpawnCombatText("Missed", Color.blue, playerTransform.position);
    //        }
    //        else
    //        {
    //            if (isCritical)
    //            {
    //                SpawnCombatText($"Critical! {damage:F1}", Color.red, playerTransform.position);
    //            }
    //            else
    //            {
    //                SpawnCombatText($"{damage:F1}", Color.white, playerTransform.position);
    //            }

    //            playerStats.health -= damage; // Correctly decrease player health
    //            playerStats.health = Mathf.Max(playerStats.health, 0);
    //            UpdateHealthUI();
    //        }
    //    }
    //}

    //public void UpdateHealthUI()
    //{
    //    if (playerHealthText != null)
    //        playerHealthText.text = $"Player Health: {playerStats.health}";

    //    if (enemyHealthText != null)
    //        enemyHealthText.text = $"Enemy Health: {enemyStats.health}";
    //}

    //public void SpawnCombatText(string message, Color color, Vector3 position)
    //{
    //    Vector3 textPosition = new Vector3(position.x, position.y + 2f, position.z);
    //    GameObject combatText = Instantiate(combatTextPrefab, textPosition, Quaternion.identity, transform);

    //    TextMeshPro textComponent = combatText.GetComponent<TextMeshPro>();
    //    textComponent.text = message;
    //    textComponent.color = color;

    //    Destroy(combatText, 1.5f); // Destroy after 1.5 seconds
    //}
}
    //public StatModifier enemyStats;
    //public StatModifier playerStats;
    //public TextMeshProUGUI playerHealthText;
    //public TextMeshProUGUI enemyHealthText;

    //private void Start()
    //{
    //    UpdateHealthUI();
    //}

    //public void AttackPlayer()
    //{
    //    if (enemyStats != null && playerStats != null)
    //    {
    //        float damage = enemyStats.CalculateDamage(playerStats);
    //        playerStats.health -= damage;
    //        playerStats.health = Mathf.Max(playerStats.health, 0); // Ensure health doesn't drop below zero
    //        UpdateHealthUI();
    //    }
    //}

    //private void UpdateHealthUI()
    //{
    //    if (playerHealthText != null)
    //        playerHealthText.text = $"Player Health: {playerStats.health}";

    //    if (enemyHealthText != null)
    //        enemyHealthText.text = $"Enemy Health: {enemyStats.health}";
    //}
//}