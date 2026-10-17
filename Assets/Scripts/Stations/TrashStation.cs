using UnityEngine;

public class TrashStation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Inventory inventory = player.GetComponent<Inventory>();

        float totalPoints = inventory.GetTotalPoints();
        inventory.EmptyInventory();

        player.properties.currentPoints += totalPoints;
    }
}
