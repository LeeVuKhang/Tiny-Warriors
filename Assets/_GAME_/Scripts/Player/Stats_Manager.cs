using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    public static Stats_Manager Instance;

    [Header("Combat stats")]
    public int attackDamage;
    public float attackCooldown;
    public float weaponRange;
    public float knockbackForce;
    public float stunTime;


    [Header("Movement stats")]
    public float speed;


    [Header("Health stats")]
    public int currentHealth;
    public int maxHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
}
