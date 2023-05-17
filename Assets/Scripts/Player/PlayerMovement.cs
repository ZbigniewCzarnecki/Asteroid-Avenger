using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _thrustForce = 6f;
    [SerializeField] private float _rotationForce = 15f;

    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePlaying() || GameManager.Instance.IsGamePaused())
        {
            return;
        }

        if (InputManager.Instance.Thrust())
        {
            _rb2d.AddForce(transform.up * _thrustForce);
        }

        if (InputManager.Instance.RotateLeft())
        {
            _rb2d.AddTorque(1 * _rotationForce);
        }
        else
        {
            _rb2d.angularVelocity *= 0.95f;
        }

        if (InputManager.Instance.RotateRight())
        {
            _rb2d.AddTorque(-1 * _rotationForce);
        }
        else
        {
            _rb2d.angularVelocity *= 0.95f;
        }
    }
}
