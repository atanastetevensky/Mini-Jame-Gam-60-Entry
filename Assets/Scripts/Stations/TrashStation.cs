using UnityEngine;

public class TrashStation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        Inventory inventory = player.GetComponent<Inventory>();

        float totalPoints = inventory.GetTotalPoints();
        inventory.EmptyInventory();

        player.properties.currentPoints += totalPoints;
    }
}
