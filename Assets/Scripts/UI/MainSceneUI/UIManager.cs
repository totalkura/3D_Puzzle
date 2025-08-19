using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("페이드 애니메이터")]
    public Animator animator;
    public float transitionTime;

    [Header("락 스테이지")]
    public GameObject[] stageObject;
    public GameObject stageMap;
    public int maxStage;

    private void Awake()
    {
        // 씬이 시작될 때 스테이지 UI 업데이트
        UpdateStageUI();

        maxStage = 10; // 최대 스테이지 수 설정
        stageMap = Resources.Load<GameObject>("Prefabs\\Stage1");

        stageObject = new GameObject[maxStage];

        for (int i = 0; i < maxStage; i++)
        {
            stageObject[i] = stageMap;
        }

        foreach (GameObject stage in stageObject)
        {
            if (stage != null)
            {
                stage.SetActive(false); // 초기에는 모든 스테이지 비활성화
            }
        }
    }

    // ========== 씬 전환 ===================
    public void OnNEWStageSelector(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        StartCoroutine(LoadSceneWithFade("InGameScene"));
    }

    // ========== 스테이지 선택 버튼 ===================

    public void OnStageSelector()
    {
        if (StageScene.activeSelf)
        {
            // 메인 메뉴가 켜져 있으면 → 스테이지 선택 화면으로 전환
            StageScene.SetActive(false);
            animator.SetTrigger("FadeOut");
            MainButton.SetActive(true);
        }
        else
        {
            // 스테이지 선택 화면이 켜져 있으면 → 메인 메뉴로 전환
            MainButton.SetActive(false);
            animator.SetTrigger("FadeOut");
            StageScene.SetActive(true);
        }
    }

    // ========== 메인 메뉴 버튼 ===================

    public void OnNewStageSelector(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }

    public void OnReLoder(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }

    // ========== 스테이지 선택 버튼 ===================

    public void OnSelectStage(int sceneNum)
    {
        // 현재 스테이지 체크
        GameManager.Instance.StageCheck(sceneNum);

        // 스테이지 잠금 여부 확인
        if (sceneNum > GameManager.Instance.userLastStage)
        {
            Debug.Log("아직 잠긴 스테이지입니다.");

            // 실행하지 않고 그대로 표시
            StageScene.SetActive(true);

        }

        // 스테이지 진입
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }

    public void OnLoadStage()
    {
        GameManager.Instance.StageCheck(GameManager.Instance.userLastStage);
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene("InGameScene");
    }

    // ========== 뒤로가기 버튼 ===================

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
        animator.SetTrigger("FadeOut");
    }

    // 옵션 버튼이 눌리면 -> 메인 메뉴로 전환
    public void CloseOption()
    {
        OptionScene.SetActive(false);  // 옵션 메뉴 비활성화
        MainButton.SetActive(true);    // 메인 메뉴 다시 활성화
        animator.SetTrigger("FadeOut");
    }


    // ========== 데이터 초기화 ===================

    public void ResetData()
    {
        Debug.Log("Check");
        PlayerPrefs.DeleteAll();
        GameManager.Instance.userLastStage = 0;
    }


    // ========== 씬 전환 애니메이션 ===================

    public void LoadStage(int sceneNum)
    {
        GameManager.Instance.StageCheck(sceneNum);
        StartCoroutine(LoadSceneWithFade("InGameScene"));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime); // FadeOut 애니메이션 길이
        SceneManager.LoadScene(sceneName);
    }

    // ========== 스테이지 UI 업데이트 ===================

    public void UpdateStageUI()
    {
      
    }
}
