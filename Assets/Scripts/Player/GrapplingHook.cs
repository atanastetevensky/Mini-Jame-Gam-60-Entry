using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float _minimumRetractDistance;
    [SerializeField] private GameObject _chainPrefab;
    [SerializeField] private float _chainDistanceMultiplier;
    [SerializeField] private float _chainLengthOffset;

    public GameObject targetObject;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _currentRotation;
    private Vector2 _originPosition;
    private Vector2 _grabbedOffset;
    private Grapple _playerGrapple;
    private Inventory _inventory;
    private SpriteRenderer _chainRenderer;
    private List<GameObject> _chainList = new List<GameObject>();

    private bool Grabbed => _grippedObject != null;
    private bool Retracting => Grabbed || _failed;
    private bool _failed = false;

    private Collectible _grippedObject;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerGrapple = Game.Instance.GetPlayer.GetComponent<Grapple>();
        _inventory = Game.Instance.GetPlayer.GetComponent<Inventory>();
        _chainRenderer = _chainPrefab.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("isRetracting", Retracting);

        //If the grappling hook has grabbed an item, store the item's position relative to the hook and keep the item at that position
        if (Grabbed)
        {
            if(_grippedObject.transform.parent == null)
            {
                _grippedObject.transform.parent = transform;
                _grabbedOffset = _grippedObject.transform.localPosition;
            }
            _grippedObject.transform.localPosition = _grabbedOffset;
        }


        _originPosition = _playerGrapple.transform.position;
        _currentRotation = Utils.GetDirectionDegree(_originPosition, Retracting ? transform.position : targetObject.transform.position) - 90f;
        _rb.rotation = _currentRotation;

        float currentDistanceFromOrigin = Vector2.Distance(_originPosition, transform.position);

        if (!Retracting)
        {
            _failed = currentDistanceFromOrigin >= _playerGrapple.Range;
        } else
        {
            CollectItem(currentDistanceFromOrigin);
        }


        Vector2 currentTargetPosition = Retracting ? _originPosition : targetObject.transform.position;
        Vector2 currentTrajectory = (currentTargetPosition - (Vector2)transform.position).normalized;
        _rb.linearVelocity = currentTrajectory * _playerGrapple.Speed;

        UpdateChain(currentDistanceFromOrigin);
    }

    private void CollectItem(float currentDistance)
    {
        if (currentDistance > _minimumRetractDistance) return;
        if(Grabbed)
        {
            _inventory.AddCollectible(_grippedObject);
            GameObject.Destroy(_grippedObject.gameObject);
        }
        _playerGrapple.isGrappling = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectible collectible = collision.GetComponent<Collectible>();
        if (collectible == null) return;
        if (!collectible.gameObject.Equals(targetObject)) return;

        _grippedObject = collectible;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _minimumRetractDistance);
    }

    private void UpdateChain(float chainOriginDistance)
    {
        float chainSpawnDistance = _chainRenderer.bounds.size.y * _chainDistanceMultiplier;
        float currentChainCount = Mathf.Floor(Mathf.Max(chainOriginDistance - _chainLengthOffset, 0f) / chainSpawnDistance);
        Vector2 chainDirection = (_originPosition - (Vector2)transform.position).normalized;
        while (_chainList.Count < currentChainCount)
        {
            _chainList.Add(Instantiate(_chainPrefab, _originPosition, Quaternion.identity, transform));
        }

        while (_chainList.Count > currentChainCount)
        {
            Destroy(_chainList[_chainList.Count - 1].gameObject);
            _chainList.RemoveAt(_chainList.Count - 1);
        }

        for (int i = 0; i < _chainList.Count; i++)
        {
            _chainList[i].transform.position = (Vector2)transform.position + chainDirection * chainSpawnDistance * (i + 1f);
            _chainList[i].transform.rotation = Quaternion.Euler(0f, 0f, Utils.GetDirectionDegree(_originPosition, transform.position) - 90f);
        }
    }
}
