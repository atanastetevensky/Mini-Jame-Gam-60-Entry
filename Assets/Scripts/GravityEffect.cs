using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    private Rigidbody2D rb;
    public float orbitDelta { get; private set; }
    public Planet currentPlanet;
    public float initialDirectionToObject;
    public bool isInOrbit => currentPlanet != null;

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

        float distanceToCenter = Vector2.Distance(planet.transform.position, transform.position);

        Vector2 gravityDirection = (planet.transform.position - transform.position).normalized;

        float distanceToSurface = distanceToCenter - planet.planetRadius;

        float gravityMultiplier = Mathf.Clamp01(planet.GravityCurve.Evaluate(1f - (distanceToSurface / planet.GravityThreshold)));

        float currentGravity = planet.MaxGravity * gravityMultiplier;

        rb.AddForce(gravityDirection * currentGravity);

        Debug.Log($"gravity direction: {gravityDirection}, current gravity: {currentGravity}");
    }


}
