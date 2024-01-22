using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void SetHealth(float health, bool isObstacle = false)
    {
        int maxHealth = (int)_healthSlider.maxValue;
        
        if (!isObstacle)
        {
            _healthSlider.value += health;
        }
        else
        {
            int newHealth = maxHealth - (int)(maxHealth / health);
            _healthSlider.value = newHealth;
        }

        if(_healthSlider.value == maxHealth)
        {
            SoundEFManager.instance.PlaySoundEffect("health");
        }
    }
}
