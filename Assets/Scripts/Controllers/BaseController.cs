using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Vector2 LookDirection { get { return lookDirection; } }
    protected Vector2 lookDirection;

    public Vector2 MoveDirection { get { return moveDirection; } }
    protected Vector2 moveDirection = Vector2.zero;

    protected Rigidbody2D _rigidbody;

    protected AnimationHandler animationHandler;

    private SpriteRenderer mainRenderer;
    private Transform weaponPivot;

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

        animationHandler = GetComponent<AnimationHandler>();

        mainRenderer = gameObject.FindComponent<SpriteRenderer>(nameof(mainRenderer).FirstCharacterToUpper());
        weaponPivot = transform.Find(nameof(weaponPivot).FirstCharacterToUpper());
    }

    protected virtual void Deinitialize()
    {

    }

    protected virtual void HandleAction()
    {
        Look();
    }

    private void Look()
    {
        bool isLeft = lookDirection.x < 0;
        mainRenderer.flipX = isLeft;
    }

    private void Move()
    {
        Vector2 velocity = moveDirection * 5f; // TODO: Temp Speed.
        _rigidbody.velocity = velocity;

        animationHandler.Move(moveDirection);
    }
}