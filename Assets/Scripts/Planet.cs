using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float _gravityThreshold;
    [SerializeField] private float _maxGravity;
    [SerializeField] private AnimationCurve _gravityCurve;
    [SerializeField] private LayerMask _layerMask;

    public float GravityThreshold => _gravityThreshold;
    public float MaxGravity => _maxGravity;
    public AnimationCurve GravityCurve => _gravityCurve;
    public float planetRadius {  get; private set; }
    public float gravityRadius { get; private set; }
    public List<Collider2D> Colliders => _colliders;

    private CircleCollider2D _circleColl;
    private ContactFilter2D _filter;
    private readonly List<Collider2D> _colliders = new List<Collider2D>();

    void Start()
    {
        _circleColl = GetComponent<CircleCollider2D>();
        planetRadius = _circleColl.bounds.size.x / 2f;
        gravityRadius = planetRadius + _gravityThreshold;

        _filter = new ContactFilter2D();
        _filter.SetLayerMask(_layerMask);
    }

    private void FixedUpdate()
    {
        Physics2D.OverlapCircle(transform.position, gravityRadius, _filter, _colliders);

        

        foreach (Collider2D currentColl in _colliders)
        {

            GravityEffect affectedObject = currentColl.GetComponent<GravityEffect>();
            if (affectedObject == null) continue;
            affectedObject.GravityPull(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        _circleColl = GetComponent<CircleCollider2D>();
        planetRadius = _circleColl.bounds.size.x / 2f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_circleColl.bounds.center, planetRadius + _gravityThreshold);
    }

    public float DegreesToObject(GravityEffect target)
    {
        Vector2 vectorToObject = (target.transform.position - transform.position).normalized;
        float initialDirectionToObject = Mathf.Atan2(vectorToObject.y, vectorToObject.x) * Mathf.Rad2Deg;
        return initialDirectionToObject;
    }
}
