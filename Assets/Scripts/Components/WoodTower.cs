using DG.Tweening;
using UnityEngine;

public class WoodTower : MonoBehaviour
{
    private Sequence jump;

    private void Awake()
    {
        jump = DOTween.Sequence().Pause()
            .Append(transform.DOScale(new Vector2(1.5f, 0.5f), 0.5f).SetEase(Ease.OutBack))
            .Append(transform.DOScale(new Vector2(0.5f, 1.5f), 0.5f).SetEase(Ease.OutBounce));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.Player) == false)
        {
            return;
        }

        jump.Play();
    }
}