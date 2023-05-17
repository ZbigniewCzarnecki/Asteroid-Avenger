using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveText;

    private void Start()
    {
        GameManager.Instance.OnWaveChange += WaveManager_OnWaveChange;

        UpdateWaveUI(1);
    }

    private void WaveManager_OnWaveChange(object sender, int waveCount)
    {
        UpdateWaveUI(waveCount);
    }

    private void UpdateWaveUI(int waveCount)
    {
        _waveText.text = waveCount.ToString("00");
    }
}
