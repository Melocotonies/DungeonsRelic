using UnityEngine;
using UnityEngine.UI;

public class RelicHealthBar : MonoBehaviour
{
    private Image _image;
    private Relic relic;

    private float health;

    private void Awake()
    {
        relic = GetComponentInParent<Relic>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        health = relic.currentHealth / relic.maxHealth;
        _image.fillAmount = Mathf.Clamp(health, 0f, relic.maxHealth);
    }
}
