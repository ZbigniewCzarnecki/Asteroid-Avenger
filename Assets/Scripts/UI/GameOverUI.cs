using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _menuButton;

    [SerializeField] private TextMeshProUGUI _gameOverScoreText;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        _menuButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();

            AudioManager.Instance.PlayGameOverSound();

            _gameOverScoreText.text = "Score: " + PlayerScore.Instance.GetScore().ToString();
        }
        else
        {
            Hide();
        }

    }

    private void Show()
    {
        gameObject.SetActive(true);
        _playButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
