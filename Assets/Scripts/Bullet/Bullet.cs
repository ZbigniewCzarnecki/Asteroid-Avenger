using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    private float _lifeTime;
    private readonly float _lifeTimeMax = 1f;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.Instance.OnWaveEnd += GameManager_OnWaveEnd;
    }

    private void GameManager_OnWaveEnd(object sender, System.EventArgs e)
    {
        if (this.gameObject.activeInHierarchy)
        {
            _lifeTime = _lifeTimeMax;
        }
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;

        if (_lifeTime >= _lifeTimeMax)
        {
            _lifeTime = 0f;
            BulletSpawner.Instance.DeactivateBullet(this);
        }
    }

    public void LaunchBullet(Vector2 launchDirection, float speed = 15f)
    {
        _rb2d.AddForce(launchDirection * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHitable hitable) && gameObject.activeInHierarchy)
        {
            this._lifeTime = 0f;
            ScreenShake.Instance.Shake();
            hitable.Hit();
            BulletSpawner.Instance.DeactivateBullet(this);
        }
    }
}
