using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance { get; private set; }

    public event EventHandler<OnScoreChangeEventArgs> OnScoreChange;
    public class OnScoreChangeEventArgs
    {
        public int scoreAll;
        public int scoreCurrentlyAdded;
    }

    private int _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerScore!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddScore(int score)
    {
        _score += score;

        OnScoreChange.Invoke(this, new OnScoreChangeEventArgs
        {
            scoreAll = _score,
            scoreCurrentlyAdded = score
        });
    }

    public int GetScore()
    {
        return _score;
    }
}
