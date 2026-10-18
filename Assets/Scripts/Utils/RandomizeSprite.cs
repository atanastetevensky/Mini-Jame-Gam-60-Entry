using UnityEngine;

public class RandomizeSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _spriteArray;

    private SpriteRenderer _renderer;

    void Awake()
    {
        if(_spriteArray.Length == 0)
        {
            Debug.Log("Sprite array is empty.");
            return;
        }

        _renderer = GetComponent<SpriteRenderer>();

        int spriteIndex = Mathf.RoundToInt(Random.Range(0, _spriteArray.Length - 1f));

        Sprite randomSprite = _spriteArray[spriteIndex];

        _renderer.sprite = randomSprite;
    }
}
