using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuRebindUI : MonoBehaviour
{
    [Header("Keyboard")]
    [SerializeField] private Button _thrustButton;
    [SerializeField] private Button _rotateLeftButton;
    [SerializeField] private Button _rotateRightButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private TextMeshProUGUI _thrustButtonText;
    [SerializeField] private TextMeshProUGUI _rotateLeftButtonText;
    [SerializeField] private TextMeshProUGUI _rotateRightButtonText;
    [SerializeField] private TextMeshProUGUI _shootButtonText;
    [SerializeField] private TextMeshProUGUI _interactButtonText;

    [Header("Gamepad")]
    [SerializeField] private Button _gamepadThrustButton;
    [SerializeField] private Button _gamepadRotateLeftButton;
    [SerializeField] private Button _gamepadRotateRightButton;
    [SerializeField] private Button _gamepadShootButton;
    [SerializeField] private Button _gamepadInteractButton;
    [SerializeField] private TextMeshProUGUI _gamepadThrustButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadRotateLeftButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadRotateRightButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadShootButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractButtonText;

    [Header("RebindKeyUI")]
    [SerializeField] private Transform _rebindUI;

    private void Awake()
    {
        _thrustButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Thrust); });
        _rotateLeftButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Rotate_Left); });
        _rotateRightButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Rotate_Right); });
        _shootButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Shoot); });
        _interactButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Interact); });

        _gamepadThrustButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Thrust); });
        _gamepadRotateLeftButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Rotate_Left); });
        _gamepadRotateRightButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Rotate_Right); });
        _gamepadShootButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Shoot); });
        _gamepadInteractButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Interact); });
    }

    private void Start()
    {
        UpdateRebindingButtonsText();
    }

    private void RebindBinding(InputManager.Binding binding)
    {
        ShowRebindUI();
        InputManager.Instance.RebindBinding(binding, () =>
        {
            HideRebindUI();
            UpdateRebindingButtonsText();
        });
    }

    private void UpdateRebindingButtonsText()
    {
        //Keyboard
        _thrustButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Thrust);
        _rotateLeftButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Rotate_Left);
        _rotateRightButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Rotate_Right);
        _shootButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Shoot);
        _interactButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);

        //Gamepad
        _gamepadThrustButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Thrust);
        _gamepadRotateLeftButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Rotate_Left);
        _gamepadRotateRightButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Rotate_Right);
        _gamepadShootButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Shoot);
        _gamepadInteractButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Interact);
    }

    public void ShowRebindUI()
    {
        _rebindUI.gameObject.SetActive(true);
    }

    public void HideRebindUI()
    {
        _rebindUI.gameObject.SetActive(false);
    }
}
