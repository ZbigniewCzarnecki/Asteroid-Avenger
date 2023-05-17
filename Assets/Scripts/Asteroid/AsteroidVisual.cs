using UnityEngine;

public class AsteroidVisual : MonoBehaviour
{
    [SerializeField] private Sprite[] _spritesArray;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = RandomSprite();
    }

    private Sprite RandomSprite()
    {
        return _spritesArray[Random.Range(0, _spritesArray.Length)];
    }
}
