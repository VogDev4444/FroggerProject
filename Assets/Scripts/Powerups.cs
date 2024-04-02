using UnityEngine;

public enum PowerUpType
{
    SpeedBoost,
    HealthBoost,
    DamageBoost,
    Shield
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;

    // Adjust these values as needed for each power-up type
    public float speedBoostAmount = 2f;
    public float healthBoostAmount = 25f;
    public float damageBoostMultiplier = 1.5f;
    public float shieldDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUp(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void ApplyPowerUp(GameObject player)
    {
        switch (type)
        {
            case PowerUpType.SpeedBoost:
                // Apply speed boost logic
                
                break;
            case PowerUpType.HealthBoost:
                // Apply health boost logic
                
                break;
            case PowerUpType.DamageBoost:
                // Apply damage boost logic
               
                break;
            case PowerUpType.Shield:
                // Apply shield logic
                
                break;
            default:
                Debug.LogWarning("Unhandled power-up type: " + type);
                break;
        }
    }
}