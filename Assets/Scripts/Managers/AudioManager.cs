using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClipsSO _audioClipsSO;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _soundLoopSource;

    private float _soundVolume = 1f;
    private float _musicVolume = .3f;

    private bool _playingThrustSound;

    private bool _muteMusicSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one AudioManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _soundVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, _soundVolume);
        _musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, _musicVolume);
    }

    private void Start()
    {
        _soundSource.volume = _soundVolume;
        _soundLoopSource.volume = _soundVolume;
        _musicSource.volume = _musicVolume;
    }

    public void MuteMusicSource()
    {
        _muteMusicSource = true;
    }

    private void Update()
    {
        if (_muteMusicSource)
        {
            _musicSource.volume -= Time.deltaTime;

            if (_musicSource.volume <= 0)
            {
                _muteMusicSource = false;
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip, _soundVolume);
    }

    public void PlayShootSound()
    {
        AudioClip shootSound = _audioClipsSO.shoot[Random.Range(0, _audioClipsSO.shoot.Length)];
        _soundSource.PlayOneShot(shootSound, _soundVolume);
    }

    public void PlayExplosionSound()
    {
        AudioClip explosionSound = _audioClipsSO.explosion[Random.Range(0, _audioClipsSO.explosion.Length)];
        _soundSource.PlayOneShot(explosionSound, _soundVolume);
    }

    public void PlayThrustSound()
    {
        if (!_playingThrustSound)
        {
            _playingThrustSound = true;

            _soundLoopSource.clip = _audioClipsSO.thrust;
            _soundLoopSource.volume = _soundVolume;
            _soundLoopSource.Play();
        }
    }

    public void ResetThrustSoundEffect()
    {
        _soundLoopSource.Stop();
        _playingThrustSound = false;
    }

    public void PlayCountdownSound()
    {
        AudioClip countdownSound = _audioClipsSO.countdownSound;
        _soundSource.PlayOneShot(countdownSound, _soundVolume);
    }

    public void PlayGameOverSound()
    {
        AudioClip gameOverSound = _audioClipsSO.gameOver;
        _soundSource.PlayOneShot(gameOverSound, _soundVolume);
    }

    public void PlaySelectSound()
    {
        AudioClip selectSound = _audioClipsSO.select;
        _soundSource.PlayOneShot(selectSound, _soundVolume);
    }

    public void PlayGameMusic()
    {
        _musicSource.clip = _audioClipsSO.gameBackground;

        _musicSource.volume = _musicVolume;
        _musicSource.Play();
    }

    public void PlayMenuMusic()
    {
        _musicSource.clip = _audioClipsSO.menuBackground;

        _musicSource.volume = _musicVolume;
        _musicSource.Play();
    }

    public void StopMusicSource()
    {
        _musicSource.Stop();
    }

    #region Volume

    public void ChangeSoundVolume()
    {
        _soundVolume += .1f;

        if (_soundVolume > 1f)
        {
            _soundVolume = 0f;
        }

        _soundSource.volume = _soundVolume;
        _soundLoopSource.volume = _soundVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, _soundVolume);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume()
    {
        _musicVolume += .1f;

        if (_musicVolume > 1f)
        {
            _musicVolume = 0f;
        }

        _musicSource.volume = _musicVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, _musicVolume);
        PlayerPrefs.Save();
    }

    public float GetSoundVolume()
    {
        return _soundVolume;
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    #endregion
}
