using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _spawnOffset;
    [SerializeField] private GameObject _grappleHookPrefab;

    public float Range => _range;
    public float Speed => _speed;

    public bool isGrappling = false;

    private Collider2D nearestCollider;

    private void FixedUpdate()
    {
        nearestCollider = GetNearestComponent();
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

        return Utils.Nearest(transform.position, collectiblesWithinRange);
    }

    void OnGrapple(InputValue inputValue)
    {
        if (isGrappling) return;
        if (nearestCollider == null) return;

        GameObject hookInstance = GameObject.Instantiate(_grappleHookPrefab, (Vector2)transform.position + _spawnOffset, Quaternion.identity, this.transform);
        hookInstance.GetComponent<GrapplingHook>().targetObject = nearestCollider.gameObject;
        isGrappling = true;
    }
}
