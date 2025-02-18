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
        float x = Mathf.Repeat(Speed * 0.1f * Time.time, 1f);
        Vector2 offset = new(x, 0);

        top.SetTextureOffset(Define.MainTex, offset);
        bottom.SetTextureOffset(Define.MainTex, offset);
    }
}