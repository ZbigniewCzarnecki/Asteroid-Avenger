using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Transform _healthPrefab;
    [SerializeField] private Transform _parent;

    private readonly List<Transform> _healthPrefabsList = new();

    private void Start()
    {
        PlayerHealth.Instance.OnIncreaseHealth += PlayerHealth_OnIncreaseHealth;
        PlayerHealth.Instance.OnDecreaseHealth += PlayerHealth_OnDecreaseHealth;
    }

    private void PlayerHealth_OnIncreaseHealth(object sender, EventArgs e)
    {
        SpawnHealthPrefab();
    }

    private void PlayerHealth_OnDecreaseHealth(object sender, EventArgs e)
    {
        DestroyHealthPrefab();
    }

    private void SpawnHealthPrefab()
    {
        Transform playerHealth = Instantiate(_healthPrefab, _parent);
        _healthPrefabsList.Add(playerHealth);
    }

    private void DestroyHealthPrefab()
    {
        if (_healthPrefabsList.Count == 0) return;

        Transform playerHealth = _healthPrefabsList[^1];
        _healthPrefabsList.Remove(playerHealth);
        Destroy(playerHealth.gameObject);
    }
}
