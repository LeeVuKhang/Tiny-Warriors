using System.Threading;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class Player_Combat : MonoBehaviour
{
    public Animator anim;
    public int attackDamage = 2;
    public float attackCooldown = 1;
    public float attackCooldownTimer;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;
    public LayerMask enemyLayer;
    private void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (attackCooldownTimer <= 0)
        {
            anim.SetBool("isAttacking", true);
            attackCooldownTimer = attackCooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-attackDamage);
            enemies[0].GetComponent<Enemy_Movement>().Knockback(transform, knockbackForce, stunTime);

        }
    }

    public void FinishAttacking() 
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
