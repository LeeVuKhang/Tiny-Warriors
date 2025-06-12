using System.Threading;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class Player_Combat : MonoBehaviour
{
    public Animator anim;
    public float attackCooldownTimer;
    public Transform attackPoint;
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
            attackCooldownTimer = Stats_Manager.Instance.attackCooldown;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, Stats_Manager.Instance.weaponRange, enemyLayer);

        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-Stats_Manager.Instance.attackDamage);
            enemies[0].GetComponent<Enemy_Movement>().Knockback(transform, Stats_Manager.Instance.knockbackForce, Stats_Manager.Instance.stunTime);

        }
    }

    public void FinishAttacking() 
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, Stats_Manager.Instance.weaponRange);
    }
}
