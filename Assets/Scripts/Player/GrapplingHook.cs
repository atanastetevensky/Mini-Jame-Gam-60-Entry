using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject target;

    private Vector2 _originPosition;
    private Grapple playerGrapple;

    private void Start()
    {
        _originPosition = transform.position;
        playerGrapple = Game.Instance.GetPlayer.GetComponent<Grapple>();
    }

    private void FixedUpdate()
    {

    }
}
