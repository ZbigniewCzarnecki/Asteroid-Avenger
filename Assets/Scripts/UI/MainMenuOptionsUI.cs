using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptionsUI : MonoBehaviour
{
    public static MainMenuOptionsUI Instance { get; private set; }

    private Action OnCloseButtonAction;

    [SerializeField] private Button _soundEffectsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _closeButton;

    [SerializeField] private TextMeshProUGUI _soundEffectsButtonText;
    [SerializeField] private TextMeshProUGUI _musicButtonText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one MainMenuOptionsUI! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _soundEffectsButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeSoundVolume();
            UpdateSoundEffectButtonText();
        });

        _musicButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.ChangeMusicVolume();
            UpdateMusicEffectButtonText();
        });

        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            OnCloseButtonAction();
        });
    }

    private void Start()
    {
        UpdateSoundEffectButtonText();
        UpdateMusicEffectButtonText();

        Hide();
    }

    private void UpdateSoundEffectButtonText()
    {
        _soundEffectsButtonText.text = "SFX Volume: " + Mathf.Round(AudioManager.Instance.GetSoundVolume() * 10f).ToString("0");
    }

    private void UpdateMusicEffectButtonText()
    {
        _musicButtonText.text = "Music Volume: " + Mathf.Round(AudioManager.Instance.GetMusicVolume() * 10f).ToString("0");
    }

    public void Show(Action onCloseButtonAction)
    {
        gameObject.SetActive(true);

        OnCloseButtonAction = onCloseButtonAction;

        _soundEffectsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
