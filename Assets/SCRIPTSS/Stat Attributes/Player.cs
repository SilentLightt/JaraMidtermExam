using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Using TextMeshPro for UI

public class Player : MonoBehaviour
{
    //public StatModifier playerStats;
    //public StatModifier enemyStats;
    //public TextMeshProUGUI playerHealthText;
    //public TextMeshProUGUI enemyHealthText;
    //public GameObject combatTextPrefab; // Reference to the Combat Text Prefab (3D TextMeshPro)
    //public Transform enemyTransform; // Reference to the enemy's Transform

    //private void Start()
    //{
    //    UpdateHealthUI();
    //}

    //public void AttackEnemy()
    //{
    //    if (enemyStats != null && playerStats != null)
    //    {
    //        float damage = playerStats.CalculateDamage(enemyStats, out bool isCritical, out bool isMissed);

    //        if (isMissed)
    //        {
    //            SpawnCombatText("Missed", Color.blue, enemyTransform.position);
    //        }
    //        else
    //        {
    //            if (isCritical)
    //            {
    //                SpawnCombatText($"Critical! {damage:F1}", Color.red, enemyTransform.position);
    //            }
    //            else
    //            {
    //                SpawnCombatText($"{damage:F1}", Color.yellow, enemyTransform.position);
    //            }

    //            enemyStats.health -= damage; // Correctly decrease enemy health
    //            enemyStats.health = Mathf.Max(enemyStats.health, 0);
    //            UpdateHealthUI();
    //        }
    //    }
    //}

    //private void UpdateHealthUI()
    //{
    //    if (playerHealthText != null)
    //        playerHealthText.text = $"Player Health: {playerStats.health}";

    //    if (enemyHealthText != null)
    //        enemyHealthText.text = $"Enemy Health: {enemyStats.health}";
    //}

    //private void SpawnCombatText(string message, Color color, Vector3 position)
    //{
    //    Vector3 textPosition = new Vector3(position.x, position.y + 2f, position.z);
    //    GameObject combatText = Instantiate(combatTextPrefab, textPosition, Quaternion.identity, transform);

    //    TextMeshPro textComponent = combatText.GetComponent<TextMeshPro>();
    //    textComponent.text = message;
    //    textComponent.color = color;

    //    Destroy(combatText, 1.5f); // Destroy after 1.5 seconds
    //}
}
    //public StatModifier playerStats;
    //public StatModifier enemyStats;
    //public TextMeshProUGUI playerHealthText;
    //public TextMeshProUGUI enemyHealthText;

    //private void Start()
    //{
    //    UpdateHealthUI();
    //}

    //public void AttackEnemy()
    //{
    //    if (playerStats != null && enemyStats != null)
    //    {
    //        float damage = playerStats.CalculateDamage(enemyStats);
    //        enemyStats.health -= damage;
    //        enemyStats.health = Mathf.Max(enemyStats.health, 0); // Ensure health doesn't drop below zero
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


