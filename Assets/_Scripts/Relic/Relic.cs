using UnityEngine;

public class Relic : MonoBehaviour
{
    public float maxHealth { get; private set; }
    public float currentHealth { get; set; }

    private void Awake()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) GameOver();
    }

    private void GameOver()
    {
        GameManager.currentState = GameManager.State.LOST;
    }
}
