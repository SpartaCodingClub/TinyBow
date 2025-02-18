using UnityEngine;

public class CloudsController : MonoBehaviour
{
    private void Awake()
    {
        SetSpeed(1f);
    }

    public void SetSpeed(float speed)
    {
        var clouds = gameObject.GetComponentsInChildren<ICloud>();
        foreach (var cloud in clouds)
        {
            cloud.Speed = speed;
        }
    }
}