using UnityEngine;
// Modified StatModifier Script

[CreateAssetMenu(fileName = "NewStatModifier", menuName = "Stats/StatModifier")]
public class StatModifier : ScriptableObject
{
    [Header("Base Stats")]
    public int baseHP = 100;
    public float baseMovementSpeed = 1f; // Starts at 25% of full speed
    public float baseAttackSpeed = 1f;   // Starts at 25% of full attack speed
    public int baseAttack = 1;
    public float baseHitchance = 20f;
    public float baseCritChance = 5f;
    public int baseArmor = 1;
    public float baseCritDamage = 10f;
    public Vector3 baseScale = new Vector3(1, 1, 1);

    public int points = 30;

    [Header("Modified Stats")]
    public int currentHP;
    public float currentMovementSpeed;
    public float currentAttackSpeed;
    public int currentAttack;
    public float currentHitchance;
    public float currentCritChance;
    public int currentArmor;
    public float currentCritDamage;
    public Vector3 currentScale;

    private void OnEnable()
    {
        ResetStats();
    }

    public void ResetStats()
    {
        currentHP = baseHP;
        currentMovementSpeed = baseMovementSpeed;
        currentAttackSpeed = baseAttackSpeed;
        currentAttack = baseAttack;
        currentHitchance = baseHitchance;
        currentCritChance = baseCritChance;
        currentArmor = baseArmor;
        currentCritDamage = baseCritDamage;
        currentScale = baseScale;
    }
    public void ApplyPoints(int vitalityPoints, int strPoints, int agiPoints, int dexPoints)
    {
        // Reset stats before applying new modifications
        ResetStats();

        // VITALITY Modifiers
        currentHP += vitalityPoints * 15;
        currentAttack += vitalityPoints * 3;
        currentCritChance = Mathf.Max(1f, currentCritChance - (vitalityPoints * 3f));

        // STR Modifiers - Increase scale on X, Y, and Z based on STR
        currentHP += strPoints * 5;
        currentAttack += strPoints * 3;
        currentScale.x += strPoints * 0.1f;
        currentScale.y += strPoints * 0.1f;
        currentScale.z += strPoints * 0.1f;
        currentMovementSpeed = Mathf.Max(1f, currentMovementSpeed - (strPoints * 1f));
        currentAttackSpeed = Mathf.Max(1f, currentAttackSpeed - (strPoints * 1f));

        // AGI Modifiers - Decrease scale on X, Y, and Z based on AGI
        currentArmor += Mathf.FloorToInt(agiPoints * 0.2f);
        currentMovementSpeed += agiPoints * 3f;
        currentScale.x = Mathf.Max(0.5f, currentScale.x - (agiPoints * 0.05f));
        currentScale.y = Mathf.Max(0.5f, currentScale.y - (agiPoints * 0.05f));
        currentScale.z = Mathf.Max(0.5f, currentScale.z - (agiPoints * 0.05f));
        currentCritDamage = Mathf.Max(1f, currentCritDamage - (agiPoints * 1f));
        currentAttack = Mathf.Max(1, currentAttack - (agiPoints * 2));
        currentAttackSpeed += agiPoints * 3f;

        // DEX Modifiers
        currentCritDamage += dexPoints * 3f;
        currentHitchance += dexPoints * 5f;
        currentCritChance += dexPoints * 5f;
    }
}
    //public void ApplyPoints(int vitalityPoints, int strPoints, int agiPoints, int dexPoints)
    //{
    //    ResetStats();

    //    // VITALITY Modifiers
    //    currentHP += vitalityPoints * 15;
    //    currentAttack += vitalityPoints * 3;
    //    currentCritChance = Mathf.Max(1f, currentCritChance - (vitalityPoints * 3f));

    //    // STR Modifiers
    //    currentHP += strPoints * 5;
    //    currentAttack += strPoints * 3;
    //    currentScale.y += strPoints * 0.1f;

    //    // Adjust movement and attack speeds by flat percentages for AGI
    //    currentMovementSpeed += agiPoints * 0.03f; // Increases by 3% per AGI point
    //    currentAttackSpeed += agiPoints * 0.03f;   // Increases by 3% per AGI point

    //    // AGI Modifiers
    //    currentArmor += Mathf.FloorToInt(agiPoints * 0.2f);
    //    currentCritDamage = Mathf.Max(1f, currentCritDamage - (agiPoints * 1f));
    //    currentAttack = Mathf.Max(1, currentAttack - (agiPoints * 2));

    //    // DEX Modifiers
    //    currentCritDamage += dexPoints * 3f;
    //    currentHitchance += dexPoints * 5f;
    //    currentCritChance += dexPoints * 5f;
    //}
    //}

//whole number speed script
//public class StatModifier : ScriptableObject
//{
//    // Base Stats
//    [Header("Base Stats")]
//    public int baseHP = 100;
//    public float baseMovementSpeed = 0.25f;
//    public float baseAttackSpeed = 0.25f;
//    public int baseAttack = 1;
//    public float baseHitchance = 0.20f;
//    public float baseCritChance = 5f;
//    public int baseArmor = 1;
//    public float baseCritDamage = 10f;
//    public Vector3 baseScale = new Vector3(1, 1, 1);

