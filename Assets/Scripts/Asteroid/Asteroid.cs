using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable
{
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    public virtual void Hit()
    {
        AsteroidDestructionFeedback(2);

        PlayerScore.Instance.AddScore(100);

        AsteroidSpawner.Instance.SpawnSmallAsteroids(transform.position);
        AsteroidSpawner.Instance.DecreaseAsteroidAmount();
        AsteroidSpawner.Instance.DeactivateAsteroid(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            AsteroidDestructionFeedback(2);

            playerHealth.DecreaseHealth();

            AsteroidSpawner.Instance.DeactivateAsteroid(this);
            AsteroidSpawner.Instance.DecreaseAsteroidAmount();
        }
    }

    public void LaunchAsteroid(Vector2 launchDirection, float speed = 1f)
    {
        _rb2d.AddForce(launchDirection * speed, ForceMode2D.Impulse);
    }

    protected void AsteroidDestructionFeedback(float screenShake = 1f)
    {
        ScreenShake.Instance.Shake(screenShake);
        AudioManager.Instance.PlayExplosionSound();
        ParticleManager.Instance.SpawnAsteroidDestroyParticle(transform.position);
    }
}
