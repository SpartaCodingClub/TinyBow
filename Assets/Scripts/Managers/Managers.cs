using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance { get; private set; }

    public static readonly GameManager Game = new();
    public static readonly UIManager UI = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        Game.Initialize();
    }

    protected void Update()
    {
        Game.HandleAction();
    }
}