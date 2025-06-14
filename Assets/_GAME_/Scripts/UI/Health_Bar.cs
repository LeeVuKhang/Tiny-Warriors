using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Health_Bar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;
    public void SetHealth(int health, int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = health;
        healthText.text = health + "/" + maxHealth;
    }

}
