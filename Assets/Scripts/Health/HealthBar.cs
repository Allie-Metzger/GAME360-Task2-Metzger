using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbar;
    public HealthController bossHealthController;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHealthController.currentHealth = bossHealthController.maximumHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = bossHealthController.currentHealth;
        healthbar.maxValue = bossHealthController.maximumHealth;
    }
}
