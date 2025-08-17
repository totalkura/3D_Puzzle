using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour
{
    public void OnClickReStartButton()
    {
        Debug.Log("ReStartButton Clicked");
        SceneManager.LoadScene("플레이어가 있는 위치로 지정한 값");
    }
}
