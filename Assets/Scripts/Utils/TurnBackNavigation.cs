using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TurnBackNavigation : MonoBehaviour
{
    [SerializeField] private InputActionReference _navigateInputActionReference;

    [SerializeField] private Selectable _firstSelected;

    private void OnEnable()
    {
        _navigateInputActionReference.action.performed += OnPerformed;
        _firstSelected.Select();
    }

    private void OnDisable()
    {
        _navigateInputActionReference.action.performed -= OnPerformed;
    }

    void OnPerformed(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.activeInHierarchy || !EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().IsInteractable())
        {
            foreach (Selectable selectable in FindObjectsOfType<Selectable>())
            {
                if (selectable.IsInteractable() && selectable.isActiveAndEnabled)
                {
                    EventSystem.current.SetSelectedGameObject(selectable.gameObject);
                }
            }
        }
    }
}