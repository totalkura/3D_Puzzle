using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject ESCPanel;
    public GameObject Option;

    // 재시작 버튼
    public void RestartGame()
    {
        Time.timeScale = 1f;  // 시간 정상화
        GameManager.Instance.userSelectStage = MapManager.Instance.nowStage;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드
    }

    public void OptionGame()
    {
        Option.SetActive(true);
        ESCPanel.SetActive(false); // ESC 패널 비활성
    }

    public void OptionExitGame()
    {
        Option.SetActive(false);
        ESCPanel.SetActive(true); // ESC 패널 비활성
    }

    // 게임 종료 버튼
    public void QuitGame()
    {
        Time.timeScale = 1f; // 시간 정상화
        SoundManager.instance.PlayBGM(SoundManager.bgm.Main); // 메인 BGM 재생
        SceneManager.LoadScene(0); // 빌드 세팅에서 0번 씬이 메인 씬일 경우
    }
}

