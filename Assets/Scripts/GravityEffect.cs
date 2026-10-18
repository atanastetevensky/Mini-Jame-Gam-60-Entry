using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    private Rigidbody2D rb;
    public float orbitDelta { get; private set; }
    public Vector2 currentGravity { get; private set; }

    [SerializeField] private float _massUnderGravity;

    [SerializeField] public bool applyGravity;
    [SerializeField] public bool applyRotation;

    public Planet currentPlanet;
    public bool isInOrbit => currentPlanet != null;

    private float initialDirectionToObject;
    private float _defaultMass;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _defaultMass = rb.mass;
    }

    private void Update()
    {
        if (!isInOrbit) return;

        rb.mass = _massUnderGravity;

        float distanceToPlanet = Vector2.Distance(currentPlanet.transform.position, transform.position);
        if (distanceToPlanet > currentPlanet.gravityRadius)
        {
            currentPlanet = null;
            orbitDelta = 0f;
            currentGravity = Vector2.zero;
            rb.mass = _defaultMass;
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

        if(applyRotation)
        {
            rb.MoveRotation(rb.rotation + orbitDelta);
        }

        float distanceToCenter = Vector2.Distance(planet.transform.position, transform.position);

        Vector2 gravityDirection = (planet.transform.position - transform.position).normalized;

        float distanceToSurface = distanceToCenter - planet.planetRadius;

        float gravityMultiplier = Mathf.Clamp01(planet.GravityCurve.Evaluate(1f - (distanceToSurface / planet.GravityThreshold)));

        float currentGravityForce = planet.MaxGravity * gravityMultiplier;

        currentGravity = gravityDirection * currentGravityForce;

        if (applyGravity)
        {
            rb.AddForce(gravityDirection * currentGravityForce);
        }
    }


}
