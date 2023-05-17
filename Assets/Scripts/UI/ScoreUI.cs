using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    private void Start()
    {
        PlayerScore.Instance.OnScoreChange += PlayerScore_OnScoreChange;

        UpdateScoreText(0);
    }

    private void PlayerScore_OnScoreChange(object sender, PlayerScore.OnScoreChangeEventArgs e)
    {
        UpdateScoreText(e.scoreAll);
    }

    private void UpdateScoreText(object score)
    {
        _scoreText.text = score.ToString();
    }
}
