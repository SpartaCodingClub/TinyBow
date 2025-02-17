using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    private static readonly int IsCollect = Animator.StringToHash(nameof(IsCollect));
    private static readonly int IsMove = Animator.StringToHash(nameof(IsMove));
    private static readonly string MainRenderer = nameof(MainRenderer);

    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.FindComponent<Animator>(MainRenderer);
    }

    public void Move(Vector2 direction)
    {
        _animator.SetBool(IsMove, direction.magnitude > 0.0f);
    }
}