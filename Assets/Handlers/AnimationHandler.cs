using System;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    #region AnimationEvents
    private void OnFireEnter()
    {
        _animator.SetInteger(Define.IsAttack, 0);
        OnFire?.Invoke();
    }
    #endregion

    public event Action OnFire;

    private Animator _animator;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Idle()
    {
        _animator.SetBool(Define.IsMove, false);
    }

    public void Attack(int index = 1)
    {
        _animator.SetInteger(Define.IsAttack, index);
    }

    public void Move(Vector2 direction)
    {
        _animator.SetBool(Define.IsMove, direction.magnitude > 0.0f);
        _animator.SetInteger(Define.IsAttack, 0);
    }
}