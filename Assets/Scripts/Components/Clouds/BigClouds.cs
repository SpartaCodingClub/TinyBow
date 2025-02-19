using Unity.VisualScripting;
using UnityEngine;

public class BigClouds : MonoBehaviour, ICloud
{
    public float Speed { get; set; }

    private Material top;
    private Material bottom;

    private void Awake()
    {
        top = gameObject.FindComponent<Renderer>(nameof(top).FirstCharacterToUpper()).material;
        bottom = gameObject.FindComponent<Renderer>(nameof(bottom).FirstCharacterToUpper()).material;
    }

    private void Update()
    {
        Vector2 offset = new(Speed * 0.05f * Time.deltaTime, 0);
        top.mainTextureOffset += offset;
        bottom.mainTextureOffset += offset;
    }
}