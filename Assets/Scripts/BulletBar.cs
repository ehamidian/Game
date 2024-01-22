using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BulletBar : MonoBehaviour
{
    Slider _bulletSlider;

    private void Start()
    {
        _bulletSlider = GetComponent<Slider>();
    }

    public void SetMaxBullet(int maxBullet)
    {
        _bulletSlider.value = maxBullet;
    }

    public void SetBullet(float bullet)
    {
       _bulletSlider.value += bullet;
    }
}
