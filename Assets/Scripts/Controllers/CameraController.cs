using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetObject;

    private void Update()
    {
        if (targetObject == null)
        {
            return;
        }

        Vector3 currentPosition = transform.position;
        Vector2 targetPosition = targetObject.position;

        float x = Mathf.Lerp(currentPosition.x, targetPosition.x, 0.01f);
        float y = Mathf.Lerp(currentPosition.y, targetPosition.y, 0.01f);

        transform.position = new(x, y, currentPosition.z);
    }
}