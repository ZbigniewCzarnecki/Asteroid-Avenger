using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class UpdateSelectedUI : MonoBehaviour
{
    public static event Action<GameObject, GameObject> OnSelectionUpdated;

    private static void SelectionUpdate(GameObject newSelectedGameObject, GameObject previousSelectedGameObject)
    {
        AudioManager.Instance.PlaySelectSound();

        OnSelectionUpdated?.Invoke(newSelectedGameObject, previousSelectedGameObject);
    }

    [SerializeField] private EventSystem eventSystem;

    private GameObject m_LastSelectedGameObject;

    private void Update()
    {
        var currentSelectedGameObject = eventSystem.currentSelectedGameObject;
        if (currentSelectedGameObject != m_LastSelectedGameObject)
        {
            SelectionUpdate(currentSelectedGameObject, m_LastSelectedGameObject);
            m_LastSelectedGameObject = currentSelectedGameObject;
        }
    }
}