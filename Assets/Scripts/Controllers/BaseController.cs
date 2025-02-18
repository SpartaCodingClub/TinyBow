using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft,
    LookDirection
}

public abstract class BaseController : MonoBehaviour
{
    #region Inspector
    [Header("Optional")]
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected GameObject effect;
    #endregion

    private readonly float direction = 0.25f;

    public Vector2 LookDirection { get { return lookDirection; } }
    protected Vector2 lookDirection;

    public Vector2 MoveDirection { get { return moveDirection; } }
    protected Vector2 moveDirection = Vector2.zero;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _mainRenderer;

    protected AnimationHandler animationHandler;

    protected bool isAttacking;
    protected Transform weaponPivot;
    private float pivotAmount;
    private float pivotOffsetY;

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake() => Initialize();
    private void OnDestroy() => Deinitialize();
    private void Update() => HandleAction();

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        animationHandler = GetComponentInChildren<AnimationHandler>();
        animationHandler.OnFire += OnFire;
        _mainRenderer = animationHandler.GetComponent<SpriteRenderer>();

        weaponPivot = transform.Find(nameof(weaponPivot).FirstCharacterToUpper());
        pivotAmount = weaponPivot.localPosition.x;
        pivotOffsetY = weaponPivot.localPosition.y;
    }

    protected virtual void Deinitialize()
    {
        animationHandler.OnFire -= OnFire;
    }

    protected virtual void HandleAction()
    {
        Look();
    }

    protected virtual void Attack(Direction direction = Direction.LookDirection)
    {
        if (direction == Direction.LookDirection)
        {
            direction = GetDirection();
        }

        switch (direction)
        {
            case Direction.Up:
                animationHandler.Attack(1);
                break;
            case Direction.UpLeft:
            case Direction.UpRight:
                animationHandler.Attack(2);
                break;
            case Direction.Left:
            case Direction.Right:
                animationHandler.Attack(3);
                break;
            case Direction.DownLeft:
            case Direction.DownRight:
                animationHandler.Attack(4);
                break;
            case Direction.Down:
                animationHandler.Attack(5);
                break;
        }

        isAttacking = true;
    }

    protected void OnFire()
    {
        isAttacking = false;
        Fire();
    }

    protected virtual ProjectileController Fire()
    {
        ProjectileController projectile = null;
        if (this.projectile != null)
        {
            Vector2 pivot = lookDirection * pivotAmount;
            pivot.y += pivotOffsetY;
            weaponPivot.localPosition = pivot;

            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            GameObject projectileObject = Instantiate(this.projectile, weaponPivot.position, Quaternion.Euler(0f, 0f, angle));
            projectile = projectileObject.GetComponent<ProjectileController>();
        }

        return projectile;
    }

    protected virtual void Look()
    {
        if (isAttacking)
        {
            return;
        }

        bool isLeft = lookDirection.x < 0;
        _mainRenderer.flipX = isLeft;
    }

    protected virtual void Move()
    {
        Vector2 velocity = isAttacking ? Vector2.zero : 3f * moveDirection;
        _rigidbody.velocity = velocity;

        animationHandler.Move(moveDirection);
    }

    private Direction GetDirection()
    {
        if (lookDirection.y > direction)
        {
            if (lookDirection.x < -direction)
            {
                return Direction.UpLeft;
            }
            else if (lookDirection.x > direction)
            {
                return Direction.UpRight;
            }

            return Direction.Up;
        }
        else if (lookDirection.y < -direction)
        {
            if (lookDirection.x < -direction)
            {
                return Direction.DownLeft;
            }
            else if (lookDirection.x > direction)
            {
                return Direction.DownRight;
            }

            return Direction.Down;
        }

        if (lookDirection.x < -direction)
        {
            return Direction.Left;
        }
        else
        {
            return Direction.Right;
        }
    }
}