using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f;

    // �� ��ȯ �޼���
    public void LoadScene(int sceneNum)
    {
        StartCoroutine(TransitionToScene(sceneNum));
        
    }

    IEnumerator TransitionToScene(int sceneNum) 
    {
        // �ִϸ��̼� Ʈ���� ����
        animator.SetTrigger("FadeOut");
        Debug.Log("FadeOut Triggered");

        // �ִϸ��̼��� ���� ������ ���
        yield return new WaitForSeconds(transitionTime);

        // �� �ε�
        SceneManager.LoadScene(sceneNum);
    }

}
