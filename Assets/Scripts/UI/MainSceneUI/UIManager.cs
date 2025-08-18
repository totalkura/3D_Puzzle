using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // ========== 싱글톤 ===================
    private void Start()
    {
        
    }

    // ========== 메인 메뉴 버튼 ===================

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnStageSelector(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        SceneManager.LoadScene("InGameScene");
    }

    public void OnReLoder(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        SceneManager.LoadScene("InGameScene");
    }

    // ========== 스테이지 선택 버튼 ===================

    public void OnStage1(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        SceneManager.LoadScene("InGameScene");
    }

    public void OnLoadStage()
    {
        GameManager.Instance.StageCheck(GameManager.Instance.userLastStage);
        SceneManager.LoadScene("InGameScene");
    }
}
