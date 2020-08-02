using Game.Wave;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { Init, Playing, GameOver };

    public GameState State { get; private set; }

    private void Start()
    {
        State = GameState.Init;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (State)
        {
            case GameState.Init:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartGame();
                }
                break;

            case GameState.Playing:
                break;

            case GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    State = GameState.Init;
                }
                break;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game End");
        State = GameState.GameOver;

        Time.timeScale = 0;
    }

    private void StartGame()
    {
        State = GameState.Playing;
        Time.timeScale = 1;
        WaveManager.Instance.StartWave();
    }
}