//    // Points available to spend
//    public int points = 30;

//    // Current stats modified by points
//    [Header("Modified Stats")]
//    public int currentHP;
//    public float currentMovementSpeed;
//    public float currentAttackSpeed;
//    public int currentAttack;
//    public float currentHitchance;
//    public float currentCritChance;
//    public int currentArmor;
//    public float currentCritDamage;
//    public Vector3 currentScale;

//    private void OnEnable()
//    {
//        // Initialize current stats with base stats
//        ResetStats();
//    }

//    public void ResetStats()
//    {
//        currentHP = baseHP;
//        currentMovementSpeed = baseMovementSpeed;
//        currentAttackSpeed = baseAttackSpeed;
//        currentAttack = baseAttack;
//        currentHitchance = baseHitchance;
//        currentCritChance = baseCritChance;
//        currentArmor = baseArmor;
//        currentCritDamage = baseCritDamage;
//        currentScale = baseScale;
//    }

//    public void ApplyPoints(int vitalityPoints, int strPoints, int agiPoints, int dexPoints)
//    {
//        // Reset stats before applying new modifications
//        ResetStats();

//        // VITALITY Modifiers
//        currentHP += vitalityPoints * 15;
//        currentAttack += vitalityPoints * 3;
//        currentCritChance = Mathf.Max(1f, currentCritChance - (vitalityPoints * 3f));

//        // STR Modifiers
//        currentHP += strPoints * 5;
//        currentAttack += strPoints * 3;
//        currentScale.y += strPoints * 0.1f;
//        currentMovementSpeed = Mathf.Max(1f, currentMovementSpeed - (strPoints * 1f));
//        currentAttackSpeed = Mathf.Max(1f, currentAttackSpeed - (strPoints * 1f));

//        // AGI Modifiers
//        currentArmor += Mathf.FloorToInt(agiPoints * 0.2f);
//        currentMovementSpeed += agiPoints * 3f;
//        currentScale.y = Mathf.Max(1f, currentScale.y - (agiPoints * 0.1f));
//        currentCritDamage = Mathf.Max(1f, currentCritDamage - (agiPoints * 1f));
//        currentAttack = Mathf.Max(1, currentAttack - (agiPoints * 2));
//        currentAttackSpeed += agiPoints * 3f;

//        // DEX Modifiers
//        currentCritDamage += dexPoints * 3f;
//        currentHitchance += dexPoints * 5f;
//        currentCritChance += dexPoints * 5f;
//    }
//}


//[CreateAssetMenu(fileName = "StatModifier", menuName = "Stats/StatModifier")]
//public class StatModifier : ScriptableObject
//{
//    // Base Stats
//    [Header("Base Stats")]
//    public int baseHP = 100;
//    public int baseAttack = 1;
//    public float baseMovementSpeed = 0.25f;  // Representing 25% as 0.25
//    public float baseAttackSpeed = 0.25f;     // Representing 25% as 0.25
//    public float baseHitChance = 0.01f;       // Representing % as 0.01
//    public float baseArmor = 1f;
//    public float baseCritDamage = 0.01f;      // Representing 1% as 0.01
//    public Vector3 baseScale = new Vector3(1, 1, 1);

//    // Points and Modifiers
//    private int points = 30;
//    private int vitalityPoints, strengthPoints, agilityPoints, dexterityPoints;

//    // Calculated Stats
//    private int currentHP;
//    private int currentAttack;
//    private float currentMovementSpeed;
//    private float currentAttackSpeed;
//    private float currentHitChance;
//    private float currentArmor;
//    private float currentCritDamage;
//    private Vector3 currentScale;

//    private void OnEnable()
//    {
//        ResetStats();
//    }

//    private void ResetStats()
//    {
//        // Set stats to base values
//        currentHP = baseHP;
//        currentAttack = baseAttack;
//        currentMovementSpeed = baseMovementSpeed;
//        currentAttackSpeed = baseAttackSpeed;
//        currentHitChance = baseHitChance;
//        currentArmor = baseArmor;
//        currentCritDamage = baseCritDamage;
//        currentScale = baseScale;
//    }

//    // Method to allocate points to an attribute
//    public void AllocatePoints(string attribute, int pointsToAllocate)
//    {
//        if (points < pointsToAllocate) return; // Ensure points limit

//        switch (attribute)
//        {
//            case "Vitality":
//                vitalityPoints += pointsToAllocate;
//                currentHP = Mathf.Max(baseHP, baseHP + vitalityPoints * 15);
//                currentAttack = Mathf.Max(baseAttack, baseAttack + vitalityPoints * 3);
//                break;

//            case "Strength":
//                strengthPoints += pointsToAllocate;
//                currentHP = Mathf.Max(baseHP, currentHP + strengthPoints * 5);
//                currentAttack = Mathf.Max(baseAttack, currentAttack + strengthPoints * 3);
//                currentMovementSpeed = Mathf.Max(baseMovementSpeed, currentMovementSpeed - strengthPoints * 0.01f);
//                currentAttackSpeed = Mathf.Max(baseAttackSpeed, currentAttackSpeed - strengthPoints * 0.01f);
//                currentScale = new Vector3(currentScale.x, baseScale.y + strengthPoints, currentScale.z);
//                break;

