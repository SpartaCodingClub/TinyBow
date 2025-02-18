using UnityEngine;

public class PinkStars : Spawner
{
    public override GameObject Spawn(Rect spawnArea)
    {
        GameObject gameObject = base.Spawn(spawnArea);
        if (Random.Range(0, 2) == 0)
        {
            gameObject.transform.localPosition = new(transform.localPosition.x, 0f);
        }
        else if (gameObject.TryGetComponent<Animator>(out var animator))
        {
            animator.SetInteger(Define.IsAttack, 1);
        }

        return gameObject;
    }
}