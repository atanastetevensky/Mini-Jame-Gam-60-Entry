using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private GameObject _storageEntryPrefab;

    public GameObject StorageEntryPrefab => _storageEntryPrefab;
}
