using UnityEngine;

public class SmallClouds : Spawner, ICloud
{
    public override GameObject Spawn(Rect spawnArea)
    {
        GameObject gameObject = base.Spawn(spawnArea);
        if (gameObject.TryGetComponent<Animator>(out var animator))
        {
            animator.speed = 0f;
            animator.Play(Define.Idle, 0, Random.Range(0f, 1f));
        }

        return gameObject;
    }
}