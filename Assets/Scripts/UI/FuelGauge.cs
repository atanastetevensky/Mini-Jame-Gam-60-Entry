using UnityEngine;

public class FuelGauge : MonoBehaviour
{
    [SerializeField] private RectTransform _gaugeTransform;
    [SerializeField] private float _gaugeBottomOffset;

    private PlayerProperties _playerProperties;
    private Vector2 transformOriginalPosition;

    void Start()
    {
        _playerProperties = Game.Instance.GetPlayer.properties;
        transformOriginalPosition = _gaugeTransform.position;
    }

    void Update()
    {
        float currentFuelNormalized = _playerProperties.currentFuel / _playerProperties.MaxFuel;
        //Debug.Log($"player properties: {_playerProperties.currentFuel}, currentFuelNormalized: {currentFuelNormalized}");

        _gaugeTransform.position = transformOriginalPosition - new Vector2(0f, _gaugeBottomOffset) * (1f - currentFuelNormalized);
    }
}
