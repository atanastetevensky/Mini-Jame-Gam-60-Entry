using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<StorageEntry> _inventoryList = new List<StorageEntry>();

    public bool AddEntry(GameObject entryPrefab)
    {
        StorageEntry entry = Instantiate(entryPrefab).GetComponent<StorageEntry>();
        _inventoryList.Add(entry);
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
