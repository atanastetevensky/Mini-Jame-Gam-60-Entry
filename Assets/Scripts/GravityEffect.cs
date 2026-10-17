using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    private Rigidbody2D rb;
    public float orbitDelta { get; private set; }
    public Vector2 currentGravity { get; private set; }
    [SerializeField] private bool _applyGravity;
    [SerializeField] private bool _applyRotation;
    public Planet currentPlanet;
    public bool isInOrbit => currentPlanet != null;

    private float initialDirectionToObject;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isInOrbit) return;
        float distanceToPlanet = Vector2.Distance(currentPlanet.transform.position, transform.position);
        if (distanceToPlanet > currentPlanet.gravityRadius)
        {
            currentPlanet = null;
            orbitDelta = 0f;
            currentGravity = Vector2.zero;
        }
    }

    public void GravityPull(Planet planet)
    {
        if(!isInOrbit)
        {
            currentPlanet = planet;
            initialDirectionToObject = planet.DegreesToObject(this);
        }
        float currentDirectionToObject = planet.DegreesToObject(this);

        orbitDelta = Mathf.DeltaAngle(initialDirectionToObject, currentDirectionToObject);
        initialDirectionToObject = currentDirectionToObject;

        if(_applyRotation)
        {
            rb.MoveRotation(rb.rotation + orbitDelta);
        }

        float distanceToCenter = Vector2.Distance(planet.transform.position, transform.position);

        Vector2 gravityDirection = (planet.transform.position - transform.position).normalized;

        float distanceToSurface = distanceToCenter - planet.planetRadius;

        float gravityMultiplier = Mathf.Clamp01(planet.GravityCurve.Evaluate(1f - (distanceToSurface / planet.GravityThreshold)));

        float currentGravityForce = planet.MaxGravity * gravityMultiplier;

        currentGravity = gravityDirection * currentGravityForce;

        if (_applyGravity)
        {
            rb.AddForce(gravityDirection * currentGravityForce);
        }
    }


}
