using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // 메뉴 패널
    private bool isPaused = false;

    void Update()
    {
        // ESC 키 입력 확인
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // 게임 멈추고 메뉴 표시
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);   // 메뉴 활성화
        Time.timeScale = 0f;          // 게임 멈춤
        isPaused = true;
    }

    // 게임 재개
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // 메뉴 숨기기
        Time.timeScale = 1f;          // 시간 정상화
        isPaused = false;
    }

    // 재시작 버튼
    public void RestartGame()
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

