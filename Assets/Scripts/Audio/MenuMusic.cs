using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }
}
