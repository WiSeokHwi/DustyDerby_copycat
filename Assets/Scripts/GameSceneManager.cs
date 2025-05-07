
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        GameOver,
        Pause,
    }
    public static GameSceneManager Instance;
    public GameState gameState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1;
        gameState = GameState.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (GameSceneManager.Instance.gameState == GameSceneManager.GameState.GameOver) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.Playing)
            {
                PauseGame();
            }
            else if (gameState == GameState.Pause)
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        
        gameState = GameState.Pause;
        UIManager.instance.pauseGamePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("PauseGame");
        Time.timeScale = 0;
        
        
    }

    public void ResumeGame()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1;
        UIManager.instance.pauseGamePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
