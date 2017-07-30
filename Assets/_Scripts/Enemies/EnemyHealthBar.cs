using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image _image;
    private Enemy enemy;

    private float health;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        health = enemy.currentHealth / enemy._maxHealth;
        _image.fillAmount = Mathf.Clamp(health, 0f, enemy._maxHealth);
    }
}
