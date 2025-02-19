using UnityEngine;

public class PinkStarSpawner : Spawner
{
    public override GameObject Spawn(Rect spawnArea)
    {
        GameObject gameObject = base.Spawn(spawnArea);
        if (Random.Range(0, 2) == 0)
        {
            gameObject.transform.localPosition = new(gameObject.transform.localPosition.x, 0f);
        }
        else if (gameObject.TryGetComponent<Animator>(out var animator))
        {
            animator.SetInteger(Define.IsAttack, 1);
        }

        ProjectileController projectile = gameObject.GetComponent<ProjectileController>();
        projectile.OnDamage += Managers.Game.MiniGame_End;

        return gameObject;
    }
}