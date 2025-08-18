using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour
{
    public int SceneNum; // 현재 씬 번호를 저장할 변수

    public void Start()
    {
        // 현재 씬 번호를 가져와서 SceneNum에 저장
        //SceneNum = GameManager.GetComponenet<>();
    }

    public void OnClickReStartButton()
    {
        Debug.Log("ReStartButton Clicked");
        SceneManager.LoadScene("플레이어가 있는 위치로 지정한 값");
    }
}
