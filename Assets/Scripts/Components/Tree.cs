using UnityEngine;

public class Tree : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.FindComponent<Animator>(Define.MainRenderer);
        _animator.Play(Define.Idle, 0, Random.Range(0f, 1f));
    }
}