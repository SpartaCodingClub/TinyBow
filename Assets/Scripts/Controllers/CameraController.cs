using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetObject;

    public Rigidbody2D Rigidbody2D { get; private set; }

    public float OffsetX { get; set; }
    public float OffsetY { get; set; }

    private void FixedUpdate()
    {
        if (targetObject == null)
        {
            return;
        }

        Vector3 currentPosition = transform.position;
        Vector2 targetPosition = targetObject.position;

        float x = Mathf.Lerp(currentPosition.x, targetPosition.x + OffsetX, 0.02f);
        float y = Mathf.Lerp(currentPosition.y, targetPosition.y + OffsetY, 0.02f);

        transform.position = new(x, y, currentPosition.z);
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}