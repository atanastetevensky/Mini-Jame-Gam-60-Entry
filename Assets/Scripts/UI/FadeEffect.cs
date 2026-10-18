using TMPro;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] AnimationCurve _fadeCurve;
    [SerializeField] float fadeTime;
    [SerializeField] bool fadeIn;

    private TextMeshProUGUI _textMesh;
    private Utils.Timer _timer;


    private void OnEnable()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        if (fadeIn)
        {
            _textMesh.alpha = 0f;
        }
        _timer = new Utils.Timer(fadeTime);
    }

    private void Update()
    {
        if(_timer.HasPassed())
        {
            _textMesh.alpha = fadeIn ? 1f : 0f;
            Destroy(this);
            return;
        }
        float currentValue = _fadeCurve.Evaluate(_timer.passedValue());

        _textMesh.alpha = fadeIn ? currentValue : 1f - currentValue;
    }
}
