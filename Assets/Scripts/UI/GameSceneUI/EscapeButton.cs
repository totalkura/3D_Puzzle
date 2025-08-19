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

    public void OptionGame()
    {
        Time.timeScale = 1f;  // 시간 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    // 게임 종료 버튼
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit(); // 빌드된 게임에서 종료됨 (에디터에서는 안 보임)
        Debug.Log("게임 종료");
    }
}

