using UnityEngine;

public class Emeny_Combat : MonoBehaviour
{
    public int collisionDamage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-collisionDamage);
        }
    }
}
