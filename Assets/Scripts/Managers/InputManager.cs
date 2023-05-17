using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static InputManager Instance { get; private set; }

    public event EventHandler OnPausePerformed;
    public event EventHandler OnInteractPerformed;
    public event EventHandler OnThrustStart;
    public event EventHandler OnThrustEnd;

    public event EventHandler OnKeyRebind;

    public enum Binding
    {
        Thrust,
        Rotate_Left,
        Rotate_Right,
        Shoot,
        Interact,
        Gamepad_Thrust,
        Gamepad_Rotate_Left,
        Gamepad_Rotate_Right,
        Gamepad_Shoot,
        Gamepad_Interact,
    }

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one InputManager!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _playerInputActions = new PlayerInputActions();

        //Load Binding Overrides after creating new PlayerInputActions and before Enableing it!
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Pause.performed += Pause_Performed;
        _playerInputActions.Player.Interact.performed += Interact_Performed;

        _playerInputActions.Player.Thrust.performed += Thrust_Performed;
        _playerInputActions.Player.Thrust.canceled += Thrust_Canceled;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Pause.performed -= Pause_Performed;
        _playerInputActions.Player.Interact.performed -= Interact_Performed;

        _playerInputActions.Player.Thrust.performed -= Thrust_Performed;
        _playerInputActions.Player.Thrust.canceled -= Thrust_Canceled;

        _playerInputActions.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Thrust_Performed(InputAction.CallbackContext obj)
    {
        OnThrustStart?.Invoke(this, EventArgs.Empty);
    }

    private void Thrust_Canceled(InputAction.CallbackContext obj)
    {
        OnThrustEnd?.Invoke(this, EventArgs.Empty);
    }

    public bool RotateLeft()
    {
        return _playerInputActions.Player.RotationLeft.IsPressed();
    }

    public bool RotateRight()
    {
        return _playerInputActions.Player.RotationRight.IsPressed();
    }

    public bool Thrust()
    {
        return _playerInputActions.Player.Thrust.IsPressed();
    }

    public bool Shoot()
    {
        return _playerInputActions.Player.Shoot.IsPressed();
    }

    #region Rebind

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.Rotate_Left => _playerInputActions.Player.RotationLeft.bindings[0].ToDisplayString(),
            Binding.Rotate_Right => _playerInputActions.Player.RotationRight.bindings[0].ToDisplayString(),
            Binding.Shoot => _playerInputActions.Player.Shoot.bindings[0].ToDisplayString(),
            Binding.Interact => _playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.Gamepad_Thrust => _playerInputActions.Player.Thrust.bindings[1].ToDisplayString(),
            Binding.Gamepad_Rotate_Left => _playerInputActions.Player.RotationLeft.bindings[1].ToDisplayString(),
            Binding.Gamepad_Rotate_Right => _playerInputActions.Player.RotationRight.bindings[1].ToDisplayString(),
            Binding.Gamepad_Shoot => _playerInputActions.Player.Shoot.bindings[1].ToDisplayString(),
            Binding.Gamepad_Interact => _playerInputActions.Player.Interact.bindings[1].ToDisplayString(),
            _ => _playerInputActions.Player.Thrust.bindings[0].ToDisplayString(),
        };
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        _playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Thrust:
                inputAction = _playerInputActions.Player.Thrust;
                bindingIndex = 0;
                break;
            case Binding.Rotate_Left:
                inputAction = _playerInputActions.Player.RotationLeft;
                bindingIndex = 0;
                break;
            case Binding.Rotate_Right:
                inputAction = _playerInputActions.Player.RotationRight;
                bindingIndex = 0;
                break;
            case Binding.Shoot:
                inputAction = _playerInputActions.Player.Shoot;
                bindingIndex = 0;
                break;
            case Binding.Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Thrust:
                inputAction = _playerInputActions.Player.Thrust;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Rotate_Left:
                inputAction = _playerInputActions.Player.RotationLeft;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Rotate_Right:
                inputAction = _playerInputActions.Player.RotationRight;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Shoot:
                inputAction = _playerInputActions.Player.Shoot;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                _playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, _playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnKeyRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

    #endregion
}