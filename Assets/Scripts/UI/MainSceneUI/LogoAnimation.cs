using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class LogoAnimation : MonoBehaviour
{
    [Header("로고 설정")]
    public TextMeshProUGUI mainLogo;
    public TextColorGradient textColorGradient; // 텍스트 색상 그라데이션 컴포넌트
    public float fadeDuration = 1f; // 페이드 인/아웃 시간
    public float displayTime = 1f;  // 유지 시간
    public Vector3 maxScale = new Vector3(1.2f, 1.2f, 1.2f); // 목표 스케일

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
                float progress = time / fadeDuration;

                c.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
                mainLogo.color = c;

                mainLogo.transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, progress);

                yield return null;
            }

            // 🔹 유지
            yield return new WaitForSeconds(displayTime);

            // 🔹 페이드 아웃
            time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                float progress = time / fadeDuration;

                c.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
                mainLogo.color = c;

                mainLogo.transform.localScale = Vector3.Lerp(maxScale, Vector3.zero, progress);

                yield return null;
            }

            // 🔹 다시 반복
            yield return new WaitForSeconds(0.2f); // 약간의 텀을 줄 수도 있음
        }
    }
}
