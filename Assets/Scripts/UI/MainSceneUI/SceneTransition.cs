using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f;

    // 씬 전환 메서드
    public void LoadScene(int sceneNum)
    {
        StartCoroutine(TransitionToScene(sceneNum));
        
    }

    IEnumerator TransitionToScene(int sceneNum) 
    {
        // 애니메이션 트리거 실행
        animator.SetTrigger("FadeOut");
        Debug.Log("FadeOut Triggered");

        // 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(transitionTime);

        // 씬 로드
        SceneManager.LoadScene(sceneNum);
    }

}
