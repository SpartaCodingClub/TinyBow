using DG.Tweening;
using UnityEngine;

public class WoodTower : MonoBehaviour
{
    private Transform mainRenderer;
    private Sequence jump;

    private bool onTriggerStay;

    private void Awake()
    {
        mainRenderer = transform.Find(Define.MainRenderer);
        jump = Utility.RecyclableSequence()
            .Append(mainRenderer.DOScale(new Vector2(1.5f, 0.5f), 0.2f).SetEase(Ease.OutBack))
            .Append(mainRenderer.DOScale(new Vector2(0.5f, 1.5f), 0.5f).SetEase(Ease.OutBounce))
            .Append(mainRenderer.DOScale(Vector2.one, 0.2f));
    }

    private void OnDestroy()
    {
        jump.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player) == false)
        {
            return;
        }

        jump.Restart();
        onTriggerStay = true;

        DOVirtual.DelayedCall(0.3f, () =>
        {
            if (onTriggerStay == false)
            {
                return;
            }

            collision.transform.DOMove(new(0f, 44f), 2.0f).OnComplete(() =>
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 1f;
            });

            Managers.Game.SetGameMode(GameMode.MiniGame);
        });
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerStay = false;
    }
}