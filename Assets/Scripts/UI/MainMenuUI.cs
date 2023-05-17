using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.GameScene);
        });

        _optionsButton.onClick.AddListener(() =>
        {
            Hide();
            MainMenuOptionsUI.Instance.Show(Show); ;
        });

        _exitButton.onClick.AddListener(() =>
        {
            QuitTheGame();
        });
    }

    private void QuitTheGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _playButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
