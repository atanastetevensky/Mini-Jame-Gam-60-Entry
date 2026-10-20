using UnityEngine;

public class FuelGauge : MonoBehaviour
{
    [SerializeField] private RectTransform _gaugeTransform;
    [SerializeField] private float _gaugeBottomOffset;

    private PlayerProperties _playerProperties;
    private Vector2 transformOriginalPosition;

    void Start()
    {
        _playerProperties = Game.instance.Player._properties;
        transformOriginalPosition = _gaugeTransform.position;
    }

    void Update()
    {
        if(Game.instance.gameOver) this.gameObject.SetActive(false);
        float currentFuelNormalized = _playerProperties.currentFuel / _playerProperties.MaxFuel;

        _gaugeTransform.position = transformOriginalPosition - new Vector2(0f, _gaugeBottomOffset) * (1f - currentFuelNormalized);
    }
}
