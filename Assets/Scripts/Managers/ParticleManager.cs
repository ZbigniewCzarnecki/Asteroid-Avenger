using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    [SerializeField] private ParticleEffectsSO _particleEffectsSO;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one ParticleManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SpawnAsteroidDestroyParticle(Vector3 spawnPosition)
    {
        Instantiate(_particleEffectsSO.asteroidExplosion, spawnPosition, Quaternion.identity);
    }

    public void SpawnPlayerShipDestroyParticle(Vector3 spawnPosition)
    {
        Instantiate(_particleEffectsSO.playerShipExplosion, spawnPosition, Quaternion.identity);
    }
}
