using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour
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

    public void SetHealth(float health, string filter = "")
    {
        if (filter == "score")
        {
            _healthSlider.value += health;
        }
        else
        {
            int maxHealth = (int)_healthSlider.maxValue;
            int newHealth = maxHealth - (int)(maxHealth / health);
            _healthSlider.value = newHealth;
        }
    }
}
