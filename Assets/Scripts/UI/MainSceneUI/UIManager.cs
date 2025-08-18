using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // ========== SetActive ===================

    [Header("메인 메뉴")]
    public GameObject StageScene;
    public GameObject MainButton;
    public GameObject BackSpace;
    public GameObject OptionScene;
    public GameObject Option;

    public void OnStageSelector()
    {
        if (StageScene.activeSelf)
        {
            // 메인 메뉴가 켜져 있으면 → 스테이지 선택 화면으로 전환
            StageScene.SetActive(false);
            MainButton.SetActive(true);
        }
        else
        {
            // 스테이지 선택 화면이 켜져 있으면 → 메인 메뉴로 전환
            MainButton.SetActive(false);
            StageScene.SetActive(true);
        }
    }

    public void OnBackSpace() 
    {
        BackSpace.SetActive(false);
        StageScene.SetActive(false);
        MainButton.SetActive(true);

        if (StageScene != null) 
        {
          BackSpace.SetActive(true);
        }
    }

    // ========== 옵션 버튼 ===================
    public void OnOption()
    {
       MainButton.SetActive(false);
       OptionScene.SetActive(true); 
    }

    // 옵션 버튼이 눌리면 -> 메인 메뉴로 전환
    public void CloseOption()
    {
        OptionScene.SetActive(false);  // 옵션 메뉴 비활성화
        MainButton.SetActive(true);    // 메인 메뉴 다시 활성화
    }

    // ========== 메인 메뉴 버튼 ===================

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnNEWStageSelector(int sceneNum)
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
