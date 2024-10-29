using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent agent;
    public float detectionRange;
    public float attackRange;
    public Animator animator;
    private bool hasAttacked = false; // Cooldown flag for attack
    private Enemy enemyScript; // Reference to the Enemy script
    public StatModifier statModifier; // Reference to StatModifier for enemy stats
    public Transform damageTextSpawnPoint; // Assign this to the desired location on the enemy, e.g., head or chest
    public TextMeshProUGUI healthText; // Reference to TextMeshPro component for displaying health

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Initialize stats from StatModifier
        if (statModifier != null)
        {
            statModifier.ResetStats(); // Ensure stats are reset, including health
        }

        // Get reference to the Enemy script on this GameObject
        enemyScript = GetComponent<Enemy>();

        // Find the player in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Target = player.transform;
        }

        UpdateHealthText(); // Initial health display
    }

    void Update()
    {
        FollowTarget();
    }

    public Transform GetDamageTextSpawnPoint()
    {
        return damageTextSpawnPoint; // Return the assigned spawn point
    }

    public void FollowTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);
        Vector3 currentVelocity = agent.velocity;

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            agent.SetDestination(Target.position);
            animator.SetTrigger("Run"); // Trigger Running state
            RotateTowards(Target.position);
        }
        else if (distanceToPlayer <= attackRange && !hasAttacked)
        {
            AttackPlayer();
            RotateTowards(Target.position); // Ensure rotation towards the player when attacking
        }
        else if (distanceToPlayer > attackRange)
        {
            StopAttacking();
            agent.velocity = Vector3.zero;

            if (distanceToPlayer <= detectionRange)
            {
                agent.SetDestination(Target.position);
                animator.SetTrigger("Run");
                RotateTowards(Target.position);
            }
            else
            {
                agent.ResetPath();
                animator.SetTrigger("Idle");
                agent.velocity = Vector3.zero;
            }
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void AttackPlayer()
    {
        hasAttacked = true;
        animator.SetTrigger("Attack");
        Invoke(nameof(ResetAttack), 1.0f);
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    public void StopAttacking()
    {
        hasAttacked = false;
        animator.SetTrigger("Idle");
        agent.velocity = Vector3.zero;
    }

    // Method to handle taking damage from the player's sword
    public void TakeDamage(int damage)
    {
        if (statModifier != null)
        {
            statModifier.currentHP -= damage;
            animator.SetTrigger("Hurt");

            UpdateHealthText(); // Update health display on taking damage

            if (statModifier.currentHP <= 0)
            {
                Die();
            }
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null && statModifier != null)
        {
            healthText.text = $"HP: {statModifier.currentHP} / {statModifier.baseHP}";
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        agent.isStopped = true;
        this.enabled = false;
        Destroy(gameObject, 5f); // Delays destruction to allow death animation to play
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

//public class EnemyAI : MonoBehaviour
//{
//    public Transform Target;
//    private NavMeshAgent agent;
//    public float detectionRange;
//    public float attackRange;
//    public Animator animator;
//    private bool hasAttacked = false; // Cooldown flag for attack
//    private Enemy enemyScript; // Reference to the Enemy script
//    public int health = 100; // Health of the enemy

//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();

//        // Get reference to the Enemy script on this GameObject
//        enemyScript = GetComponent<Enemy>();

//        // Find the player in the scene
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            Target = player.transform;
//        }
//    }

//    void Update()
//    {
//        FollowTarget();
//    }

//    public void FollowTarget()
//    {
//        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);
//        Vector3 currentVelocity = agent.velocity;

//        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
//        {
//            // Follow the player if within detection range but outside attack range
//            agent.SetDestination(Target.position);
//            animator.SetTrigger("Run"); // Trigger Running state

//            // Rotate to face the player
//            RotateTowards(Target.position);
//        }
//        else if (distanceToPlayer <= attackRange && !hasAttacked)
//        {
//            // Attack the player if within attack range and hasn't attacked yet
//            AttackPlayer();
//            RotateTowards(Target.position); // Ensure rotation towards the player when attacking
//        }
//        else if (distanceToPlayer > attackRange)
//        {
//            // Stop attacking and follow or idle when out of attack range
//            StopAttacking();
//            agent.velocity = Vector3.zero;

//            // Continue following if within detection range but outside attack range
//            if (distanceToPlayer <= detectionRange)
//            {
//                agent.SetDestination(Target.position);
//                animator.SetTrigger("Run"); // Trigger Running state
//                RotateTowards(Target.position);
//            }
//            else
//            {
//                // Stop moving when outside detection range
//                agent.ResetPath();
//                animator.SetTrigger("Idle"); // Trigger Idle state
//                agent.velocity = Vector3.zero;
//            }
//        }
//    }

//    private void RotateTowards(Vector3 targetPosition)
//    {
//        Vector3 direction = (targetPosition - transform.position).normalized; // Calculate direction towards the player
//        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Create a rotation that only affects the Y-axis
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smoothly rotate towards the target
//    }

//    public void AttackPlayer()
//    {
//        hasAttacked = true; // Set attack cooldown
//        animator.SetTrigger("Attack"); // Trigger Attacking state

//        // Call the AttackPlayer method from the Enemy script to handle the attack
//        if (enemyScript != null)
//        {
//            //enemyScript.AttackPlayer();
//        }

//        // Add a delay before the enemy can attack again (cooldown)
//        Invoke(nameof(ResetAttack), 1.0f); // Adjust cooldown duration as needed
//    }

//    private void ResetAttack()
//    {
//        hasAttacked = false;
//    }

//    public void StopAttacking()
//    {
//        hasAttacked = false; // Reset the cooldown flag
//        animator.SetTrigger("Idle"); // Trigger Idle state
//        agent.velocity = Vector3.zero;
//    }

//    // Method to handle taking damage from the player's sword
//    public void TakeDamage(int damage)
//    {
//        health -= damage;

//        // Trigger hurt animation or effect if needed
//        animator.SetTrigger("Hurt");

//        if (health <= 0)
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        // Trigger death animation and disable AI behavior
//        animator.SetTrigger("Die");
//        agent.isStopped = true;
//        this.enabled = false; // Disable this script
//        // Optionally disable or destroy the GameObject
//        Destroy(gameObject, 3f); // Delays destruction to allow death animation to play
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//    }
//}

//working script
//public class EnemyAI : MonoBehaviour
//{
//    public Transform Target;
//    private NavMeshAgent agent;
//    public float detectionRange;
//    public float attackRange;
//    public Animator animator;
//    private bool hasAttacked = false; // Cooldown flag for attack
//    private Enemy enemyScript; // Reference to the Enemy script

//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();

//        // Get reference to the Enemy script on this GameObject
//        enemyScript = GetComponent<Enemy>();

//        // Find the player in the scene
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            Target = player.transform;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        FollowTarget();
//    }

//    public void FollowTarget()
//    {
//        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);
//        Vector3 currentVelocity = agent.velocity;

//        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
//        {
//            // Follow the player if within detection range but outside attack range
//            agent.SetDestination(Target.position);
//            animator.SetInteger("State", 1); // Running

//            // Rotate to face the player
//            RotateTowards(Target.position);
//        }
//        else if (distanceToPlayer <= attackRange && !hasAttacked)
//        {
//            // Attack the player if within attack range and hasn't attacked yet
//            AttackPlayer();
//            RotateTowards(Target.position); // Ensure rotation towards the player when attacking
//        }
//        else if (distanceToPlayer > attackRange)
//        {
//            // Stop attacking and follow or idle when out of attack range
//            StopAttacking();
//            agent.velocity = new Vector3(0, 0, 0);
//            // Continue following if within detection range but outside attack range
//            if (distanceToPlayer <= detectionRange)
//            {
//                agent.SetDestination(Target.position);
//                animator.SetInteger("State", 1); // Running
//                RotateTowards(Target.position);
//            }
//            else
//            {
//                // Stop moving when outside detection range
//                agent.ResetPath();
//                animator.SetInteger("State", 0); // Idle
//                agent.velocity = new Vector3(0, 0, 0);
//            }
//        }
//    }

//    private void RotateTowards(Vector3 targetPosition)
//    {
//        Vector3 direction = (targetPosition - transform.position).normalized; // Calculate direction towards the player
//        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Create a rotation that only affects the Y-axis
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smoothly rotate towards the target
//    }

//    public void AttackPlayer()
//    {
//        hasAttacked = true; // Set attack cooldown
//        animator.SetInteger("State", 2); // Attacking

//        // Call the AttackPlayer method from the Enemy script to handle the attack
//        if (enemyScript != null)
//        {
//            enemyScript.AttackPlayer();
//        }

//        // Start cooldown coroutine to prevent continuous attacking
//        StartCoroutine(AttackCooldown());
//    }

//    public void StopAttacking()
//    {
//        hasAttacked = false; // Reset the cooldown flag
//        animator.SetInteger("State", 0); // Set to Idle
//        agent.velocity = new Vector3(0, 0, 0);
//    }

//    private IEnumerator AttackCooldown()
//    {
//        yield return new WaitForSeconds(1.5f); // Adjust the cooldown time as needed
//        hasAttacked = false; // Reset the cooldown flag
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//    }
//}
//private bool IsPlayerVisible()
//{
//    // Cast a ray from the enemy to the player
//    RaycastHit hit;
//    Vector3 directionToPlayer = (Target.position - transform.position).normalized;

//    if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, obstacleLayer))
//    {
//        // Check if the ray hits something other than the player
//        if (hit.transform != Target)
//        {
//            // There's an obstacle between the enemy and the player
//            return false;
//        }
//    }

//    // Player is visible if no obstacle is hit
//    return true;
//}
//public class EnemyAI : MonoBehaviour
//{
//    public Transform Target;
//    NavMeshAgent agent;
//    public float detectionRange;
//    public float attackRange;
//    public Animator animator;
//    void Start()
//    {
//        agent = GetComponent<NavMeshAgent>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        FollowTarget();

//    }

//    public void FollowTarget()
//    {
//        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);
//        //Debug.Log("Enemy Location = " + Target.velocity);
//        Vector3 currentVelocity = agent.velocity;

//        if (distanceToPlayer <= detectionRange) 
//        {
//            //It should follow the player
//            agent.SetDestination(Target.position);
//            animator.SetInteger("Running", 1);
//        }
//        else if (currentVelocity.magnitude <=0)
//        {
//            //enemy stops
//            animator.SetInteger("Idle", 0);
//            //new Vector3 (0, 0, 0);
//        }
//    }
//    public void AttackPlayer()
//    {
//        float distanceToPlayer = Vector3.Distance(transform.position, Target.position);
//        if (distanceToPlayer <= attackRange)
//        {  agent.SetDestination(Target.position);
//            animator.SetInteger("Attacking", 2);
//        }

//    }
//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(transform.position, attackRange);
//    }
//}
