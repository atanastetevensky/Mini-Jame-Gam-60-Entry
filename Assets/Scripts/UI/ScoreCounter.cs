using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _scoreMesh;
    private PlayerProperties _playerProperties;

    private void Start()
    {
        _scoreMesh = GetComponent<TextMeshProUGUI>();
        _playerProperties = Game.instance.Player._properties;
    }

    private void Update()
    {
        float currentPoints = _playerProperties.currentPoints;
        if(_scoreMesh.text != currentPoints.ToString())
        {
            _scoreMesh.text = currentPoints.ToString();
        }
    }
}
