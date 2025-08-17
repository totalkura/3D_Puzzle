using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image transitionImage;
    public float transitionDuration;
    public bool fadeInOnStart = true;


    public void Start()
    {
        //StartCoroutine(FadeIn());
    }

    public void LoadScene(int sceneName) 
    { 
        //StartCoroutine(FadeOut(sceneName));
    }

    //IEnumerator FaidIn() 
    //{ 

    //}

    IEnumerator FadeOut(int sceneName) 
    { 
        float elapsedTime = 0f;
        Color color = transitionImage.color;
        while (elapsedTime < transitionDuration) 
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / transitionDuration);
            transitionImage.color = color;
            yield return null;
        }
        //SceneManager.LoadScene(sceneName);
    }
}
