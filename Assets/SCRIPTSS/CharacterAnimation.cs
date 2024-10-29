using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    public StatModifier statModifier; // Reference to StatModifier for attack speed
    public int idleVariants = 2;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        StartCoroutine(SwitchIdleAnimation());
    }

    public void UpdateAnimationStates(bool isWalking, bool isRunning, bool isJumping)
    {
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);

        if (isWalking || isRunning || isJumping)
        {
            StopCoroutine(SwitchIdleAnimation());
        }
        else
        {
            if (!IsInvoking(nameof(RestartIdleSwitching)))
            {
                Invoke(nameof(RestartIdleSwitching), 0.1f);
            }
        }
    }

    public void SetAttackSpeed()
    {
        // Update the animator speed based on currentAttackSpeed from StatModifier
        animator.SetFloat("AttackSpeedMultiplier", statModifier.currentAttackSpeed);
    }

    public void TriggerAttackAnimation(int attackType)
    {
        SetAttackSpeed(); // Ensure the attack speed is updated
        animator.SetInteger("AttackType", attackType);
        animator.SetTrigger("Attack");
    }

    public void TriggerJump()
    {
        animator.SetTrigger("Jump");
    }

    private void RestartIdleSwitching()
    {
        if (!IsWalkingOrRunning() && !animator.GetBool("IsJumping"))
        {
            StartCoroutine(SwitchIdleAnimation());
        }
    }

    private bool IsWalkingOrRunning()
    {
        return animator.GetBool("IsWalking") || animator.GetBool("IsRunning");
    }

    private IEnumerator SwitchIdleAnimation()
    {
        while (true)
        {
            int randomIdle = Random.Range(1, idleVariants + 1);
            animator.SetInteger("IdleVariant", randomIdle);

            yield return new WaitForSeconds(Random.Range(2, 10));
        }
    }
}


