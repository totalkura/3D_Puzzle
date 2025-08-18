using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ExitButton : MonoBehaviour
{
    public int SceneNum; // 현재 씬 번호를 저장할 변수

    public void Start()
    {
        // 현재 씬 번호를 가져와서 SceneNum에 저장
        //SceneNum = GameManager.GetComponenet<>();
    }

    public void OnClickExitButton() 
    {
            Debug.Log("ExitButton Clicked");
            SceneManager.LoadScene("MainScene");
    }
}
