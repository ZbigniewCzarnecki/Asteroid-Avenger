using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get; private set; }

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private UnityEngine.Transform _firePoint;
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private int _maxActiveBullets = 4;

    private ObjectPool<Bullet> _bulletPool;

    private bool _readyToShoot = true;
    private bool _waveSpawned = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one BulletSpawner!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnWaveStart += GameManager_OnWaveStart;
        GameManager.Instance.OnWaveEnd += GameManager_OnWaveEnd;

        PoolBullets();
    }

    private void GameManager_OnWaveStart(object sender, System.EventArgs e)
    {
        _waveSpawned = true;
    }

    private void GameManager_OnWaveEnd(object sender, System.EventArgs e)
    {
        _waveSpawned = false;
    }

    private void PoolBullets()
    {
        _bulletPool = new ObjectPool<Bullet>(() =>
        {
            return Instantiate(_bulletPrefab);
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
            bullet.LaunchBullet(transform.up);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet.gameObject);
        }, true, _maxActiveBullets, _maxActiveBullets);
    }

    private void Update()
    {
        if (!_waveSpawned || !GameManager.Instance.IsGamePlaying() || GameManager.Instance.IsGamePaused())
        {
            return;
        }

        if (InputManager.Instance.Shoot() && _readyToShoot)
        {
            _readyToShoot = false;

            if (_bulletPool.CountActive < _maxActiveBullets)
            {
                _bulletPool.Get();

                AudioManager.Instance.PlayShootSound();
            }

            Invoke(nameof(ResetShoot), _fireRate);
        }
    }

    private void ResetShoot()
    {
        _readyToShoot = true;
    }

    public void DeactivateBullet(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }
}
