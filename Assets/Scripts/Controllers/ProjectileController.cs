using DG.Tweening;
using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Inspector
    [SerializeField] private LayerMask target;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    #endregion

    public event Action OnDamage;

    private SpriteRenderer spriteRenderer;
    private Tween reservedTween;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (duration == 0f)
        {
            return;
        }

        reservedTween = DOVirtual.DelayedCall(duration, () =>
        {
            spriteRenderer.DOFade(0.0f, 1.0f).OnComplete(() => Destroy(gameObject));
        });
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.right);
    }

    private void OnDestroy()
    {
        if (duration == 0f)
        {
            return;
        }

        spriteRenderer.DOKill();
        reservedTween.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target.value == (target.value | (1 << collision.gameObject.layer)))
        {
            spriteRenderer.DOFade(0.0f, 0.1f).OnComplete(() => Destroy(gameObject));
            OnDamage?.Invoke();
        }
    }
}