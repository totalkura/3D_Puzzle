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
        UpdateLockState();
    }

    public void setInit(int stage)
    {
        // stageNum에 대한 값이 있어야 되고
        stageNum = stage;
        // 예시: GameManager에 플레이어가 클리어한 최대 스테이지 번호 저장되어 있다고 가정
        playerStage = GameManager.Instance.StageCheck(GameManager.Instance.userLastStage);
    }

    public void UpdateLockState()
    {
        if (stageNum <= playerStage) // stageNum: 1~9 <= playerStage: 1~9(laststage) 
        {
            // 열림
            Locks.SetActive(false);
        }

        else
        {
            // 잠김
            Locks.SetActive(true);
        }
    }
}
