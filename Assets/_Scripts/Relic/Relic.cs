using UnityEngine;

public class Relic : MonoBehaviour
{
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    private void Awake()
    {
        maxHealth = 20;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) GameOver();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("You lost");
    }
}
