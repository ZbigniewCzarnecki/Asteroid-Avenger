using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        AsteroidSpawner.ResetStaticData();
    }
}
