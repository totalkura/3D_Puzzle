using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoadScene : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
       animator.SetTrigger("FadeOut");
       Debug.Log("FadeIn Triggered");
    }

    //// 씬 전환 메서드
    //public void LoadScene(int sceneNum)
    //{
    //    StartCoroutine(TransitionToScene(sceneNum));
    //}

    //IEnumerator TransitionToScene(int sceneNum)
    //{
    //    // 애니메이션 트리거 실행
    //    animator.SetTrigger("FadeOut");
    //    Debug.Log("FadeOut Triggered");

    //    // 애니메이션이 끝날 때까지 대기
    //    yield return new WaitForSeconds(transitionTime);

    //    // 씬 로드
    //    SceneManager.LoadScene(sceneNum);
    //}
}


