using UnityEngine;

[CreateAssetMenu()]
public class AudioClipsSO : ScriptableObject
{
    [Header("Player")]
    public AudioClip[] shoot;
    public AudioClip thrust;

    [Header("Asteroids")]
    public AudioClip[] explosion;

    [Header("UI")]
    public AudioClip select;
    public AudioClip countdownSound;
    public AudioClip startSound;
    public AudioClip gameOver;

    [Header("Music")]
    public AudioClip gameBackground;
    public AudioClip menuBackground;
}
