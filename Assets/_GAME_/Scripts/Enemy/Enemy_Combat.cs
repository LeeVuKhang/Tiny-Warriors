using UnityEngine;

public class Emeny_Combat : MonoBehaviour
{
    public int collisionDamage = 1;
    public int attackDamage = 2;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;
    public LayerMask playerLayer;
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-collisionDamage);
    //    }
    //}
    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-attackDamage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, stunTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
