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
    [Header("���ȭ��")]
    public GameObject Locks;

    [Header("�������� ��ȣ")]
    public int stageNum;
    private int playerStage;

    private void Start()
    {
        // stageNum�� ���� ���� �־�� �ǰ�
        stageNum = GameManager.Instance.StageCheck(GameManager.Instance.userLastStage);
        // ����: GameManager�� �÷��̾ Ŭ������ �ִ� �������� ��ȣ ����Ǿ� �ִٰ� ����
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
            // ����
            Locks.SetActive(true);
        }

        else
        {
            // ���
            Locks.SetActive(false);
        }
    }
}
