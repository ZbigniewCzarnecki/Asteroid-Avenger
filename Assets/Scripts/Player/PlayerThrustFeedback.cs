using System;
using UnityEngine;

public class PlayerThrustFeedback : MonoBehaviour
{
    [SerializeField] private GameObject _thrustVisualEffect;

    private void Start()
    {
        InputManager.Instance.OnThrustStart += InputManager_OnThrustStart;
        InputManager.Instance.OnThrustEnd += InputManager_OnThrustEnd;

        Hide();
    }

    private void InputManager_OnThrustStart(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying() || GameManager.Instance.IsGamePaused())
        {
            return;
        }

        AudioManager.Instance.PlayThrustSound();

        Show();
    }

    private void InputManager_OnThrustEnd(object sender, EventArgs e)
    {
        AudioManager.Instance.ResetThrustSoundEffect();

        Hide();
    }

    private void Show()
    {
        _thrustVisualEffect.SetActive(true);
    }

    private void Hide()
    {
        _thrustVisualEffect.SetActive(false);
    }
}