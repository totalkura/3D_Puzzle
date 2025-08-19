using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // 메뉴 패널
    private bool isPaused = false;

    // 재시작 버튼
    public void RestartGame()
    {
        Time.timeScale = 1f;  // 시간 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    public void OptionGame(int sceneNum)
    {
        UIManager uIManager = FindObjectOfType<UIManager>();

        if (uIManager != null)
        {
            uIManager.OnOption();

            Time.timeScale = 1f;

            SceneManager.LoadScene("OptionScene"); // 옵션 씬으로 전환
        }

        else
        {
            Debug.LogError("UIManager not found in the scene.");
        }

    }

    // 게임 종료 버튼
    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }
}

