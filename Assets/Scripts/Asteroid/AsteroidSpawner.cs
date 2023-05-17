using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    private static int _asteroidAmount = 0;

    public static AsteroidSpawner Instance { get; private set; }

    public event EventHandler<int> OnAsteroidAmountChange;
    public event EventHandler OnAllAsteroidsDestroyed;

    [SerializeField] private Asteroid _asteroidBigPrefab;
    [SerializeField] private Asteroid _asteroidSmallPrefab;

    [SerializeField] private float _trajectoryVariance = 15f;
    [SerializeField] private float _spawnDistance = 15f;

    private ObjectPool<Asteroid> _asteroidBigPool;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one AsteroidSpawner!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        PoolBigAsteroids();
    }

    private void PoolBigAsteroids()
    {
        _asteroidBigPool = new ObjectPool<Asteroid>(() =>
        {
            return Instantiate(_asteroidBigPrefab);
        }, asteroid =>
        {
            asteroid.gameObject.SetActive(true);
            asteroid.transform.SetPositionAndRotation(GetSpawnPosition(), GetRandomRotation());
            asteroid.LaunchAsteroid(GetRandomRotation() * -GetRandomPointOnCircle());
        }, asteroid =>
        {
            asteroid.gameObject.SetActive(false);
        }, asteroid =>
        {
            Destroy(asteroid.gameObject);
        }, true, 10, 20);
    }

    public void SpawnBigAsteroids(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            _asteroidBigPool.Get();
            _asteroidAmount++;
            OnAsteroidAmountChange?.Invoke(this, _asteroidAmount);
        }
    }

    public void SpawnSmallAsteroids(Vector3 position, float speed = 2f)
    {
        for (int i = 0; i < 2; i++)
        {
            Asteroid asteroid = Instantiate(_asteroidSmallPrefab, position, GetRandomRotation());
            asteroid.LaunchAsteroid(GetRandomRotation() * -GetRandomPointOnCircle(), speed);
            _asteroidAmount++;
            OnAsteroidAmountChange?.Invoke(this, _asteroidAmount);
        }
    }

    private Vector3 GetRandomPointOnCircle()
    {
        Vector3 point = Random.insideUnitCircle.normalized * _spawnDistance;
        return point;
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = transform.position + GetRandomPointOnCircle();
        return spawnPosition;
    }

    private Quaternion GetRandomRotation()
    {
        float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        return rotation;
    }

    public void DeactivateAsteroid(Asteroid asteroid)
    {
        _asteroidBigPool.Release(asteroid);
    }

    public void DecreaseAsteroidAmount()
    {
        _asteroidAmount--;

        CheckIfAllAsteroidsDestroyed();

        OnAsteroidAmountChange?.Invoke(this, _asteroidAmount);
    }

    private void CheckIfAllAsteroidsDestroyed()
    {
        if (_asteroidAmount <= 0)
        {
            OnAllAsteroidsDestroyed.Invoke(this, EventArgs.Empty);
        }
    }

    public static void ResetStaticData()
    {
        _asteroidAmount = 0;
    }
}
