using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private float _storageSizeMultiplier;
    [SerializeField] private GameObject _inventoryParent;
    [SerializeField] private GameObject _inventorySpawnPoint;

    private List<StorageEntry> _inventoryList = new List<StorageEntry>();

    public bool AddCollectible(Collectible collectible)
    {
        GameObject entryPrefab = collectible.StorageEntryPrefab;
        CircleCollider2D prefabCollider = entryPrefab.GetComponent<CircleCollider2D>();

        float actualRadius = prefabCollider.radius * entryPrefab.transform.lossyScale.x * _storageSizeMultiplier;
        Vector3 spawnLocation = new Vector3(0f, -actualRadius, 0f) + _inventorySpawnPoint.transform.position;

        Collider2D[] collisions = Physics2D.OverlapCircleAll(spawnLocation,
            actualRadius,
            LayerMask.GetMask("Inventory"));

        if (collisions.Length > 0)
        {
            Debug.Log($"Collisions detected: {collisions[0].name}");
            return false;
        }

        Debug.Log($"Testing");
        GameObject entry = Instantiate(collectible.StorageEntryPrefab, _inventoryParent.transform);
        entry.transform.position = spawnLocation;
        entry.transform.localScale = Vector2.one * _storageSizeMultiplier;

        _inventoryList.Add(entry.GetComponent<StorageEntry>());
        return true;
    }

    public float GetTotalPoints()
    {
        float totalPoints = 0f;

        foreach (StorageEntry entry in _inventoryList)
        {
            totalPoints += entry.Points;
        }
        return totalPoints;
    }

    public void EmptyInventory()
    {
        foreach (StorageEntry entry in _inventoryList)
        {
            Destroy(entry.gameObject);
        }

        _inventoryList = new List<StorageEntry>();
    }
}
