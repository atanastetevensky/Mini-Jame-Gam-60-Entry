using UnityEngine;

public class RefuelStation : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
        if (player == null) return;
        Inventory inventory = player.GetComponent<Inventory>();
        PlayerFuel fuel = player.GetComponent<PlayerFuel>();

        float totalPoints = inventory.GetTotalPoints();
        inventory.EmptyInventory();
        fuel.FullRefuel();


        player._properties.currentPoints += totalPoints;
    }
}
