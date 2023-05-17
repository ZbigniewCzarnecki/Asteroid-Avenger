using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event EventHandler OnPlayerDie;
    public event EventHandler OnIncreaseHealth;
    public event EventHandler OnDecreaseHealth;

    public static PlayerHealth Instance { get; private set; }

    [SerializeField] private GameObject _playerShipSprite;
    private CircleCollider2D _playerCollider;

    [SerializeField] private int _healthOnStart = 3;
    private int _currentHealth;

    private int _scoreNeededToAddHealth = 0;
    private readonly int _scoreNeededToAddHealthMax = 10000;

    private void Awake()
    {
        _playerCollider = GetComponent<CircleCollider2D>();

        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerHealth!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        PlayerScore.Instance.OnScoreChange += PlayerScore_OnScoreChange;

        for (int i = 0; i < _healthOnStart; i++)
        {
            AddHealth();
        }
    }

    private void PlayerScore_OnScoreChange(object sender, PlayerScore.OnScoreChangeEventArgs e)
    {
        _scoreNeededToAddHealth += e.scoreCurrentlyAdded;

        if (_scoreNeededToAddHealth >= _scoreNeededToAddHealthMax)
        {
            _scoreNeededToAddHealth -= _scoreNeededToAddHealthMax;
            AddHealth();
        }
    }

    private void AddHealth()
    {
        _currentHealth++;

        OnIncreaseHealth?.Invoke(this, EventArgs.Empty);
    }

    public void DecreaseHealth()
    {
        _currentHealth--;

        OnDecreaseHealth?.Invoke(this, EventArgs.Empty);

        if (_currentHealth <= 0)
        {
            ParticleManager.Instance.SpawnPlayerShipDestroyParticle(transform.position);

            _playerShipSprite.SetActive(false);

            _playerCollider.enabled = false;

            OnPlayerDie?.Invoke(this, EventArgs.Empty);
        }
    }
}
