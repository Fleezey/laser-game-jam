using Game.Wave;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { Init, Playing, GameOver };

    public GameState State { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private GameObject m_StartPanel = null;
    [SerializeField] private GameObject m_EndPanel = null;

    private void Start()
    {
        State = GameState.Init;
        m_StartPanel.SetActive(true);
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
                    m_StartPanel.SetActive(false);
                    StartGame();
                }
                break;

            case GameState.Playing:
                break;

            case GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
        }
    }

    public void GameOver()
    {
        Cursor.visible = true;
        m_EndPanel.SetActive(true);
        State = GameState.GameOver;
        Time.timeScale = 0;
    }

    private void StartGame()
    {
        Cursor.visible = false;
        State = GameState.Playing;
        Time.timeScale = 1;
        WaveManager.Instance.StartWave();
    }
}
