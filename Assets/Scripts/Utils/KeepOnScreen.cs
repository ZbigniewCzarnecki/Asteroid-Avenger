using UnityEngine;

public class KeepOnScreen : MonoBehaviour
{
    Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 playerInViewportPosition = _camera.WorldToViewportPoint(transform.position);

        playerInViewportPosition = playerInViewportPosition switch
        {
            { x: < 0f } => new Vector3(1f, playerInViewportPosition.y, playerInViewportPosition.z),
            { x: > 1f } => new Vector3(0f, playerInViewportPosition.y, playerInViewportPosition.z),
            { y: < 0f } => new Vector3(playerInViewportPosition.x, 1f, playerInViewportPosition.z),
            { y: > 1f } => new Vector3(playerInViewportPosition.x, 0f, playerInViewportPosition.z),
            _ => playerInViewportPosition
        };

        transform.position = _camera.ViewportToWorldPoint(playerInViewportPosition);
    }
}
