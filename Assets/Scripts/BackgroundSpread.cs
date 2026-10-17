using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundSpread : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] private float _repeats = 6f;

    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _material.mainTextureScale = new Vector2(_repeats, _repeats);
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = _camera.transform.position;

        transform.position = (Vector2)cameraPosition;

        _material.mainTextureOffset = (Vector2)cameraPosition * _repeats / transform.localScale.x;
    }
}
