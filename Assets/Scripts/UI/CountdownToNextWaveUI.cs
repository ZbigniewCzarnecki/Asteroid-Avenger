using System;
using TMPro;
using UnityEngine;

public class CountdownToNextWaveUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI _countdownText;
    private Animator _animator;

    private int _previousCountdownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnWaveStart += GameManager_OnWaveStart;
        GameManager.Instance.OnWaveEnd += GameManager_OnWaveEnd;

        Hide();
    }

    private void GameManager_OnWaveStart(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnWaveEnd(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToNextWaveTimer());
        _countdownText.text = countdownNumber.ToString("0");

        if (_previousCountdownNumber != countdownNumber)
        {
            _previousCountdownNumber = countdownNumber;
            _animator.SetTrigger(NUMBER_POPUP);
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
