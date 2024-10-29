using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyStatDisplay : MonoBehaviour
{
    public StatModifier statModifier;

    // Base and current stat text displays
    public TextMeshProUGUI baseVitalityText;
    public TextMeshProUGUI baseStrengthText;
    public TextMeshProUGUI baseAgilityText;
    public TextMeshProUGUI baseDexterityText;
    public TextMeshProUGUI vitalityText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI dexterityText;

    private int vitalityPoints = 5;
    private int strPoints = 5;
    private int agiPoints = 5;
    private int dexPoints = 5;

    private void Start()
    {
        DisplayBaseStats();
        ApplyAndDisplayCurrentStats();
    }

    private void DisplayBaseStats()
    {
        baseVitalityText.text = "Base VIT: " + statModifier.baseHP;
        baseStrengthText.text = "Base STR: " + statModifier.baseAttack;
        baseAgilityText.text = "Base AGI: " + statModifier.baseMovementSpeed;
        baseDexterityText.text = "Base DEX: " + statModifier.baseCritChance;
    }

    private void ApplyAndDisplayCurrentStats()
    {
        statModifier.ApplyPoints(vitalityPoints, strPoints, agiPoints, dexPoints);

        vitalityText.text = "VIT: " + vitalityPoints;
        strengthText.text = "STR: " + strPoints;
        agilityText.text = "AGI: " + agiPoints;
        dexterityText.text = "DEX: " + dexPoints;
    }
}
//public class EnemyStatDsiplay : MonoBehaviour
//{
//public StatModifier enemyStats;

//// Basic attribute points display
//public TextMeshProUGUI enemyStrengthText;
//public TextMeshProUGUI enemyVitalityText;
//public TextMeshProUGUI enemyAgilityText;
//public TextMeshProUGUI enemyDexterityText;

//// Derived stats display
//public TextMeshProUGUI enemyHPText;
//public TextMeshProUGUI enemyMovementSpeedText;
//public TextMeshProUGUI enemyAttackSpeedText;
//public TextMeshProUGUI enemyAttackText;
//public TextMeshProUGUI enemyHitChanceText;
//public TextMeshProUGUI enemyCritChanceText;
//public TextMeshProUGUI enemyArmorText;
//public TextMeshProUGUI enemyCritDamageText;
//public TextMeshProUGUI enemySizeText;

//private void Start()
//{
//    UpdateEnemyDisplay();
//    UpdateEnemyDerivedStats();
//}

//private void UpdateEnemyDisplay()
//{
//    enemyStrengthText.text = "Strength: " + enemyStats.strengthPoints;
//    enemyVitalityText.text = "Vitality: " + enemyStats.vitalityPoints;
//    enemyAgilityText.text = "Agility: " + enemyStats.agilityPoints;
//    enemyDexterityText.text = "Dexterity: " + enemyStats.dexterityPoints;
//}

//private void UpdateEnemyDerivedStats()
//{
//    enemyStats.ApplyAttributes();  // Recalculate stats based on new values

//    enemyHPText.text = "HP: " + enemyStats.currentHP + " (VIT " + enemyStats.vitalityPoints + ")";
//    enemyMovementSpeedText.text = "Movement Speed: " + enemyStats.currentMovementSpeed + " (AGI " + enemyStats.agilityPoints + ")";
//    enemyAttackSpeedText.text = "Attack Speed: " + enemyStats.currentAttackSpeed + " (AGI " + enemyStats.agilityPoints + ")";
//    enemyAttackText.text = "Attack: " + enemyStats.currentAttack + " (STR " + enemyStats.strengthPoints + ")";
//    enemyHitChanceText.text = "Hit Chance: " + enemyStats.currentHitChance + " (DEX " + enemyStats.dexterityPoints + ")";
//    enemyCritChanceText.text = "Crit Chance: " + enemyStats.currentCritChance + " (DEX " + enemyStats.dexterityPoints + ")";
//    enemyArmorText.text = "Armor: " + enemyStats.currentArmor + " (AGI " + enemyStats.agilityPoints + ")";
//    enemyCritDamageText.text = "Crit Damage: " + enemyStats.currentCritDamage + " (DEX " + enemyStats.dexterityPoints + ")";
//    enemySizeText.text = "Size: " + enemyStats.scale.y + " (STR " + enemyStats.strengthPoints + ")";
//}

//public void IncreaseEnemyStat(string statName)
//{
//    ModifyStat(statName, 1);
//    UpdateEnemyDerivedStats();
//}

//public void DecreaseEnemyStat(string statName)
//{
//    ModifyStat(statName, -1);
//    UpdateEnemyDerivedStats();
//}

//private void ModifyStat(string statName, int change)
//{
//    switch (statName)
//    {
//        case "Strength":
//            enemyStats.strengthPoints += change;
//            break;
//        case "Vitality":
//            enemyStats.vitalityPoints += change;
//            break;
//        case "Agility":
//            enemyStats.agilityPoints += change;
//            break;
//        case "Dexterity":
//            enemyStats.dexterityPoints += change;
//            break;
//    }
//    enemyStats.ApplyAttributes();
//    UpdateEnemyDisplay();
//}
//}
