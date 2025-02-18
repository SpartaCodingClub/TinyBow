using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetObject;

    public float OffsetX { get; set; }
    public float OffsetY { get; set; }

    private void LateUpdate()
    {
        if (targetObject == null)
        {
            return;
        }

        Vector3 currentPosition = transform.position;
        Vector2 targetPosition = targetObject.position;

        float x = Mathf.Lerp(currentPosition.x, targetPosition.x + OffsetX, 0.01f);
        float y = Mathf.Lerp(currentPosition.y, targetPosition.y + OffsetY, 0.01f);

        transform.position = new(x, y, currentPosition.z);
    }
}