
using UnityEngine;



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
            Debug.Log("GameSceneManager 인스턴스 생성됨: " + gameObject.name);
        }
        else
        {
            Debug.Log("중복 GameSceneManager 발견, 삭제됨: " + gameObject.name);
            Destroy(gameObject);
        }
        Time.timeScale = 1;
        gameState = GameState.Playing;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    void PauseGame()
    {
        
        gameState = GameState.Pause;
        UIManager.instance.pauseGamePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("PauseGame");
        Time.timeScale = 0;
        
        
    }

    void ResumeGame()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1;
        UIManager.instance.pauseGamePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
