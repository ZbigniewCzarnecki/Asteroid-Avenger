using UnityEngine;

public class AsteroidSmall : Asteroid
{
    public override void Hit()
    {
        //If DeactivateAsteroid will be triggered this AsteroidSmall will be added to BigAsteroidPool!!!
        //AsteroidSpawner.Instance.DeactivateAsteroid(this);

        AsteroidDestructionFeedback();

        PlayerScore.Instance.AddScore(200);

        AsteroidSpawner.Instance.DecreaseAsteroidAmount();

        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            AsteroidDestructionFeedback();

            playerHealth.DecreaseHealth();

            AsteroidSpawner.Instance.DecreaseAsteroidAmount();

            Destroy(gameObject);
        }
    }
}
