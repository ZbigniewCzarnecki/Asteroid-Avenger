using UnityEngine;

public class WaitingToStartUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
