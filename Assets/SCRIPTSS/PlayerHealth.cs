using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public TextMeshProUGUI hpText;

    // Reference to the damage text prefab
    public GameObject damageTextPrefab;
    public Transform damageTextSpawnPoint; // A point in the world where the damage text should appear, e.g., above the player

    void Start()
    {
        // Initialize player's health
        currentHealth = maxHealth;
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        // Ensure health doesn't drop below zero
        if (currentHealth <= 0)
        {
            // If health is already 0 or less, don't take more damage
            Debug.Log("Player is already dead.");
            return;
        }

        // Subtract the damage from current health
        currentHealth -= damage;

        // Log damage taken and current health
        Debug.Log("Player took " + damage + " damage! Current health: " + currentHealth);

        // Ensure currentHealth doesn't go below zero
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Update the health text UI
        UpdateHealthText();

        // Show damage text
        ShowDamageText(damage);

        // Check if the player has died
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Function to handle player death
    private void Die()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Debug.Log("Player has died!");
        // Implement any additional logic for when the player dies, like disabling controls or showing a game over screen
    }

    private void UpdateHealthText()
    {
        hpText.text = " " + currentHealth.ToString();
    }

    // Function to show damage text when player takes damage
    private void ShowDamageText(int damage)
    {
        if (damageTextPrefab != null && damageTextSpawnPoint != null)
        {
            // Instantiate the damage text prefab at the spawn point
            GameObject damageTextInstance = Instantiate(damageTextPrefab, damageTextSpawnPoint.position, Quaternion.identity);

            // Get the TextMeshProUGUI component from the instance and set its text to the damage amount
            TextMeshProUGUI damageText = damageTextInstance.GetComponent<TextMeshProUGUI>();
            if (damageText != null)
            {
                damageText.text = "-" + damage.ToString();
            }

            // Destroy the damage text instance after 1 second
            Destroy(damageTextInstance, 1.0f);
        }
    }
}
