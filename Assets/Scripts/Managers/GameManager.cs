using DG.Tweening;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public enum GameMode
{
    MainGame,
    MiniGame,
    Count
}

public enum GameState
{
    Ready,
    Start,
    End
}

public class GameManager
{
    public GameState CurrentState { get; private set; }
    public int MiniGameScore { get; set; }

    private GameMode gameMode;
    private Camera mainCamera;
    private CameraController cameraController;
    private PlayerController player;

    private CloudsController cloudsController;
    private SmallCloudSpawner smallCloudSpawner;
    private PinkStarSpawner pinkStarSpawner;

    private float timer;
    private int bestScore;
    private UI_MinigameScore miniGameScoreUI;

    private readonly GameObject[] gameModes = new GameObject[(int)GameMode.Count];

    public void Initialize()
    {
        mainCamera = Camera.main;
        cameraController = mainCamera.GetComponent<CameraController>();
        player = Object.FindObjectOfType<PlayerController>();

        cloudsController = Object.FindObjectOfType<CloudsController>();
        smallCloudSpawner = Object.FindObjectOfType<SmallCloudSpawner>();
        pinkStarSpawner = Object.FindObjectOfType<PinkStarSpawner>();

        var names = Enum.GetNames(typeof(GameMode));
        for (int i = 0; i < gameModes.Length; i++)
        {
            gameModes[i] = GameObject.Find(names[i]);
        }

        DOVirtual.DelayedCall(1.0f, () => SetGameMode(GameMode.MainGame));
        bestScore = PlayerPrefs.GetInt(nameof(bestScore), 0);
    }

    public void HandleAction()
    {
        switch (gameMode)
        {
            case GameMode.MainGame:
                break;
            case GameMode.MiniGame:
                if (CurrentState != GameState.Start) break;
                timer += Time.deltaTime;
                miniGameScoreUI.UpdateUI((int)timer, bestScore);
                break;
        }
    }

    public void SetGameMode(GameMode gameMode)
    {
        for (int i = 0; i < (int)GameMode.Count; i++)
        {
            if (i == (int)gameMode)
            {
                gameModes[(int)gameMode].SetActive(true);
                continue;
            }

            gameModes[i].SetActive(false);
        }

        switch (gameMode)
        {
            case GameMode.MainGame:
                mainCamera.DOColor(Define.Sea, 2f);
                cameraController.OffsetX = 0f;
                cameraController.OffsetY = 0f;
                player.SetController(GameMode.MainGame);
                CurrentState = GameState.Start;
                miniGameScoreUI?.Death();
                DOVirtual.DelayedCall(2.0f, () => cameraController.Rigidbody2D.simulated = true);
                break;
            case GameMode.MiniGame:
                MiniGame_Ready();
                mainCamera.DOColor(Define.Sky, 2f);
                cameraController.OffsetX = 6f;
                cameraController.OffsetY = 3f;
                player.SetController(GameMode.MiniGame);
                CurrentState = GameState.Ready;
                cameraController.Rigidbody2D.simulated = false;
                break;
        }

        this.gameMode = gameMode;
    }

    private void SpawnClouds()
    {
        for (int i = 0; i < 20; i++)
        {
            smallCloudSpawner.Spawn(new(-Spawner.Width, -40f, Spawner.Width * 2f, 40f));
        }
    }

    private void MiniGame_Ready()
    {
        MiniGameScore = 0;
        timer = 0;

        smallCloudSpawner.Clear();
        smallCloudSpawner.StartSpawning();
        SpawnClouds();

        DOVirtual.DelayedCall(3f, MiniGame_Intro);
    }

    private void MiniGame_Intro()
    {
        Managers.UI.Show<UI_MinigameIntro>();
        miniGameScoreUI = Managers.UI.Show<UI_MinigameScore>();
    }

    public void MiniGame_Start()
    {
        CurrentState = GameState.Start;
        pinkStarSpawner.StartSpawning();

        float speed = 1f;
        DOTween.To(() => speed, x =>
        {
            speed = x;
            cloudsController.SetSpeed(speed);
        }, 5f, 1f).Play();
    }

    public void MiniGame_End()
    {
        int score = (int)timer;
        if (score > bestScore)
        {
            bestScore = score;
            miniGameScoreUI.UpdateUI(score, bestScore);
            PlayerPrefs.SetInt(nameof(bestScore), bestScore);
        }

        CurrentState = GameState.End;
        smallCloudSpawner.Clear();
        pinkStarSpawner.Clear();

        float speed = 5f;
        DOTween.To(() => speed, x =>
        {
            speed = x;
            cloudsController.SetSpeed(speed);
        }, 1f, 1f).Play();

        MainGame_Ready();
    }

    private void MainGame_Ready()
    {
        SpawnClouds();
        player.transform.DOMove(Vector2.zero, 2f).SetEase(Ease.InSine).OnComplete(() => SetGameMode(GameMode.MainGame));
    }
}