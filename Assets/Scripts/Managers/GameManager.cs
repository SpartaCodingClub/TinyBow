using DG.Tweening;
using UnityEngine;

public enum GameMode
{
    MainGame,
    MiniGame
}

public enum GameState
{
    Ready,
    Start,
    End
}

public class GameManager
{
    private Camera mainCamera;
    private CameraController cameraController;
    private PlayerController player;
    private SmallClouds smallClouds;

    public GameState CurrentState { get; private set; } = GameState.Start;

    public void Initialize()
    {
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();
        player = Object.FindObjectOfType<PlayerController>();
        smallClouds = Object.FindObjectOfType<SmallClouds>();
    }

    public void SetGameMode(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.MainGame:
                break;
            case GameMode.MiniGame:
                Create_SmallClouds(20);
                mainCamera.DOColor(Define.Sky, 2f);
                cameraController.OffsetX = 6f;
                cameraController.OffsetY = 3f;
                player.SetController(GameMode.MiniGame);
                break;
        }

        //CurrentState = GameState.Ready;
    }

    private void Create_SmallClouds(int count)
    {
        for (int i = 0; i < count; i++)
        {
            smallClouds.Spawn(new(-Spawner.Width, -30f, Spawner.Width * 2f, 40f));
        }
    }
}