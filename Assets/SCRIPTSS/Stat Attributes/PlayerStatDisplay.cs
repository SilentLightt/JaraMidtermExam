using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStatDisplay : MonoBehaviour
{
    public StatModifier statModifier;
    public TextMeshProUGUI healthText;

    [Header("UI Elements")]
    public GameObject statPanel;
    public Button openButton;
    public Button closeButton;

    public TextMeshProUGUI vitalityText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI pointsText;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI movementSpeedText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI hitchanceText;
    public TextMeshProUGUI critChanceText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI critDamageText;
    public TextMeshProUGUI scaleText;

    private int vitalityPoints = 0;
    private int strengthPoints = 0;
    private int agilityPoints = 0;
    private int dexterityPoints = 0;

    private int remainingPoints;
    public static bool isStatPanelOpen { get; private set; } // Static flag to track if the panel is open

    private void Start()
    {
        remainingPoints = statModifier.points;
        UpdateUI();

        openButton.onClick.AddListener(OpenStatPanel);
        closeButton.onClick.AddListener(CloseStatPanel);

        statPanel.SetActive(false);
        isStatPanelOpen = false; // Ensure it starts as closed
    }

    public void OpenStatPanel()
    {
        statPanel.SetActive(true);
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(true);
        isStatPanelOpen = true; // Set the flag to true when the panel opens
        UpdateUI();
    }

    public void CloseStatPanel()
    {
        statPanel.SetActive(false);
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        isStatPanelOpen = false; // Set the flag to false when the panel closes
    }

    public void IncreaseStat(string stat)
    {
        if (remainingPoints <= 0) return;

        switch (stat)
        {
            case "VIT":
                vitalityPoints++;
                break;
            case "STR":
                strengthPoints++;
                break;
            case "AGI":
                agilityPoints++;
                break;
            case "DEX":
                dexterityPoints++;
                break;
        }
        remainingPoints--;
        ApplyStats();
    }

    public void DecreaseStat(string stat)
    {
        switch (stat)
        {
            case "VIT":
                if (vitalityPoints > 0) { vitalityPoints--; remainingPoints++; }
                break;
            case "STR":
                if (strengthPoints > 0) { strengthPoints--; remainingPoints++; }
                break;
            case "AGI":
                if (agilityPoints > 0) { agilityPoints--; remainingPoints++; }
                break;
            case "DEX":
                if (dexterityPoints > 0) { dexterityPoints--; remainingPoints++; }
                break;
        }
        ApplyStats();
    }
    public void TakeDamage(int damage)
    {
        if (statModifier != null)
        {
            // Reduce the player's current HP
            statModifier.currentHP -= damage;

            // Clamp the HP to ensure it doesn’t go below zero
            statModifier.currentHP = Mathf.Max(statModifier.currentHP, 0);

            // Update the health UI to reflect the new HP
            UpdateHealthUI();

            // Check if the player has died
            if (statModifier.currentHP <= 0)
            {
                HandlePlayerDeath();
            }
        }
    }
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {statModifier.currentHP} / {statModifier.baseHP}";
        }
    }
    private void HandlePlayerDeath()
    {
        Debug.Log("Player has died!");
        // You can add additional logic here, like triggering a death animation or restarting the level
    }
    private void ApplyStats()
    {
        statModifier.ApplyPoints(vitalityPoints, strengthPoints, agilityPoints, dexterityPoints);
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointsText.text = "Points: " + remainingPoints;
        vitalityText.text = "VIT: " + vitalityPoints;
        strengthText.text = "STR: " + strengthPoints;
        agilityText.text = "AGI: " + agilityPoints;
        dexterityText.text = "DEX: " + dexterityPoints;

        hpText.text = "HP: " + statModifier.currentHP;

        float movementSpeedPercentage = statModifier.currentMovementSpeed * 100f;
        float attackSpeedPercentage = statModifier.currentAttackSpeed * 100f;

        movementSpeedText.text = "Movement Speed: " + movementSpeedPercentage.ToString("F1") + "%";
        attackSpeedText.text = "Attack Speed: " + attackSpeedPercentage.ToString("F1") + "%";

        attackText.text = "Attack: " + statModifier.currentAttack;
        hitchanceText.text = "Hit Chance: " + statModifier.currentHitchance + "%";
        critChanceText.text = "Crit Chance: " + statModifier.currentCritChance + "%";
        armorText.text = "Armor: " + statModifier.currentArmor;
        critDamageText.text = "Crit Damage: " + statModifier.currentCritDamage + "%";
        scaleText.text = "Scale: " + statModifier.currentScale;
    }
}

//public class StatDisplay : MonoBehaviour
//{
//public StatModifier playerStats;
//public StatModifier enemyStats;
//public TextMeshProUGUI playerHealthText;
//public TextMeshProUGUI playerArmorText;
//public TextMeshProUGUI playerCritChanceText;
//public TextMeshProUGUI enemyHealthText;
//public TextMeshProUGUI enemyArmorText;
//public TextMeshProUGUI enemyCritChanceText;

//private void Update()
//{
//    UpdatePlayerUI();
//    UpdateEnemyUI();
//}

//private void UpdatePlayerUI()
//{
//    if (playerStats != null)
//    {
//        playerHealthText.text = $"Player Health: {playerStats.health}";
//        playerArmorText.text = $"Player Armor: {playerStats.armor}";
//        playerCritChanceText.text = $"Player Crit Chance: {playerStats.critChance}%";
//    }
//}

//private void UpdateEnemyUI()
//{
//    if (enemyStats != null)
//    {
//        enemyHealthText.text = $"Enemy Health: {enemyStats.health}";
//        enemyArmorText.text = $"Enemy Armor: {enemyStats.armor}";
//        enemyCritChanceText.text = $"Enemy Crit Chance: {enemyStats.critChance}%";
//    }
//}
//}

