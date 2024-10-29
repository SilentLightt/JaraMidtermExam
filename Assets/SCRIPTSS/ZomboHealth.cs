using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZomboHealth : MonoBehaviour
{
    public int maxHealth = 100;      // Maximum health for the zombie
    public int currentHealth;       // Current health of the zombie
    public TextMeshProUGUI hpText;   // UI element to display health

    void Start()
    {
        // Initialize zombie's health
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        // Ensure health doesn't drop below zero
        if (currentHealth <= 0)
        {
            Debug.Log("Zombie is already dead.");
            return;
        }

        // Subtract the damage from current health
        currentHealth -= damage;

        // Log damage taken and current health
        Debug.Log("Zombie took " + damage + " damage! Current health: " + currentHealth);

        // Ensure currentHealth doesn't go below zero
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthText();

        // Check if the zombie has died
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Function to handle zombie death
    public void Die()
    {
        Debug.Log("Zombie has died!");

        // You can add an animation or effect here if necessary before destroying
        Destroy(gameObject);  // Destroy the zombie object
    }

    public void UpdateHealthText()
    {
        if (hpText != null)
        {
            hpText.text = "Health: " + currentHealth.ToString();
        }
    }
}
