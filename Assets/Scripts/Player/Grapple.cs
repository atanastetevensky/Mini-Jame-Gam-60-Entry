using System;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _speed;

    public float Range => _range;
    public float speed => _speed;

    private void FixedUpdate()
    {
        Collider2D nearest = GetNearestComponent();
        if (nearest == null) return;

        Vector2 targetPosition = nearest.transform.position;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _range);
    }

    private Collider2D GetNearestComponent()
    {
        Collider2D[] allWithinRange = Physics2D.OverlapCircleAll(transform.position, _range);
        if (allWithinRange.Length == 0) return null;

        Collider2D[] collectiblesWithinRange = Array.FindAll(
            allWithinRange,
            coll => coll.GetComponent<Collectible>() != null);

        if (collectiblesWithinRange.Length == 0) return null;

        return Utils.Nearest(transform.position, allWithinRange);
    }
}
