using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.VersionControl.ListControl;

public class LockButton : MonoBehaviour
{
    [Header("잠김화면")]
    public GameObject Locks;

    [Header("스테이지 번호")]
    public int stageNum;
    private int playerStage;

    private void Start()
    {
        // stageNum에 대한 값이 있어야 되고
        stageNum = GameManager.Instance.StageCheck(GameManager.Instance.userLastStage);
        // 예시: GameManager에 플레이어가 클리어한 최대 스테이지 번호 저장되어 있다고 가정
        playerStage = PlayerPrefs.GetInt("PlayerStage", UIManager.Instantiate<UIManager>(gameObject.stage.transform));
        UpdateLockState();
    }

    public void UnlockStage(int clearedStage)
    {
        if (clearedStage > playerStage)
        {
            playerStage = clearedStage;
            PlayerPrefs.SetInt("PlayerStage", playerStage);
            PlayerPrefs.Save();
        }
    }

    public void UpdateLockState()
    {
        if (stageNum <= playerStage)
        {
            // 열림
            Locks.SetActive(true);
        }

        else
        {
            // 잠김
            Locks.SetActive(false);
        }
    }
}
