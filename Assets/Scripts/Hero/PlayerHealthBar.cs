using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image bar;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        bar.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        bar.color = gradient.Evaluate(slider.normalizedValue);
    }
}
