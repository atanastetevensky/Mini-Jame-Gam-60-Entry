using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    [SerializeField] int frames;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        int currentFrame = Utils.getCurrentFrameIndex(_animator);
        if(currentFrame == frames - 1)
        {
            Destroy(this.gameObject);
        }
    }
}
