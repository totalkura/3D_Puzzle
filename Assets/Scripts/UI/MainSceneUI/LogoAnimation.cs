using System.Collections;
using UnityEngine;
using TMPro;

public class LogoAnimation : MonoBehaviour
{
    public TextMeshProUGUI mainLogo;
    public float fadeDuration = 1f; // 페이드 인/아웃 시간
    public float displayTime = 1f;  // 유지 시간

    private void Start()
    {
        // 게임 시작 시 한 번만 코루틴 실행
        StartCoroutine(LoopFadeLogo());
    }

    IEnumerator LoopFadeLogo()
    {
        Color c = mainLogo.color;

        while (true) // 무한 반복
        {
            // 🔹 페이드 인
            float time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                c.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
                mainLogo.color = c;
                yield return null;
            }

            // 🔹 유지
            yield return new WaitForSeconds(displayTime);

            // 🔹 페이드 아웃
            time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                c.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
                mainLogo.color = c;
                yield return null;
            }

            // 🔹 다시 반복
            yield return new WaitForSeconds(0.2f); // 약간의 텀을 줄 수도 있음
        }
    }
}
