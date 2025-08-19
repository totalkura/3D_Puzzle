using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI; // 메뉴 패널

    // 재시작 버튼
    public void RestartGame()
    {
        Time.timeScale = 1f;  // 시간 정상화
        GameManager.Instance.userSelectStage = MapManager.Instance.nowStage;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    public void OptionGame()
    {
        UIManager uIManager = FindObjectOfType<UIManager>();
        Time.timeScale = 1f;
        uIManager.Option.SetActive(true); // 옵션 패널 활성화
    }

    // 게임 종료 버튼
    public void QuitGame()
    {
        Time.timeScale = 1f; // 시간 정상화
        SceneManager.LoadScene(0); // 빌드 세팅에서 0번 씬이 메인 씬일 경우
    }
}

