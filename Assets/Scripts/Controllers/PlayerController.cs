using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    #region InputSystem
    private void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPosition - (Vector2)transform.position).normalized;
    }

    private void OnMove(InputValue inputValue)
    {
        moveDirection = inputValue.Get<Vector2>().normalized;
    }
    #endregion

    private Camera mainCamera;

    protected override void Initialize()
    {
        base.Initialize();
        mainCamera = Camera.main;
    }
}