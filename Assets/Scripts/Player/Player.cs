using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class Player : MonoBehaviour
{
    private PlayerProperties _properties;
    void Start()
    {
        _properties = GetComponent<PlayerProperties>();
        _properties.currentPoints = _properties.StartingPoints;
    }
}
