using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class Player : MonoBehaviour
{
    private PlayerProperties _properties;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _properties = GetComponent<PlayerProperties>();
        _properties.currentHealth = _properties.MaxHealth;
        _properties.currentPoints = _properties.StartingPoints;
        _properties.currentFuel = _properties.MaxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
