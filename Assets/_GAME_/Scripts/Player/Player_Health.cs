using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public Health_Bar health_Bar;
    private void Start()
    {
        health_Bar.SetHealth(Stats_Manager.Instance.currentHealth, Stats_Manager.Instance.maxHealth);
    }
    public void ChangeHealth(int amount)
    {
        Stats_Manager.Instance.currentHealth += amount;
        if (Stats_Manager.Instance.currentHealth > Stats_Manager.Instance.maxHealth)
        {
            Stats_Manager.Instance.currentHealth = Stats_Manager.Instance.maxHealth;
        }
        if (Stats_Manager.Instance.currentHealth <=0) 
        {
            Stats_Manager.Instance.currentHealth = 0;
            gameObject.SetActive(false);
        }
        health_Bar.SetHealth(Stats_Manager.Instance.currentHealth, Stats_Manager.Instance.maxHealth);
    }
}
