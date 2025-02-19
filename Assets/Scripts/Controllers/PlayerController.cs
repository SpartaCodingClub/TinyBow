using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    #region Inspector
    [Header("Player")]
    [SerializeField] GameObject effect_Minigame;
    #endregion

    #region Input_Main
    private void OnLook(InputValue inputValue)
    {
        if (Managers.Game.CurrentState != GameState.Start)
        {
            return;
        }

        if (isAttacking)
        {
            return;
        }

        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPosition - (Vector2)transform.position).normalized;
    }

    private void OnMove(InputValue inputValue)
    {
        if (Managers.Game.CurrentState != GameState.Start)
        {
            return;
        }

        moveDirection = inputValue.Get<Vector2>().normalized;
    }
    #endregion
    #region Input_MiniGame
    private void OnJump()
    {
        if (Managers.Game.CurrentState != GameState.Start)
        {
            return;
        }

        Attack();
    }
    #endregion

    private GameMode gameMode;
    private Camera mainCamera;

    protected override void Initialize()
    {
        base.Initialize();
        mainCamera = Camera.main;
    }

    protected override void Look()
    {
        switch (gameMode)
        {
            case GameMode.MainGame:
                base.Look();
                break;
            case GameMode.MiniGame:
                break;
        }
    }

    protected override void Move()
    {
        switch (gameMode)
        {
            case GameMode.MainGame:
                base.Move();
                break;
            case GameMode.MiniGame:
                if (Managers.Game.CurrentState != GameState.Start) break;
                animationHandler.Move(Vector2.right);
                break;
        }
    }

    protected override void Attack(Direction direction = Direction.LookDirection)
    {
        switch (gameMode)
        {
            case GameMode.MainGame:
                base.Attack(direction);
                break;
            case GameMode.MiniGame:
                base.Attack(Direction.Down);
                break;
        }
    }

    protected override ProjectileController Fire()
    {
        if (gameMode == GameMode.MiniGame)
        {
            lookDirection = Vector2.down;
        }

        ProjectileController projectile = base.Fire();
        projectile.OnDamage += () =>
        {
            GameObject effectObject = Instantiate(effect_Minigame, projectile.transform.position, Quaternion.identity);
            EffectController effect = effectObject.GetComponent<EffectController>();
            effect.OnTrigger += () => _rigidbody.AddForce(8f * Vector2.up, ForceMode2D.Impulse);
        };

        return projectile;
    }

    public void SetController(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.MainGame:
                _rigidbody.gravityScale = 0f;
                _rigidbody.velocity = Vector2.zero;
                animationHandler.Idle();
                moveDirection = Vector2.zero;
                break;
            case GameMode.MiniGame:
                _rigidbody.velocity = Vector2.zero;
                _mainRenderer.flipX = false;
                animationHandler.Idle();
                break;
        }

        this.gameMode = gameMode;
    }
}