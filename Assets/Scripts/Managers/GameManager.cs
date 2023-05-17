using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public event EventHandler<int> OnWaveChange;
    public event EventHandler OnWaveStart;
    public event EventHandler OnWaveEnd;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;

    private float _countdownToStartTimer = 3f;

    private float _countdownToNextWave = 3f;
    private readonly float _countdownToNextWaveMax = 3f;

    private bool _isGamePaused = false;

    private bool _isWaveSpawned = false;
    private int _waveCount = 0;

    private int _asteroidAmount;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one GameManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _state = State.WaitingToStart;
    }

    private void Start()
    {
        InputManager.Instance.OnPausePerformed += InputManager_OnPausePerformed;
        InputManager.Instance.OnInteractPerformed += InputManager_OnInteractPerformed;

        PlayerHealth.Instance.OnPlayerDie += PlayerHealth_OnPlayerDie;

        AsteroidSpawner.Instance.OnAsteroidAmountChange += Instance_OnAsteroidAmountChange;
        AsteroidSpawner.Instance.OnAllAsteroidsDestroyed += OnAllAsteroidsDestroyed;
    }

    private void OnAllAsteroidsDestroyed(object sender, EventArgs e)
    {
        OnWaveEnd?.Invoke(this, EventArgs.Empty);
    }

    private void Instance_OnAsteroidAmountChange(object sender, int asteroidAmount)
    {
        _asteroidAmount = asteroidAmount;
    }

    private void InputManager_OnInteractPerformed(object sender, EventArgs e)
    {
        if (_state == State.WaitingToStart && !IsGamePaused())
        {
            _state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void InputManager_OnPausePerformed(object sender, EventArgs e)
    {
        if (IsGameOver() || IsCountdownToStart()) return;

        TogglePauseGame();
    }

    private void PlayerHealth_OnPlayerDie(object sender, EventArgs e)
    {
        _state = State.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused)
        {
            Time.timeScale = 0;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                Time.timeScale = 1f;
                _countdownToStartTimer -= Time.deltaTime;

                if (_countdownToStartTimer < 0f)
                {
                    _state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:

                if (_asteroidAmount <= 0)
                {
                    _countdownToNextWave -= Time.deltaTime;

                    if (_countdownToNextWave <= 0)
                    {
                        _countdownToNextWave = _countdownToNextWaveMax;
                        ResetWaveSpawn();
                    }

                    if (!_isWaveSpawned)
                    {
                        _isWaveSpawned = true;

                        _waveCount++;

                        AsteroidSpawner.Instance.SpawnBigAsteroids(_waveCount);

                        OnWaveChange?.Invoke(this, _waveCount);

                        OnWaveStart?.Invoke(this, EventArgs.Empty);
                    }
                }
                break;
            case State.GameOver:

                break;
        }
    }

    public bool IsCountdownToStart()
    {
        return _state == State.CountdownToStart;
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }

    public float GetCountdownToNextWaveTimer()
    {
        return _countdownToNextWave;
    }

    public bool IsGamePaused()
    {
        return _isGamePaused;
    }

    public bool IsWaveSpawned()
    {
        return _isWaveSpawned;
    }

    public void ResetWaveSpawn()
    {
        _isWaveSpawned = false;
    }
}