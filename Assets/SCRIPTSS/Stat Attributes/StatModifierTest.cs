using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatModifierTest : MonoBehaviour
{
    //[Header("References")]
    //public StatModifier playerStats;    // Reference to player's StatModifier ScriptableObject
    //public StatModifier enemyStats;     // Reference to enemy's StatModifier ScriptableObject

    //[Header("UI Elements")]
    //public TextMeshProUGUI resultText;  // Text element to display attack results
    //public TextMeshProUGUI playerHealthText; // Text element to display player's health
    //public TextMeshProUGUI enemyHealthText;  // Text element to display enemy's health
    //public Button attackButton;         // Button to initiate the attack

    //// Private health variables to track current health
    //private float playerCurrentHP;
    //private float enemyCurrentHP;

    //private void Start()
    //{
    //    // Initialize UI and health
    //    attackButton.onClick.AddListener(SimulateAttack);
    //    playerCurrentHP = playerStats.baseHP;
    //    enemyCurrentHP = enemyStats.baseHP;
    //    UpdateHealthUI();
    //    resultText.text = "Press 'Attack' to start!";
    //}

    //private void SimulateAttack()
    //{
    //    if (playerCurrentHP <= 0 || enemyCurrentHP <= 0)
    //    {
    //        resultText.text = "Battle is over! Reset to play again.";
    //        return;
    //    }

    //    // Calculate damage dealt by player and enemy for this attack
    //    float playerDamage = playerStats.SimulateAttack(1f); // 1 second of attacks
    //    float enemyDamage = enemyStats.SimulateAttack(1f);   // 1 second of attacks

    //    // Apply damage to health
    //    enemyCurrentHP -= playerDamage;
    //    playerCurrentHP -= enemyDamage;

    //    // Check for end of battle
    //    if (enemyCurrentHP <= 0 && playerCurrentHP > 0)
    //    {
    //        resultText.text = $"Player dealt {playerDamage:F1} damage. Enemy defeated!";
    //        enemyCurrentHP = 0;
    //    }
    //    else if (playerCurrentHP <= 0 && enemyCurrentHP > 0)
    //    {
    //        resultText.text = $"Enemy dealt {enemyDamage:F1} damage. Player defeated!";
    //        playerCurrentHP = 0;
    //    }
    //    else if (playerCurrentHP <= 0 && enemyCurrentHP <= 0)
    //    {
    //        resultText.text = "Both player and enemy are defeated!";
    //        playerCurrentHP = 0;
    //        enemyCurrentHP = 0;
    //    }
    //    else
    //    {
    //        // Display attack results
    //        resultText.text = $"Player dealt {playerDamage:F1} damage to Enemy.\n" +
    //                          $"Enemy dealt {enemyDamage:F1} damage to Player.";
    //    }

    //    // Update health UI after attack
    //    UpdateHealthUI();
    //}

    //private void UpdateHealthUI()
    //{
    //    playerHealthText.text = $"Player Health: {playerCurrentHP:F1}";
    //    enemyHealthText.text = $"Enemy Health: {enemyCurrentHP:F1}";
    //}
}