//            case "Agility":
//                agilityPoints += pointsToAllocate;
//                currentArmor = Mathf.Max(baseArmor, currentArmor + agilityPoints * 0.2f);
//                currentMovementSpeed = Mathf.Max(baseMovementSpeed, currentMovementSpeed + agilityPoints * 0.03f);
//                currentAttackSpeed = Mathf.Max(baseAttackSpeed, currentAttackSpeed + agilityPoints * 0.03f);
//                currentCritDamage = Mathf.Max(baseCritDamage, currentCritDamage - agilityPoints * 0.01f);
//                currentAttack = Mathf.Max(baseAttack, currentAttack - agilityPoints * 2);
//                currentScale = new Vector3(currentScale.x, Mathf.Max(1, baseScale.y - agilityPoints), currentScale.z);
//                break;

//            case "Dexterity":
//                dexterityPoints += pointsToAllocate;
//                currentCritDamage = Mathf.Max(baseCritDamage, currentCritDamage + dexterityPoints * 0.03f);
//                currentHitChance = Mathf.Max(baseHitChance, currentHitChance + dexterityPoints * 0.05f);
//                break;
//        }

//        points -= pointsToAllocate; // Deduct allocated points
//    }

//    public void DisplayCurrentStats()
//    {
//        Debug.Log($"Current HP: {currentHP}");
//        Debug.Log($"Current Attack: {currentAttack}");
//        Debug.Log($"Current Movement Speed: {currentMovementSpeed * 100}%");
//        Debug.Log($"Current Attack Speed: {currentAttackSpeed * 100}%");
//        Debug.Log($"Current Hit Chance: {currentHitChance * 100}%");
//        Debug.Log($"Current Armor: {currentArmor}");
//        Debug.Log($"Current Crit Damage: {currentCritDamage * 100}%");
//        Debug.Log($"Current Scale: {currentScale}");
//        Debug.Log($"Remaining Points: {points}");
//    }
//}


//oldupdate script
//[CreateAssetMenu(fileName = "NewStatModifier", menuName = "RPG/StatModifier")]
//public class StatModifier : ScriptableObject
//{
//    [Header("Constitution-Based Stats")]
//    public float health;
//    public float armor;
//    [Range(0, 100)] public float armorReduction; // Reduction percentage

//    [Header("Dexterity-Based Stats")]
//    [Range(0, 100)] public float critChance; // Percentage
//    [Range(0, 100)] public float hitChance; // Percentage

//    [Header("Strength-Based Stats")]
//    public float attack;
//    [Range(0, 100)] public float critDamage; // Percentage

//    [Header("Agility-Based Stats")]
//    public float attackSpeed;
//    public float playerMovementSpeed;

//    // Method to simulate an attack on a target
//    public float CalculateDamage(StatModifier target, out bool isCritical, out bool isMissed)
//    {
//        isCritical = false;
//        isMissed = false;

//        // Calculate hit chance
//        float hitRoll = Random.Range(0f, 100f);
//        if (hitRoll > hitChance) // Missed attack
//        {
//            isMissed = true;
//            return 0;
//        }

//        // Calculate if it's a critical hit
//        float damage = attack;
//        float critRoll = Random.Range(0f, 100f);
//        if (critRoll < critChance)
//        {
//            isCritical = true;
//            damage += (damage * (critDamage / 100f)); // Add crit bonus damage
//        }

//        // Apply armor reduction of the target
//        float damageReduction = target.armor * (1 - (armorReduction / 100f));
//        float finalDamage = Mathf.Max(damage - damageReduction, 0); // Ensure no negative damage

//        return finalDamage;
//    }
//}
//[Header("Constitution-Based Stats")]
//public float health;
//public float armor;
//[Range(0, 100)] public float armorReduction; // Reduction percentage

//[Header("Dexterity-Based Stats")]
//[Range(0, 100)] public float critChance; // Percentage
//[Range(0, 100)] public float hitChance; // Percentage

//[Header("Strength-Based Stats")]
//public float attack;
//[Range(0, 100)] public float critDamage; // Percentage

//[Header("Agility-Based Stats")]
//public float attackSpeed;
//public float playerMovementSpeed;

//// Method to simulate an attack on a target
//public float CalculateDamage(StatModifier target)
//{
//    // Calculate hit chance
//    float hitRoll = Random.Range(0f, 100f);
//    if (hitRoll > hitChance) // Missed attack
//    {
//        return 0;
//    }

//    // Calculate if it's a critical hit
//    float damage = attack;
//    float critRoll = Random.Range(0f, 100f);
//    if (critRoll < critChance)
//    {
//        damage += (damage * (critDamage / 100f)); // Add crit bonus damage
//    }

//    // Apply armor reduction of the target
//    float damageReduction = target.armor * (1 - (armorReduction / 100f));
//    float finalDamage = Mathf.Max(damage - damageReduction, 0); // Ensure no negative damage

//    return finalDamage;
//}
//}

