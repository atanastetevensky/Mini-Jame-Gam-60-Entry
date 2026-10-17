using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private GameObject _storageEntryPrefab;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory playerInventory = collision.GetComponent<Inventory>();
        if (playerInventory == null) return;

        if(playerInventory.AddEntry(_storageEntryPrefab))
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
